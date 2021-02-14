using Setur.Services.Report.Application.DTO;
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

        public static ReportRequestDto AsDto(this ReportRequestDocument document)
           => new ReportRequestDto
           {
               Id = document.Id,
               RequestDate = document.RequestDate,
               State = document.State.ToString()
           };

        public static ReportDetailDto AsDetailDto(this ReportRequestDocument document)
           => new ReportDetailDto
           {
               Id = document.Id,
               State = document.State.ToString(),
               RequestDate = document.RequestDate,
               ReportResults = document.ReportResults.Select(p => new ReportDto
               {
                   ContactCount = p.ContactCount,
                   Location = p.Location,
                   PhoneNumberCount = p.PhoneNumberCount
               })
           };
    }
}
