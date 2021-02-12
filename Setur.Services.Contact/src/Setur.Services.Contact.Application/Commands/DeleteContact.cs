using System;

namespace Setur.Services.Contact.Application.Commands
{
    public class DeleteContact : ICommand
    {
        public Guid Id { get; }
        public DeleteContact(Guid id)
        {
            Id = id;
        }
    }
}
