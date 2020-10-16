using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Address;
using Pharmacy.Authorization;
using Pharmacy.Customers.Dtos;
using Pharmacy.Customers.Exporting;
using Pharmacy.Dto;
using Pharmacy.Invoices;
using Pharmacy.Lookups;
namespace Pharmacy.Customers
{
    public class CustomersAppService : PharmacyAppServiceBase, ICustomersAppService
    {
        private readonly IRepository<Lookup> _lookupRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Region> _regionRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly ICustomersExcelExporter _customerExcelExporter;
        public CustomersAppService(IRepository<Lookup> lookupRepository, IRepository<Customer> customerRepository,
            ICustomersExcelExporter customerExcelExporter, IRepository<Country> countryRepository, IRepository<City> cityRepository, IRepository<Region> regionRepository, IRepository<Invoice> invoiceRepository)
        {
            _lookupRepository = lookupRepository;
            _customerRepository = customerRepository;
            _customerExcelExporter = customerExcelExporter;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
            _regionRepository = regionRepository;
            _invoiceRepository = invoiceRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Customers_Manage)]
        public async Task CreateOrUpdateCustomer(CustomerDto input)
        {
            if (input.Id == null)
                await CreateAsync(input);
            else
                await UpdateAsync(input);

        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Customers_Manage)]
        public async Task DeleteCustomer(int? id)
        {
            if (id.HasValue)
                await _customerRepository.DeleteAsync(id.Value);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Customers)]
        public async Task<PagedResultDto<CustomerListDto>> GetAllCustomers(GetAllCustomerInput input)
        {
            var filteredCustomers = _customerRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.FullName.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                    || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(input.Filter.ToLower().Trim()) || !string.IsNullOrEmpty(e.PrimaryMobileNumber) && e.PrimaryMobileNumber.ToLower().Trim().Contains(input.Filter.ToLower().Trim()) || !string.IsNullOrEmpty(e.SecondaryMobileNumber) && e.SecondaryMobileNumber.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));

            var query = from customer in filteredCustomers
                        join country in _countryRepository.GetAll().AsNoTracking() on customer.NationalityId equals country.Id

                        select new CustomerListDto
                        {
                            Id = customer.Id,
                            FullName = customer.FullName.CurrentCultureText,
                            Code = customer.Code,
                            Email = customer.Email,
                            Nationality = country.Nationality.CurrentCultureText,
                            NoOfDependencies = customer.NoOfDependencies,
                            IsActive = customer.IsActive,
                            PrimaryPhoneNumber = customer.PrimaryMobileNumber
                        };
            var totalCount = await query.CountAsync();
            var customers = await query.OrderBy(input.Sorting ?? "id desc").PageBy(input).ToListAsync();
            return new PagedResultDto<CustomerListDto>(totalCount, customers);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Customers_Manage)]
        public async Task<CustomerDto> GetCustomerForEdit(int id)
        {
            var customer = await _customerRepository.GetAsync(id);
            return ObjectMapper.Map<CustomerDto>(customer);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Customers_Manage)]
        public async Task<GetCustomerForViewDto> GetCustomerForView(int id)
        {
            var customer = await _customerRepository.GetAll().FirstOrDefaultAsync(b => b.Id == id);
            if (customer == null)
                throw new UserFriendlyException($"No Customer With Id {id}");

            var country = await _countryRepository.FirstOrDefaultAsync(c => c.Id == customer.CountryId);
            var nationality = await _countryRepository.FirstOrDefaultAsync(c => c.Id == customer.NationalityId);
            var gender = await _lookupRepository.FirstOrDefaultAsync(c => c.Id == customer.GenderId);

            Region region = null;
            if (customer.RegionId.HasValue && customer.RegionId > 0)
                region = await _regionRepository.FirstOrDefaultAsync(c => c.Id == customer.RegionId);

            City city = null;
            if (customer.CityId.HasValue && customer.CityId > 0)
                city = await _cityRepository.FirstOrDefaultAsync(c => c.Id == customer.CityId);

            Lookup maritalStatus = null;
            if (customer.MaritalStatusId.HasValue && customer.MaritalStatusId > 0)
                maritalStatus = await _lookupRepository.FirstOrDefaultAsync(l => l.Id == customer.MaritalStatusId);

            var output = new GetCustomerForViewDto
            {
                Id = customer.Id,
                Code = customer.Code,
                FullName = customer.FullName.CurrentCultureText,
                DateOfBirth = customer.DateOfBirth,
                Email = customer.Email,
                NoOfDependencies = customer.NoOfDependencies,
                Notes = customer.Notes,
                PrimaryMobileNumber = customer.PrimaryMobileNumber,
                SecondaryMobileNumber = customer.SecondaryMobileNumber,
                PersonalPhoto = customer.PersonalPhoto,
                IsActive = customer.IsActive,
                Country = country?.Name.CurrentCultureText,
                Nationality = nationality?.Nationality.CurrentCultureText,
                Region = region?.Name.CurrentCultureText,
                City = city?.Name.CurrentCultureText,
                Gender = gender?.Name.CurrentCultureText,
                MaritalStatus = maritalStatus?.Name.CurrentCultureText,
                Address = customer.Address
            };
            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Customers_Export)]
        public async Task<FileDto> GetCustomersToExcel(GetAllCustomerForExcelInput input)
        {
            var filteredCustomers = _customerRepository.GetAll().AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => e.FullName.StringValue.ToLower().Contains(input.Filter.ToLower().Trim())
                                                                        || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(input.Filter.ToLower().Trim()));

            var query = from customer in filteredCustomers
                        join country in _countryRepository.GetAll().AsNoTracking() on customer.NationalityId equals country.Id

                        select new CustomerListDto
                        {
                            Id = customer.Id,
                            FullName = customer.FullName.CurrentCultureText,
                            Code = customer.Code,
                            Email = customer.Email,
                            Nationality = country.Nationality.CurrentCultureText,
                            NoOfDependencies = customer.NoOfDependencies,
                            IsActive = customer.IsActive,
                            PrimaryPhoneNumber = customer.PrimaryMobileNumber
                        };
            var customers = await query.ToListAsync();
            return _customerExcelExporter.ExportCustomersToFile(customers);
        }

        private async Task CreateAsync(CustomerDto input)
        {
            var customer = ObjectMapper.Map<Customer>(input);
            await _customerRepository.InsertAsync(customer);
        }

        private async Task UpdateAsync(CustomerDto input)
        {
            if (input.Id != null)
            {
                var customer = await _customerRepository.FirstOrDefaultAsync((int)input.Id);
                ObjectMapper.Map(input, customer);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Customers_Manage)]
        public async Task<GetLiteCustomerData> GetLiteCustomerData(string filter)
        {
            var customer = await _customerRepository.GetAllIncluding().WhereIf(!string.IsNullOrWhiteSpace(filter), e => e.FullName.StringValue.ToLower().Contains(filter.ToLower().Trim())
                   || !string.IsNullOrEmpty(e.Code) && e.Code.ToLower().Trim().Contains(filter.ToLower().Trim()) || !string.IsNullOrEmpty(e.PrimaryMobileNumber) && e.PrimaryMobileNumber.ToLower().Trim().Contains(filter.ToLower().Trim())).FirstOrDefaultAsync();
            if (customer == null)
                return null;

            var output = new GetLiteCustomerData
            {
                Id = customer.Id,
                FullName = customer.FullName.CurrentCultureText,
                MobileNumber = customer.PrimaryMobileNumber ?? customer.SecondaryMobileNumber,
            };
            return output;

        }

        public async Task<PagedResultDto<GetCustomerLoyaltyOutput>> GetCustomerLoyalty(GetCustomerLoyaltyInput input)
        {
            var query = from invoice in _invoiceRepository.GetAll().AsNoTracking().Where(i => i.CustomerId == input.CustomerId)
                        join invoiceType in _lookupRepository.GetAll().AsNoTracking() on invoice.InvoiceTypeId equals  invoiceType.Id
                        join customer in _customerRepository.GetAll() on invoice.CustomerId equals customer.Id
                        select new GetCustomerLoyaltyOutput
                        {
                            CustomerName = customer.FullName,
                            InvoiceId = invoice.Id,
                            InvoiceNumber = invoice.InvoiceNo,
                            Amount = invoice.NetAmount,
                            InvoiceDate = invoice.CreationTime,
                            InvoiceType = invoiceType.Name.CurrentCultureText
                        };
            var totalCount = await query.CountAsync();
            var customers = await query.OrderBy(input.Sorting ?? "invoiceNumber desc").PageBy(input).ToListAsync();
            return new PagedResultDto<GetCustomerLoyaltyOutput>(totalCount, customers);
        }
    }
}
