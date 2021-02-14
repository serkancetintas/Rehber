using Setur.Services.Report.Core.Entities;
using Setur.Services.Report.Infrastructure.Types;
using System;
using System.Collections.Generic;

namespace Setur.Services.Report.Infrastructure.Mongo.Documents
{
    [BsonCollection("reportrequests")]
    public class ReportRequestDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public DateTime RequestDate { get; set; }
        public State State { get; set; }
        public IEnumerable<ReportResultDocument> ReportResults { get; set; }
    }
}
