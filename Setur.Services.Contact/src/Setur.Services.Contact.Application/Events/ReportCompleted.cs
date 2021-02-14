using Setur.Services.Contact.Application.DTO;
using System;
using System.Collections.Generic;

namespace Setur.Services.Contact.Application.Events
{
    public class ReportCompleted : IEvent
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
