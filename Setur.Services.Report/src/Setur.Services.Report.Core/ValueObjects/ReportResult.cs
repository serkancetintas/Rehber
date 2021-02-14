namespace Setur.Services.Report.Core.ValueObjects
{
    public class ReportResult
    {
        public string Location { get; }
        public int ContactCount { get; }
        public int PhoneNumberCount { get; }

        public ReportResult(string location, int contactCount, int phoneNumberCount)
           => (Location, ContactCount, PhoneNumberCount) = (location, contactCount, phoneNumberCount);
    }
}
