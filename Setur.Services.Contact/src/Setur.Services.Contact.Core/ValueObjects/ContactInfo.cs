using System;

namespace Setur.Services.Contact.Core.ValueObjects
{
    public struct ContactInfo
    {
        public string InfoContent { get; }
        public InfoType InfoType { get; }
        public DateTime CreatedAt { get; }

        public ContactInfo(string infoContent, InfoType infoType, DateTime createdAt)
           => (InfoContent, InfoType, CreatedAt) = (infoContent, infoType, createdAt);

    }
}
