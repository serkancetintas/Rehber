using Setur.Services.Contact.Application.DTO;
using System;

namespace Setur.Services.Contact.Application.Queries
{
    public class GetContactDetails: IQuery<ContactDetailDto>
    {
        public Guid Id { get; set; }
    }
}
