using System.Threading.Tasks;
using entity = Setur.Services.Contact.Core.Entities;

namespace Setur.Services.Contact.Core.Repositories
{
    public interface IContactRepository
    {
        Task AddAsync(entity.Contact contact);
        Task<bool> IsExist(string name, string surname, string companyName);
    }
}
