using Setur.Services.Report.Application.DTO;
using System;

namespace Setur.Services.Report.Application.Queries
{
    public class GetReportDetails : IQuery<ReportDetailDto>
    {
        public Guid Id { get; set; }
    }
}
