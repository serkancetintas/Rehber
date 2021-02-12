namespace Setur.Services.Contact.Application.Commands
{
    public class CreateContact : ICommand
    {
        public string Name { get; }
        public string Surname { get; }
        public string CompanyName { get; }

        public CreateContact(string name, string surname, string companyName)
        {
            Name = name;
            Surname = surname;
            CompanyName = companyName;
        }
    }
}
