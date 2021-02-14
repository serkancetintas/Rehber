using System;

namespace Setur.Services.Report.Application.DTO
{
    public class ReportRequestDto
    {
        public Guid Id { get; set; }
        public DateTime RequestDate { get; set; }
        public string State { get; set; }
    }
}
