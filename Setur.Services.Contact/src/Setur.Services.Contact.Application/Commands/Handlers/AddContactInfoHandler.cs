using Setur.Services.Contact.Application.Exceptions;
using Setur.Services.Contact.Core.Exceptions;
using Setur.Services.Contact.Core.Repositories;
using Setur.Services.Contact.Core.ValueObjects;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Setur.Services.Contact.Application.Commands.Handlers
{
    public class AddContactInfoHandler : ICommandHandler<AddContactInfo>
    {
        private readonly IContactRepository _repository;


        public AddContactInfoHandler(IContactRepository repository)
        {
            _repository = repository;
        }

        public async Task HandleAsync(AddContactInfo command)
        {
            var contact = await _repository.GetAsync(command.ContactId);
            if (contact is null)
            {
                throw new ContactNotFoundException(command.ContactId);
            }

            if (!Enum.TryParse<InfoType>(command.InfoType, true, out var infoType))
            {
                throw new InvalidInfoTypeException(command.InfoType);
            }

            switch (infoType)
            {
                case InfoType.PhoneNumber:
                    ValidatePhoneNumber(command.InfoContent);
                    break;
                case InfoType.Email:
                    ValidateEmail(command.InfoContent);
                    break;
                case InfoType.Location:
                    ValidateLocation(command.InfoContent);
                    break;
                default:
                    throw new InvalidInfoTypeException(command.InfoType);

            }

            contact.AddContactInfo(new ContactInfo(command.InfoContent, infoType, DateTime.Now));
            await _repository.UpdateAsync(contact);
        }

        private void ValidateEmail(string email)
        {
            Regex EmailRegex = new Regex(
           @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
           RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

            if (!EmailRegex.IsMatch(email))
            {
                throw new InvalidEmailException(email);
            }
        }

        private void ValidatePhoneNumber(string phoneNumber)
        {
            Regex PhoneNumberRegex = new Regex(
           @"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$",
           RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

            if (!PhoneNumberRegex.IsMatch(phoneNumber))
            {
                throw new InvalidPhoneNumberException(phoneNumber);
            }
        }

        private void ValidateLocation(string location)
        {
            Regex LocationRegex = new Regex(
           @"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$",
           RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

            if (!LocationRegex.IsMatch(location))
            {
                throw new InvalidLocationException(location);
            }
        }


    }
}
