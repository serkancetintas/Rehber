namespace Setur.Services.Contact.Core.Exceptions
{
    public class InvalidNameException: DomainException
    {
        public override string Code { get; } = "invalid_name";

        public InvalidNameException() : base("Invalid name.")
        {
        }
    }
}
