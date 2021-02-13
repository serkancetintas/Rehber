using Setur.Services.Contact.Core.Entities;
using System;
using System.Threading.Tasks;
using entity = Setur.Services.Contact.Core.Entities;

namespace Setur.Services.Contact.Core.Repositories
{
    public interface IContactRepository
    {
        Task<entity.Contact> GetAsync(AggregateId id);
        Task AddAsync(entity.Contact contact);
        Task UpdateAsync(entity.Contact contact);
        Task DeleteAsync(Guid id);
        Task<bool> IsExist(string name, string surname, string companyName);
    }
}
