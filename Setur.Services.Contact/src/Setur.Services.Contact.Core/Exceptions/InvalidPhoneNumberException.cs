namespace Setur.Services.Contact.Core.Exceptions
{
    public class InvalidPhoneNumberException : DomainException
    {
        public override string Code { get; } = "invalid_phone_number";
        public InvalidPhoneNumberException(string phoneNumber) : base($"Invalid phone number: {phoneNumber}.")
        {
        }
    }
}
