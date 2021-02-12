using Setur.Services.Contact.Core.ValueObjects;
using System;

namespace Setur.Services.Contact.Infrastructure.Mongo.Documents
{
    public class ContactInfoDocument
    {
        public string InfoContent { get; set; }
        public InfoType InfoType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
