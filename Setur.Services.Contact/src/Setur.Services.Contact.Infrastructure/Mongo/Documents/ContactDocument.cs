using Setur.Services.Contact.Infrastructure.Types;
using System;
using System.Collections.Generic;

namespace Setur.Services.Contact.Infrastructure.Mongo.Documents
{
    [BsonCollection("contacts")]
    public class ContactDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public IEnumerable<ContactInfoDocument> ContactInfos { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
