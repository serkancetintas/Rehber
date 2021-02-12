namespace Setur.Services.Contact.Core.Exceptions
{
    public class InvalidCreatedAtException : DomainException
    {
        public override string Code { get; } = "invalid_created_at";

        public InvalidCreatedAtException() : base("Invalid created at.")
        {
        }
    }
}
