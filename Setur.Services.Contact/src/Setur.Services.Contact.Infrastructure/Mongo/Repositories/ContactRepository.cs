using Setur.Services.Contact.Core.Repositories;
using Setur.Services.Contact.Infrastructure.Mongo.Documents;
using System;
using System.Threading.Tasks;
using entity = Setur.Services.Contact.Core.Entities;

namespace Setur.Services.Contact.Infrastructure.Mongo.Repositories
{
    public class ContactRepository:IContactRepository
    {
        private readonly IMongoRepository<ContactDocument, Guid> _repository;
        public ContactRepository(IMongoRepository<ContactDocument, Guid> repository)
        {
            _repository = repository;
        }

        public Task AddAsync(entity.Contact contact) => _repository.AddAsync(contact.AsDocument());

        public async Task<bool> IsExist(string name, string surname, string companyName)
        {
            var result = await _repository.ExistsAsync(x => x.Name == name && x.Surname == surname && x.CompanyName == companyName);

            return result;
        }
    }
}
