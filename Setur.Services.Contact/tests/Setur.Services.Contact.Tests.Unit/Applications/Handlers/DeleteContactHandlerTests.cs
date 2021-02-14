using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Setur.Services.Contact.Application.Commands;
using Setur.Services.Contact.Application.Commands.Handlers;
using Setur.Services.Contact.Application.Exceptions;
using Setur.Services.Contact.Core.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;
using entity = Setur.Services.Contact.Core.Entities;

namespace Setur.Services.Contact.Tests.Unit.Applications.Handlers
{
    public class DeleteContactHandlerTests
    {
        [Theory, AutoMoqData]
        public async Task given_invalid_contact_should_throw_an_exception
             ([Frozen] Mock<IContactRepository> contactRepositoy,
            DeleteContactHandler handler)
        {
            var id = Guid.NewGuid();
            entity.Contact contact = null;
            contactRepositoy.Setup(r => r.GetAsync(id)).ReturnsAsync(contact);

            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(new DeleteContact(id)));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<ContactNotFoundException>();
        }

        [Theory, AutoMoqData]
        public async Task given_valid_parameters_delete_contact_should_success
             ([Frozen] Mock<IContactRepository> contactRepositoy,
            entity.Contact contact,
            DeleteContactHandler handler)
        {
            var id = Guid.NewGuid();
            contactRepositoy.Setup(r => r.GetAsync(id)).ReturnsAsync(contact);

            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(new DeleteContact(id)));

            exception.Should().BeNull();
        }
    }
}
