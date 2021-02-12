using Setur.Services.Contact.Core.Exceptions;
using Setur.Services.Contact.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Setur.Services.Contact.Core.Entities
{
    public class Contact: AggregateRoot
    {
        private ISet<ContactInfo> _contactInfos = new HashSet<ContactInfo>();
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string CompanyName { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public IEnumerable<ContactInfo> ContactInfos
        {
            get => _contactInfos;
            private set => _contactInfos = new HashSet<ContactInfo>(value);
        }

        public Contact(Guid id, string name, string surname, string companyName, DateTime createdAt, IEnumerable<ContactInfo> contactInfos = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidNameException();
            }

            if (string.IsNullOrWhiteSpace(surname))
            {
                throw new InvalidSurnameException();
            }

            if (string.IsNullOrWhiteSpace(companyName))
            {
                throw new InvalidCompanyNameException();
            }

            if (DateTime.MinValue == createdAt)
            {
                throw new InvalidCreatedAtException();
            }

            Id = id;
            Name = name;
            Surname = surname;
            CompanyName = companyName;
            CreatedAt = createdAt;
            ContactInfos = contactInfos ?? Enumerable.Empty<ContactInfo>();
        }
    }
}
