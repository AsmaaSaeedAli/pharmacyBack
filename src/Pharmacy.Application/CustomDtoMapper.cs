using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using AutoMapper;
using Pharmacy.Address;
using Pharmacy.Address.CityDtos;
using Pharmacy.Address.CountryDtos;
using Pharmacy.Address.RegionDtos;
using Pharmacy.Auditing.Dto;
using Pharmacy.Authorization.Accounts.Dto;
using Pharmacy.Authorization.Permissions.Dto;
using Pharmacy.Authorization.Roles;
using Pharmacy.Authorization.Roles.Dto;
using Pharmacy.Authorization.Users;
using Pharmacy.Authorization.Users.Dto;
using Pharmacy.Authorization.Users.Importing.Dto;
using Pharmacy.Authorization.Users.Profile.Dto;
using Pharmacy.Branches;
using Pharmacy.Branches.Dtos;
using Pharmacy.Corporates;
using Pharmacy.Corporates.Dtos;
using Pharmacy.Chat;
using Pharmacy.Chat.Dto;
using Pharmacy.Customers;
using Pharmacy.Customers.Dtos;
using Pharmacy.Editions;
using Pharmacy.Editions.Dto;
using Pharmacy.Employees;
using Pharmacy.Employees.Dtos;
using Pharmacy.Friendships;
using Pharmacy.Friendships.Cache;
using Pharmacy.Friendships.Dto;
using Pharmacy.Jobs.Dtos;
using Pharmacy.Localization.Dto;
using Pharmacy.Lookups;
using Pharmacy.Lookups.Dtos;
using Pharmacy.MultiTenancy;
using Pharmacy.MultiTenancy.Dto;
using Pharmacy.MultiTenancy.HostDashboard.Dto;
using Pharmacy.MultiTenancy.Payments;
using Pharmacy.MultiTenancy.Payments.Dto;
using Pharmacy.Notifications.Dto;
using Pharmacy.Organizations.Dto;
using Pharmacy.Sessions.Dto;
using Shared.SeedWork;
using Pharmacy.Categories.Dtos;
using Pharmacy.Categories;
using Pharmacy.Invoices;
using Pharmacy.Invoices.Dtos;
using Pharmacy.SubCategories.Dtos;
using Pharmacy.SubCategories;
using Pharmacy.Jobs;
using Pharmacy.ItemClasses;
using Pharmacy.ItemClasses.Dtos;
using Pharmacy.Items;
using Pharmacy.Items.ItemBarCodeDtos;
using Pharmacy.Items.ItemDtos;
using Pharmacy.Items.ItemPriceDtos;
using Pharmacy.Items.ItemQuantityDtos;
using Pharmacy.ManuFactories;
using Pharmacy.ManuFactories.Dtos;

namespace Pharmacy
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();


            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            configuration.CreateMap<LookupDto, Lookup>().ForMember(dest => dest.Name, opts => opts.MapFrom(src => new LocalizedText(src.Name))).ReverseMap();
            configuration.CreateMap<CountryDto, Country>().ForMember(dest => dest.Name, opts => opts.MapFrom(src => new LocalizedText(src.Name)))
                .ForMember(dest => dest.Nationality, opts => opts.MapFrom(src => new LocalizedText(src.Nationality))).ReverseMap();

            configuration.CreateMap<RegionDto, Region>().ForMember(dest => dest.Name, opts => opts.MapFrom(src => new LocalizedText(src.Name))).ReverseMap();
            configuration.CreateMap<CityDto, City>().ForMember(dest => dest.Name, opts => opts.MapFrom(src => new LocalizedText(src.Name))).ReverseMap();
            configuration.CreateMap<BranchDto, Branch>().ForMember(dest => dest.Name, opts => opts.MapFrom(src => new LocalizedText(src.Name))).ReverseMap();
            configuration.CreateMap<CorporateDto, Corporate>().ForMember(dest => dest.Name, opts => opts.MapFrom(src => new LocalizedText(src.Name))).ReverseMap();
            configuration.CreateMap<EmployeeDto, Employee>().ForMember(dest => dest.FullName, opts => opts.MapFrom(src => new LocalizedText(src.FullName))).ReverseMap();
            configuration.CreateMap<JobDto, Job>().ForMember(dest => dest.Name, opts => opts.MapFrom(src => new LocalizedText(src.Name))).ReverseMap();
            configuration.CreateMap<CustomerDto, Customer>().ForMember(dest => dest.FullName, opts => opts.MapFrom(src => new LocalizedText(src.FullName))).ReverseMap();
            configuration.CreateMap<CategoryDto, Category>().ForMember(dest => dest.Name, opts => opts.MapFrom(src => new LocalizedText(src.Name)))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => new LocalizedText(src.Description))).ReverseMap();
            configuration.CreateMap<SubCategoryDto, SubCategory>().ForMember(dest => dest.Name, opts => opts.MapFrom(src => new LocalizedText(src.Name))).ReverseMap();
            configuration.CreateMap<ItemClassDto, ItemClass>().ForMember(dest => dest.Name, opts => opts.MapFrom(src => new LocalizedText(src.Name))).ReverseMap();
            configuration.CreateMap<ItemDto, Item>().ForMember(dest => dest.Name, opts => opts.MapFrom(src => new LocalizedText(src.Name)))
                .ForMember(dest => dest.Description, opts => opts.MapFrom(src => new LocalizedText(src.Description))).ReverseMap();
            configuration.CreateMap<ItemBarCodeDto, ItemBarCode>().ReverseMap();
            configuration.CreateMap<ItemPriceDto, ItemPrice>().ReverseMap();
            configuration.CreateMap<ItemQuantityDto, ItemQuantity>().ReverseMap();
            configuration.CreateMap<InvoiceItemDto, InvoiceItem>().ReverseMap();
            configuration.CreateMap<InvoiceDto, Invoice>().ReverseMap();
            configuration.CreateMap<ManuFactoryDto, ManuFactory>().ForMember(dest => dest.Name, opts => opts.MapFrom(src => new LocalizedText(src.Name))).ReverseMap();
        }
    }
}
