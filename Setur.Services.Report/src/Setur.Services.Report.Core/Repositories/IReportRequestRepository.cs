using Setur.Services.Report.Core.Entities;
using System.Threading.Tasks;

namespace Setur.Services.Report.Core.Repositories
{
    public interface IReportRequestRepository
    {
        Task<ReportRequest> GetAsync(AggregateId id);
        Task AddAsync(ReportRequest reportRequest);
        Task UpdateAsync(ReportRequest reportRequest);
    }
}
