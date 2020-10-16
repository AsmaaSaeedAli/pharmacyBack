using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Pharmacy.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));
            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            var branches = administration.CreateChildPermission(AppPermissions.Pages_Administration_Branches, L("Branches"), multiTenancySides: MultiTenancySides.Tenant);
            branches.CreateChildPermission(AppPermissions.Pages_Administration_Branches_Manage, L("ManageBranches"), multiTenancySides: MultiTenancySides.Tenant);
            branches.CreateChildPermission(AppPermissions.Pages_Administration_Branches_Export, L("ExportBranches"), multiTenancySides: MultiTenancySides.Tenant);

            var corporates = administration.CreateChildPermission(AppPermissions.Pages_Administration_Corporates, L("Corporates"), multiTenancySides: MultiTenancySides.Tenant);
            corporates.CreateChildPermission(AppPermissions.Pages_Administration_Corporates_Manage, L("ManageCorporates"), multiTenancySides: MultiTenancySides.Tenant);
            corporates.CreateChildPermission(AppPermissions.Pages_Administration_Corporates_Export, L("ExportCorporates"), multiTenancySides: MultiTenancySides.Tenant);

            var manuFactories = administration.CreateChildPermission(AppPermissions.Pages_Administration_ManuFactories, L("ManuFactories"), multiTenancySides: MultiTenancySides.Tenant);
            manuFactories.CreateChildPermission(AppPermissions.Pages_Administration_ManuFactories_Manage, L("ManageManuFactories"), multiTenancySides: MultiTenancySides.Tenant);
            manuFactories.CreateChildPermission(AppPermissions.Pages_Administration_ManuFactories_Export, L("ExportManuFactories"), multiTenancySides: MultiTenancySides.Tenant);

            var employees = administration.CreateChildPermission(AppPermissions.Pages_Administration_Employees, L("Employees"), multiTenancySides: MultiTenancySides.Tenant);
            employees.CreateChildPermission(AppPermissions.Pages_Administration_Employees_Manage, L("ManageEmployees"), multiTenancySides: MultiTenancySides.Tenant);
            employees.CreateChildPermission(AppPermissions.Pages_Administration_Employees_Export, L("ExportEmployees"), multiTenancySides: MultiTenancySides.Tenant);

            var jobs = administration.CreateChildPermission(AppPermissions.Pages_Administration_Jobs, L("Jobs"), multiTenancySides: MultiTenancySides.Tenant);
            jobs.CreateChildPermission(AppPermissions.Pages_Administration_Jobs_Manage, L("ManageJobs"), multiTenancySides: MultiTenancySides.Tenant);
            jobs.CreateChildPermission(AppPermissions.Pages_Administration_Jobs_Export, L("ExportJobs"), multiTenancySides: MultiTenancySides.Tenant);


            var customers = administration.CreateChildPermission(AppPermissions.Pages_Administration_Customers, L("Customers"), multiTenancySides: MultiTenancySides.Tenant);
            customers.CreateChildPermission(AppPermissions.Pages_Administration_Customers_Manage, L("ManageCustomers"), multiTenancySides: MultiTenancySides.Tenant);
            customers.CreateChildPermission(AppPermissions.Pages_Administration_Customers_Export, L("ExportCustomers"), multiTenancySides: MultiTenancySides.Tenant);

            var invoices = administration.CreateChildPermission(AppPermissions.Pages_Administration_Invoices, L("Invoices"), multiTenancySides: MultiTenancySides.Tenant);
            invoices.CreateChildPermission(AppPermissions.Pages_Administration_Invoices_Manage, L("ManageInvoices"), multiTenancySides: MultiTenancySides.Tenant);
            invoices.CreateChildPermission(AppPermissions.Pages_Administration_Invoices_Discount, L("DiscountInvoices"), multiTenancySides: MultiTenancySides.Tenant);
            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);

            var lookups = pages.CreateChildPermission(AppPermissions.Pages_Administration_Host_Lookups, L("Lookups"), multiTenancySides: MultiTenancySides.Host);
            lookups.CreateChildPermission(AppPermissions.Pages_Administration_Host_Lookups_Manage, L("ManageLookups"), multiTenancySides: MultiTenancySides.Host);
            lookups.CreateChildPermission(AppPermissions.Pages_Administration_Host_Lookups_Export, L("ExportLookups"), multiTenancySides: MultiTenancySides.Host);

            var countries = pages.CreateChildPermission(AppPermissions.Pages_Administration_Host_Countries, L("Countries"), multiTenancySides: MultiTenancySides.Host);
            countries.CreateChildPermission(AppPermissions.Pages_Administration_Host_Countries_Manage, L("ManageCountries"), multiTenancySides: MultiTenancySides.Host);
            countries.CreateChildPermission(AppPermissions.Pages_Administration_Host_Countries_Export, L("ExportCountries"), multiTenancySides: MultiTenancySides.Host);

            var regions = pages.CreateChildPermission(AppPermissions.Pages_Administration_Host_Regions, L("Regions"), multiTenancySides: MultiTenancySides.Host);
            regions.CreateChildPermission(AppPermissions.Pages_Administration_Host_Regions_Manage, L("ManageRegions"), multiTenancySides: MultiTenancySides.Host);
            regions.CreateChildPermission(AppPermissions.Pages_Administration_Host_Regions_Export, L("ExportRegions"), multiTenancySides: MultiTenancySides.Host);

            var cities = pages.CreateChildPermission(AppPermissions.Pages_Administration_Host_Cities, L("Cities"), multiTenancySides: MultiTenancySides.Host);
            cities.CreateChildPermission(AppPermissions.Pages_Administration_Host_Cities_Manage, L("ManageCities"), multiTenancySides: MultiTenancySides.Host);
            cities.CreateChildPermission(AppPermissions.Pages_Administration_Host_Cities_Export, L("ExportCities"), multiTenancySides: MultiTenancySides.Host);

            var categories = pages.CreateChildPermission(AppPermissions.Pages_Administration_Host_Categories, L("Categories"), multiTenancySides: MultiTenancySides.Host);
            categories.CreateChildPermission(AppPermissions.Pages_Administration_Host_Categories_Manage, L("ManageCategories"), multiTenancySides: MultiTenancySides.Host);
            categories.CreateChildPermission(AppPermissions.Pages_Administration_Host_Categories_Export, L("ExportCategories"), multiTenancySides: MultiTenancySides.Host);

            var subcategories = pages.CreateChildPermission(AppPermissions.Pages_Administration_Host_SubCategories, L("SubCategories"), multiTenancySides: MultiTenancySides.Host);
            subcategories.CreateChildPermission(AppPermissions.Pages_Administration_Host_SubCategories_Manage, L("ManageSubCategories"), multiTenancySides: MultiTenancySides.Host);
            subcategories.CreateChildPermission(AppPermissions.Pages_Administration_Host_SubCategories_Export, L("ExportSubCategories"), multiTenancySides: MultiTenancySides.Host);

            var itemclasses = pages.CreateChildPermission(AppPermissions.Pages_Administration_Host_ItemClasses, L("ItemClasses"), multiTenancySides: MultiTenancySides.Host);
            itemclasses.CreateChildPermission(AppPermissions.Pages_Administration_Host_ItemClasses_Manage, L("ManageItemClasses"), multiTenancySides: MultiTenancySides.Host);
            itemclasses.CreateChildPermission(AppPermissions.Pages_Administration_Host_ItemClasses_Export, L("ExportItemClasses"), multiTenancySides: MultiTenancySides.Host);

            var items = pages.CreateChildPermission(AppPermissions.Pages_Administration_Items, L("Items"));
            items.CreateChildPermission(AppPermissions.Pages_Administration_Items_Manage, L("ManageItems"));
            items.CreateChildPermission(AppPermissions.Pages_Administration_Items_Export, L("ExportItems"));
            items.CreateChildPermission(AppPermissions.Pages_Administration_Items_ChangeVat, L("ChangeVat"));

            var itembarcodes = pages.CreateChildPermission(AppPermissions.Pages_Administration_ItemBarCodes, L("ItemBarCodes"));
            itembarcodes.CreateChildPermission(AppPermissions.Pages_Administration_ItemBarCodes_Manage, L("ManageItemBarCodes"));
            itembarcodes.CreateChildPermission(AppPermissions.Pages_Administration_ItemBarCodes_Export, L("ExportItemBarCodes"));

            var itemprices = pages.CreateChildPermission(AppPermissions.Pages_Administration_ItemPrices, L("ItemPrices"), multiTenancySides: MultiTenancySides.Tenant);
            itemprices.CreateChildPermission(AppPermissions.Pages_Administration_ItemPrices_Manage, L("ManageItemPrices"), multiTenancySides: MultiTenancySides.Tenant);
            itemprices.CreateChildPermission(AppPermissions.Pages_Administration_ItemPrices_Export, L("ExportItemPrices"), multiTenancySides: MultiTenancySides.Tenant);

            var itemquantities = pages.CreateChildPermission(AppPermissions.Pages_Administration_ItemQuantities, L("ItemQuantities"), multiTenancySides: MultiTenancySides.Tenant);
            itemquantities.CreateChildPermission(AppPermissions.Pages_Administration_ItemQuantities_Manage, L("ManageItemQuantities"), multiTenancySides: MultiTenancySides.Tenant);
            itemquantities.CreateChildPermission(AppPermissions.Pages_Administration_ItemQuantities_Export, L("ExportItemQuantities"), multiTenancySides: MultiTenancySides.Tenant);

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, PharmacyConsts.LocalizationSourceName);
        }
    }
}
