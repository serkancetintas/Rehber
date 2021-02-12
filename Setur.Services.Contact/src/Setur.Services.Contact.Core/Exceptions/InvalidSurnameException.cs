namespace Setur.Services.Contact.Core.Exceptions
{
    public class InvalidSurnameException : DomainException
    {
        public override string Code { get; } = "invalid_surname";

        public InvalidSurnameException() : base("Invalid surname.")
        {
        }
    }
}
