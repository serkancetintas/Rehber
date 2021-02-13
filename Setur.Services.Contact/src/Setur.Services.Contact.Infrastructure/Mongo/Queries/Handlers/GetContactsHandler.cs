using Setur.Services.Contact.Application.DTO;
using Setur.Services.Contact.Infrastructure.Mongo.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using query = Setur.Services.Contact.Application.Queries;

namespace Setur.Services.Contact.Infrastructure.Mongo.Queries.Handlers
{
    public class GetContactsHandler: query.IQueryHandler<query.GetContacts,IEnumerable<ContactDto>>
    {
        private readonly IMongoRepository<ContactDocument, Guid> _contactRepository;

        public GetContactsHandler(IMongoRepository<ContactDocument, Guid> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<ContactDto>> HandleAsync(query.GetContacts query)
        {
            var result = await _contactRepository.FindAsync(p => true);

            return result.Select(r => r.AsDto());
        }
    }
}
