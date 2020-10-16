namespace Pharmacy.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_Administration_Customers = "Pages.Administration.Customers";
        public const string Pages_Administration_Customers_Manage = "Pages.Administration.Customers.Manage";
        public const string Pages_Administration_Customers_Export = "Pages.Administration.Customers.Export";

        public const string Pages_Administration_Invoices = "Pages.Administration.Invoices";
        public const string Pages_Administration_Invoices_Manage = "Pages.Administration.Invoices.Manage";
        public const string Pages_Administration_Invoices_Discount = "Pages.Administration.Invoices.Discount";


        public const string Pages_Administration_Branches = "Pages.Administration.Branches";
        public const string Pages_Administration_Branches_Manage = "Pages.Administration.Branches.Manage";
        public const string Pages_Administration_Branches_Export = "Pages.Administration.Branches.Export";

        public const string Pages_Administration_Corporates = "Pages.Administration.Corporates";
        public const string Pages_Administration_Corporates_Manage = "Pages.Administration.Corporates.Manage";
        public const string Pages_Administration_Corporates_Export = "Pages.Administration.Corporates.Export";

        public const string Pages_Administration_ManuFactories = "Pages.Administration.ManuFactories";
        public const string Pages_Administration_ManuFactories_Manage = "Pages.Administration.ManuFactories.Manage";
        public const string Pages_Administration_ManuFactories_Export = "Pages.Administration.ManuFactories.Export";

        public const string Pages_Administration_Employees = "Pages.Administration.Employees";
        public const string Pages_Administration_Employees_Manage = "Pages.Administration.Employees.Manage";
        public const string Pages_Administration_Employees_Export = "Pages.Administration.Employees.Export";

        public const string Pages_Administration_Jobs = "Pages.Administration.Jobs";
        public const string Pages_Administration_Jobs_Manage = "Pages.Administration.Jobs.Manage";
        public const string Pages_Administration_Jobs_Export = "Pages.Administration.Jobs.Export";


        public const string Pages_DemoUiComponents= "Pages.DemoUiComponents";
        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions = "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Impersonation = "Pages.Administration.Users.Impersonation";
        public const string Pages_Administration_Users_Unlock = "Pages.Administration.Users.Unlock";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";
        public const string Pages_Administration_OrganizationUnits_ManageRoles = "Pages.Administration.OrganizationUnits.ManageRoles";

        public const string Pages_Administration_HangfireDashboard = "Pages.Administration.HangfireDashboard";

        public const string Pages_Administration_UiCustomization = "Pages.Administration.UiCustomization";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";

        public const string Pages_Administration_Tenant_SubscriptionManagement = "Pages.Administration.Tenant.SubscriptionManagement";

        //HOST-SPECIFIC PERMISSIONS

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";
        public const string Pages_Editions_MoveTenantsToAnotherEdition = "Pages.Editions.MoveTenantsToAnotherEdition";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";
        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";
        public const string Pages_Administration_Host_Dashboard = "Pages.Administration.Host.Dashboard";
        
        
        
        public const string Pages_Administration_Host_Lookups= "Pages.Administration.Host.Lookups";
        public const string Pages_Administration_Host_Lookups_Manage= "Pages.Administration.Host.Lookups.Manage";
        public const string Pages_Administration_Host_Lookups_Export = "Pages.Administration.Host.Lookups.Export";

        public const string Pages_Administration_Host_Countries = "Pages.Administration.Host.Countries";
        public const string Pages_Administration_Host_Countries_Manage = "Pages.Administration.Host.Countries.Manage";
        public const string Pages_Administration_Host_Countries_Export = "Pages.Administration.Host.Countries.Export";

        public const string Pages_Administration_Host_Regions = "Pages.Administration.Host.Regions";
        public const string Pages_Administration_Host_Regions_Manage = "Pages.Administration.Host.Regions.Manage";
        public const string Pages_Administration_Host_Regions_Export = "Pages.Administration.Host.Regions.Export";

        public const string Pages_Administration_Host_Cities = "Pages.Administration.Host.Cities";
        public const string Pages_Administration_Host_Cities_Manage = "Pages.Administration.Host.Cities.Manage";
        public const string Pages_Administration_Host_Cities_Export = "Pages.Administration.Host.Cities.Export";

        public const string Pages_Administration_Host_Categories = "Pages.Administration.Host.Categories";
        public const string Pages_Administration_Host_Categories_Manage = "Pages.Administration.Host.Categories.Manage";
        public const string Pages_Administration_Host_Categories_Export = "Pages.Administration.Host.Categories.Export";

        public const string Pages_Administration_Host_SubCategories = "Pages.Administration.Host.SubCategories";
        public const string Pages_Administration_Host_SubCategories_Manage = "Pages.Administration.Host.SubCategories.Manage";
        public const string Pages_Administration_Host_SubCategories_Export = "Pages.Administration.Host.SubCategories.Export";

        public const string Pages_Administration_Host_ItemClasses = "Pages.Administration.Host.ItemClasses";
        public const string Pages_Administration_Host_ItemClasses_Manage = "Pages.Administration.Host.ItemClasses.Manage";
        public const string Pages_Administration_Host_ItemClasses_Export = "Pages.Administration.Host.ItemClasses.Export";

        public const string Pages_Administration_Items = "Pages.Administration.Items";
        public const string Pages_Administration_Items_Manage = "Pages.Administration.Items.Manage";
        public const string Pages_Administration_Items_Export = "Pages.Administration.Items.Export";
        public const string Pages_Administration_Items_ChangeVat= "Pages.Administration.Items.ChangeVat";

        public const string Pages_Administration_ItemBarCodes = "Pages.Administration.ItemBarCodes";
        public const string Pages_Administration_ItemBarCodes_Manage = "Pages.Administration.ItemBarCodes.Manage";
        public const string Pages_Administration_ItemBarCodes_Export = "Pages.Administration.ItemBarCodes.Export";

        public const string Pages_Administration_ItemPrices = "Pages.Administration.ItemPrices";
        public const string Pages_Administration_ItemPrices_Manage = "Pages.Administration.ItemPrices.Manage";
        public const string Pages_Administration_ItemPrices_Export = "Pages.Administration.ItemPrices.Export";

        public const string Pages_Administration_ItemQuantities = "Pages.Administration.Tenants.ItemQuantities";
        public const string Pages_Administration_ItemQuantities_Manage = "Pages.Administration.Tenants.ItemQuantities.Manage";
        public const string Pages_Administration_ItemQuantities_Export = "Pages.Administration.Tenants.ItemQuantities.Export";

    }
}