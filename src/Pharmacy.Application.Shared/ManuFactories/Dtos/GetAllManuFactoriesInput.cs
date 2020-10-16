using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.ManuFactories.Dtos
{
    public class GetAllManuFactoriesInput:PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
    
}
