using Setur.Services.Contact.Core.ValueObjects;

namespace Setur.Services.Contact.Core.Exceptions
{
    public class ContactInfoNotFoundException : DomainException
    {
        public override string Code { get; } = "contact_info_not_found";
        public InfoType InfoType { get; }
        public string InfoContent { get; }

        public ContactInfoNotFoundException(InfoType infoType, string infoContent) 
            : base($"Contact info with {infoType.ToString()} : {infoContent}  was not found.")
        {
            InfoType = infoType;
            InfoContent = infoContent;
        }
    }
}
