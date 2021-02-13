using Setur.Services.Contact.Application.DTO;
using System.Collections.Generic;

namespace Setur.Services.Contact.Application.Queries
{
    public class GetContacts: IQuery<IEnumerable<ContactDto>>
    {
    }
}
