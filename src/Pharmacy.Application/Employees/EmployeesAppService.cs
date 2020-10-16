using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Pharmacy.Branches;
using Pharmacy.Dto;
using Pharmacy.Employees.Dtos;
using System.Threading.Tasks;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;
using Pharmacy.Employees.Exporting;
using Abp.Authorization;
using Pharmacy.Authorization;
using Pharmacy.Authorization.Users;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Notifications;
using Abp.Notifications;
using System.Collections.ObjectModel;
using Abp.Authorization.Users;
using Pharmacy.Authorization.Roles;
using Pharmacy.Jobs;
using Shared.SeedWork;

namespace Pharmacy.Employees
{
    public class EmployeesAppService : PharmacyAppServiceBase, IEmployeesAppService
    {
        private readonly IRepository<Employee> _employeeRepository;
        private readonly IRepository<Branch> _branchRepository;
        private readonly IRepository<Job> _jobRepository;
        private readonly IEmployeesExcelExporter _employeesExcelExporter;
        private readonly IUserPolicy _userPolicy;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAppNotifier _appNotifier;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly RoleManager _roleManager;

        public EmployeesAppService(IRepository<Employee> employeeRepository,
            IRepository<Branch> branchRepository, IEmployeesExcelExporter employeesExcelExporter, IUserPolicy userPolicy, 
            IPasswordHasher<User> passwordHasher, IAppNotifier appNotifier, 
            INotificationSubscriptionManager notificationSubscriptionManager, RoleManager roleManager, IRepository<Job> jobRepository)
        {
            _employeeRepository = employeeRepository;
            _branchRepository = branchRepository;
            _employeesExcelExporter = employeesExcelExporter;
            _userPolicy = userPolicy;
            _passwordHasher = passwordHasher;
            _appNotifier = appNotifier;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _roleManager = roleManager;
            _jobRepository = jobRepository;
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_Employees_Manage)]
        public async Task CreateOrUpdateEmployee(EmployeeDto input)
        {
            await ValidateEmployeeNationalId(input);
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);
        }

        async Task ValidateEmployeeNationalId(EmployeeDto input)
        {
            var employee = await _employeeRepository.GetAll().IgnoreQueryFilters().Where(e =>
                !e.IsDeleted && e.NationalId == input.NationalId.Trim() && e.Id != input.Id).FirstOrDefaultAsync();
           
            if (employee != null)
                throw new UserFriendlyException(L("NationalIdIsExist", input.NationalId));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Employees_Manage)]
        public async Task DeleteEmployee(int? id)
        {
            if (id.HasValue)
                await _employeeRepository.DeleteAsync(id.Value);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Employees)]
        public async Task<PagedResultDto<EmployeeListDto>> GetAllEmployees(GetAllEmployeeInput input)
        {
            var filteredEmployees = _employeeRepository.GetAllIncluding(e => e.Nationality).AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    e => e.FullName.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                    || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));

            var query = from employee in filteredEmployees
                        join branch in _branchRepository.GetAll().AsNoTracking() on employee.BranchId equals branch.Id into branches
                        from branch in branches.DefaultIfEmpty()

                        select new EmployeeListDto
                        {
                            Id = employee.Id,
                            FullName = employee.FullName.CurrentCultureText,
                            Email = employee.Email,
                            DateOfBirth = employee.DateOfBirth,
                            PhoneNumber = employee.PhoneNumber,
                            BranchName = branch == null ? "" : branch.Name.CurrentCultureText,
                            NationalId = employee.NationalId,
                            Code = employee.Code,
                            IsActive = employee.IsActive,
                            Nationality = employee.Nationality != null? employee.Nationality.Nationality.CurrentCultureText : ""
                        };
            var totalCount = await query.CountAsync();
            var employees = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<EmployeeListDto>(totalCount, employees);
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_Employees_Manage)]
        public async Task<EmployeeDto> GetEmployeeForEdit(int id)
        {
            var employee = await _employeeRepository.GetAsync(id);
            return ObjectMapper.Map<EmployeeDto>(employee);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Employees_Manage)]
        public async Task<EmployeeForViewDto> GetEmployeeForView(int id)
        {
            var employee = await _employeeRepository.GetAllIncluding(b => b.Nationality, b => b.Gender).FirstOrDefaultAsync(b => b.Id == id);

            if (employee == null)
                throw new UserFriendlyException($"No Employee With Id {id}");
            var branchName = "";
            if (employee.BranchId.HasValue)
            {
                var branch = await _branchRepository.GetAsync(employee.BranchId.Value);
                branchName = branch?.Name.CurrentCultureText;
            }
            var jobName = "";
            if (employee.JobId.HasValue)
            {
                var job = await _jobRepository.GetAsync(employee.JobId.Value);
                jobName = job?.Name.CurrentCultureText;
            }

            var output = new EmployeeForViewDto
            {
                Id = employee.Id,
                Code = employee.Code,
                Name = employee.FullName.CurrentCultureText,
                IsActive = employee.IsActive,
                Address = employee.Address,
                PhoneNumber = employee.PhoneNumber,
                BranchName = branchName,
                NationalId = employee.NationalId,
                GenderName = employee.Gender.Name.CurrentCultureText,
                Nationality = employee.Nationality.Nationality.CurrentCultureText,
                JobName = jobName,
                PersonalPhoto = employee.PersonalPhone,
                Email = employee.Email
            };
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Employees_Export)]
        public async Task<FileDto> GetEmployeesToExcel(string filter)
        {
            var filteredEmployees = _employeeRepository.GetAllIncluding(e => e.Nationality).AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(filter),
                    e => e.FullName.StringValue.ToLower().Contains(filter.ToLower().Trim())
                         || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(filter.ToLower().Trim()));

            var query = from employee in filteredEmployees
                        join branch in _branchRepository.GetAll().AsNoTracking() on employee.BranchId equals branch.Id into branches
                        from branch in branches.DefaultIfEmpty()

                        select new EmployeeListDto
                        {
                            Id = employee.Id,
                            FullName = employee.FullName.CurrentCultureText,
                            Email = employee.Email,
                            DateOfBirth = employee.DateOfBirth,
                            PhoneNumber = employee.PhoneNumber,
                            BranchName = branch == null ? "" : branch.Name.CurrentCultureText,
                            NationalId = employee.NationalId,
                            Code = employee.Code,
                            IsActive = employee.IsActive,
                            Nationality = employee.Nationality == null ? "":  employee.Nationality.Nationality.CurrentCultureText
                        };
            return _employeesExcelExporter.ExportToFile(await query.ToListAsync());
        }

        private async Task CreateAsync(EmployeeDto input)
        {
            var employee = ObjectMapper.Map<Employee>(input);
            await _employeeRepository.InsertAsync(employee);
            if (input.HasUser)
                await CreateUser(input);

        }

        async Task CreateUser(EmployeeDto input)
        {
            if (AbpSession.TenantId.HasValue)
                await _userPolicy.CheckMaxUserCountAsync(AbpSession.GetTenantId());

            var user = new User
            {
                Name = new LocalizedText(input.FullName)["en"],
                Surname = new LocalizedText(input.FullName)["en"],
                UserName = input.NationalId,
                EmailAddress = input.Email,
                TenantId = AbpSession.TenantId,
            };


            user.Password = _passwordHasher.HashPassword(user, input.NationalId);
            user.ShouldChangePasswordOnNextLogin = true;

            //Assign roles
            user.Roles = new Collection<UserRole>();
            var role = await _roleManager.GetRoleByNameAsync(StaticRoleNames.Tenants.User);
            user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));


            CheckErrors(await UserManager.CreateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync();

            //Notifications
            await _notificationSubscriptionManager.SubscribeToAllAvailableNotificationsAsync(user.ToUserIdentifier());
            await _appNotifier.WelcomeToTheApplicationAsync(user);

        }
        private async Task UpdateAsync(EmployeeDto input)
        {
            if (input.Id != null)
            {
                var employee = await _employeeRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, employee);
            }
        }
    }
}
