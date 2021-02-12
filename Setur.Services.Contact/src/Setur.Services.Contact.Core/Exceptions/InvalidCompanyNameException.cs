namespace Setur.Services.Contact.Core.Exceptions
{
    public class InvalidCompanyNameException : DomainException
    {
        public override string Code { get; } = "invalid_company_name";

        public InvalidCompanyNameException() : base("Invalid company name.")
        {
        }
    }
}
