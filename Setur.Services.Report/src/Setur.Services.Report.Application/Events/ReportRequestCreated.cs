using System;

namespace Setur.Services.Report.Application.Events
{
    public class ReportRequestCreated:IEvent
    {
        public Guid ReportRequestId { get; }

        public ReportRequestCreated(Guid reportRequestId)
        {
            ReportRequestId = reportRequestId;
        }
    }
}
