using Setur.Services.Contact.Application.Services;
using System;

namespace Setur.Services.Contact.Application.Events
{
    [Message("report")]
    public class ReportRequestCreated:IEvent
    {
        public Guid ReportRequestId { get; }

        public ReportRequestCreated(Guid reportRequestId)
        {
            ReportRequestId = reportRequestId;
        }
    }
}
