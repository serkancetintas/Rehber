namespace Setur.Services.Contact.Application.Exceptions
{
    public class ContactAlreadyExistException : AppException
    {
        public override string Code { get; } = "contact_already_exist";
        public string Name { get; }
        public string Surname { get; }
        public string CompanyName { get; }

        public ContactAlreadyExistException(string name, string surname, string companyName)
            : base($"Contact is already exist with name: '{name}', surname: '{surname}' in {companyName} company.")
        {
            Name = name;
            Surname = surname;
            CompanyName = companyName;
        }
    }
}
