using System;
using System.Collections.Generic;

namespace Setur.Services.Report.Application.DTO
{
    public class ReportDetailDto
    {
        public Guid Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string State { get; set; }
        public IEnumerable<ReportDto> ReportResults { get; set; }
    }
}
