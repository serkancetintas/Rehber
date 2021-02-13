using System;

namespace Setur.Services.Contact.Application.Commands
{
    public class DeleteContactInfo : ICommand
    {
        public Guid ContactId { get; }
        public string InfoType { get; }
        public string InfoContent { get; }
        public DeleteContactInfo(Guid contactId, string infoType, string infoContent)
        {
            ContactId = contactId;
            InfoType = infoType;
            InfoContent = infoContent;
        }
    }
}
