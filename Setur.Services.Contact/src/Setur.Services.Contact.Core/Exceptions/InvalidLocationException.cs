namespace Setur.Services.Contact.Core.Exceptions
{
    public class InvalidLocationException : DomainException
    {
        public override string Code { get; } = "invalid_location";
        public InvalidLocationException(string location) : base($"Invalid location: {location}.")
        {
        }
    }
}
