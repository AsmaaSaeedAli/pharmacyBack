using Abp.Modules;
using Pharmacy.Test.Base;

namespace Pharmacy.Tests
{
    [DependsOn(typeof(PharmacyTestBaseModule))]
    public class PharmacyTestModule : AbpModule
    {
       
    }
}
