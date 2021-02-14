using Setur.Services.Report.Application.DTO;
using Setur.Services.Report.Application.Services;
using System;
using System.Collections.Generic;

namespace Setur.Services.Report.Application.Events.External
{
    [Message("contact")]
    public class ReportCompleted: IEvent
    {
        public Guid ReportId { get; }
        public List<ReportDto> ReportResult { get; }

        public ReportCompleted(Guid reportId, List<ReportDto> reportResult)
        {
            ReportId = reportId;
            ReportResult = reportResult;
        }
    }
}
