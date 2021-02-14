using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Setur.Services.Contact.Application.Commands;
using Setur.Services.Contact.Application.Commands.Handlers;
using Setur.Services.Contact.Application.Exceptions;
using Setur.Services.Contact.Core.Exceptions;
using Setur.Services.Contact.Core.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;
using entity = Setur.Services.Contact.Core.Entities;


namespace Setur.Services.Contact.Tests.Unit.Applications.Handlers
{
    public class AddContactInfoHandlerTests
    {
        [Theory, AutoMoqData]
        public async Task given_invalid_contact_should_throw_an_exception
            ([Frozen] Mock<IContactRepository> contactRepositoy,
           AddContactInfoHandler handler)
        {
            var id = Guid.NewGuid();
            entity.Contact contact = null;
            contactRepositoy.Setup(r => r.GetAsync(id)).ReturnsAsync(contact);

            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(new AddContactInfo(id,"","")));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<ContactNotFoundException>();
        }

        [Theory, AutoMoqData]
        public async Task given_invalid_info_type_should_throw_an_exception
           ([Frozen] Mock<IContactRepository> contactRepositoy,
            entity.Contact contact,
            AddContactInfoHandler handler)
        {
            var id = Guid.NewGuid();
            contactRepositoy.Setup(r => r.GetAsync(id)).ReturnsAsync(contact);

            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(new AddContactInfo(id, "Address", "")));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidInfoTypeException>();
        }

        [Theory, AutoMoqData]
        public async Task given_invalid_phone_number_should_throw_an_exception
           ([Frozen] Mock<IContactRepository> contactRepositoy,
            entity.Contact contact,
            AddContactInfoHandler handler)
        {
            var id = Guid.NewGuid();
            contactRepositoy.Setup(r => r.GetAsync(id)).ReturnsAsync(contact);
            string phoneNumber = "123";
            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(new AddContactInfo(id, "PhoneNumber", phoneNumber)));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidPhoneNumberException>();
        }

        [Theory, AutoMoqData]
        public async Task given_invalid_email_should_throw_an_exception
          ([Frozen] Mock<IContactRepository> contactRepositoy,
           entity.Contact contact,
           AddContactInfoHandler handler)
        {
            var id = Guid.NewGuid();
            contactRepositoy.Setup(r => r.GetAsync(id)).ReturnsAsync(contact);
            string email = "aa@com";
            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(new AddContactInfo(id, "Email", email)));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidEmailException>();
        }

        [Theory, AutoMoqData]
        public async Task given_invalid_location_should_throw_an_exception
          ([Frozen] Mock<IContactRepository> contactRepositoy,
           entity.Contact contact,
           AddContactInfoHandler handler)
        {
            var id = Guid.NewGuid();
            contactRepositoy.Setup(r => r.GetAsync(id)).ReturnsAsync(contact);
            string location = "10";
            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(new AddContactInfo(id, "Location", location)));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidLocationException>();
        }
    }
}
