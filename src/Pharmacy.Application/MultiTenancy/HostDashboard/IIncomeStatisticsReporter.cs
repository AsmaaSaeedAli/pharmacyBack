using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pharmacy.MultiTenancy.HostDashboard.Dto;

namespace Pharmacy.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}