using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Pharmacy.Editions.Dto;

namespace Pharmacy.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}