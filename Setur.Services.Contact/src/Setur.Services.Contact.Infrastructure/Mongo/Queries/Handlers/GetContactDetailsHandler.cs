using Setur.Services.Contact.Application.DTO;
using Setur.Services.Contact.Infrastructure.Mongo.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using query = Setur.Services.Contact.Application.Queries;

namespace Setur.Services.Contact.Infrastructure.Mongo.Queries.Handlers
{
    public class GetContactDetailsHandler: query.IQueryHandler<query.GetContactDetails,ContactDetailDto>
    {
        private readonly IMongoRepository<ContactDocument, Guid> _contactRepository;

        public GetContactDetailsHandler(IMongoRepository<ContactDocument, Guid> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ContactDetailDto> HandleAsync(query.GetContactDetails query)
        {
            var result = await _contactRepository.GetAsync(query.Id);

            return result.AsDetailDto();
        }
    }
}
