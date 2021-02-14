using Setur.Services.Report.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace Setur.Services.Report.Core.Entities
{
    public class ReportRequest:AggregateRoot
    {
        private ISet<ReportResult> _reportResults = new HashSet<ReportResult>();

        public DateTime RequestDate { get; private set; }
        public State State { get; private set; }
        public IEnumerable<ReportResult> ReportResults
        {
            get => _reportResults;
            private set => _reportResults = new HashSet<ReportResult>(value);
        }

        public ReportRequest(Guid id, DateTime requestDate, State state)
        {
            Id = id;
            RequestDate = requestDate;
            State = state;
        }

        public void SetReportResult(IEnumerable<ReportResult> reportResults)
        {
            ReportResults = reportResults;
        }

        public void SetCompleted() => State = State.Completed;

    }
}
