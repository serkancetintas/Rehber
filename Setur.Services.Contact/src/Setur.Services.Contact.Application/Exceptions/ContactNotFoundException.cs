using System;

namespace Setur.Services.Contact.Application.Exceptions
{
    public class ContactNotFoundException: AppException
    {
        public override string Code { get; } = "contact_not_found";
        public Guid Id { get; }

        public ContactNotFoundException(Guid id) : base($"Contact with id: {id} was not found.")
            => Id = id;
    }
}
