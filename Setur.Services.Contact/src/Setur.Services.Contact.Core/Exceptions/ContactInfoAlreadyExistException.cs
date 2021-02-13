namespace Setur.Services.Contact.Core.Exceptions
{
    public class ContactInfoAlreadyExistException : DomainException
    {
        public override string Code { get; } = "contact_info_already_exist";
        public string InfoType { get; }
        public string InfoContent { get; }
        public ContactInfoAlreadyExistException(string infoType, string infoContent)
            : base($"Contact info is already exist with {infoType} : {infoContent}")
        {
            InfoType = infoType;
            InfoContent = infoContent;
        }
    }
}
