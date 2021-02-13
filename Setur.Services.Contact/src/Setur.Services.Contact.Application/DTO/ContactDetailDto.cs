using System;
using System.Collections.Generic;

namespace Setur.Services.Contact.Application.DTO
{
    public class ContactDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        public IEnumerable<ContactInfoDto> ContactInfos { get; set; }
    }
}
