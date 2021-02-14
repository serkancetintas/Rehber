using Setur.Services.Report.Core.Entities;
using System.Linq;

namespace Setur.Services.Report.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static ReportRequest AsEntity(this ReportRequestDocument reportRequest)
           => new ReportRequest(reportRequest.Id,
                                reportRequest.RequestDate,
                                reportRequest.State);


        public static ReportRequestDocument AsDocument(this ReportRequest entity)
            => new ReportRequestDocument
            {
                Id = entity.Id,
                State = entity.State,
                RequestDate = entity.RequestDate,
                ReportResults = entity.ReportResults.Select(p => new ReportResultDocument
                {
                    Location = p.Location,
                    ContactCount = p.ContactCount,
                    PhoneNumberCount = p.PhoneNumberCount
                })
            };
    }
}
