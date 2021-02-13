using System;

namespace Setur.Services.Contact.Application.DTO
{
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
    }
}
