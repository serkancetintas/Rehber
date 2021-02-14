using Setur.Services.Report.Core.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Setur.Services.Report.Core.ValueObjects;

namespace Setur.Services.Report.Application.Events.External.Handlers
{
    public class ReportCompletedHandler : IEventHandler<ReportCompleted>
    {
        private readonly IReportRequestRepository _repository;
        public ReportCompletedHandler(IReportRequestRepository repository)
        {
            _repository = repository;
        }
        public async Task HandleAsync(ReportCompleted @event)
        {
            var report  = await _repository.GetAsync(@event.ReportId);

            var reportResults = @event.ReportResult.Select(p => new ReportResult(p.Location, p.ContactCount, p.PhoneNumberCount));
            report.SetReportResult(reportResults);
            report.SetCompleted();

            await _repository.UpdateAsync(report);
        }
    }
}
