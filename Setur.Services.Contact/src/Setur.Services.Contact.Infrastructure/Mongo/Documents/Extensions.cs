using Setur.Services.Contact.Application.DTO;
using Setur.Services.Contact.Core.ValueObjects;
using System.Linq;
using entity = Setur.Services.Contact.Core.Entities;

namespace Setur.Services.Contact.Infrastructure.Mongo.Documents
{
    internal static class Extensions
    {
        public static entity.Contact AsEntity(this ContactDocument contact)
            => new entity.Contact(contact.Id,
                                  contact.Name,
                                  contact.Surname,
                                  contact.CompanyName,
                                  contact.CreatedAt,
                                  contact.ContactInfos.Select(p => new ContactInfo(p.InfoContent, p.InfoType, p.CreatedAt)));

        public static ContactDocument AsDocument(this entity.Contact entity)
            => new ContactDocument
            {
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                CompanyName = entity.CompanyName,
                CreatedAt = entity.CreatedAt,
                ContactInfos = entity.ContactInfos.Select(p => new ContactInfoDocument
                {
                    InfoType = p.InfoType,
                    InfoContent = p.InfoContent,
                    CreatedAt = p.CreatedAt
                })
            };

        public static ContactDto AsDto(this ContactDocument document)
            => new ContactDto
            {
                Id = document.Id,
                Name = document.Name,
                Surname = document.Surname,
                CompanyName = document.CompanyName
            };

        public static ContactDetailDto AsDetailDto(this ContactDocument document)
           => new ContactDetailDto
           {
               Id = document.Id,
               Name = document.Name,
               Surname = document.Surname,
               CompanyName = document.CompanyName,
               ContactInfos = document.ContactInfos.Select(p=>new ContactInfoDto
               {
                   InfoContent = p.InfoContent,
                   InfoType = p.InfoType.ToString(),
               })
           };
    }
}
