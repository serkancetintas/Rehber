using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using Setur.Services.Contact.Application.Commands;
using Setur.Services.Contact.Application.Commands.Handlers;
using Setur.Services.Contact.Application.Exceptions;
using Setur.Services.Contact.Core.Repositories;
using System.Threading.Tasks;
using Xunit;


namespace Setur.Services.Contact.Tests.Unit.Applications.Handlers
{
    public class CreateContactHandlerTests
    {
        [Theory, AutoMoqData]
        public async Task given_same_contact_should_throw_an_exception
            ([Frozen] Mock<IContactRepository> contactRepositoy,
            CreateContactHandler handler)
        {
            contactRepositoy.Setup(r => r.IsExist("Test", "Test", "Test")).ReturnsAsync(true);

            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(new CreateContact("Test","Test","Test") ));

            exception.Should().NotBeNull();
            exception.Should().BeOfType<ContactAlreadyExistException>();
        }

        [Theory, AutoMoqData]
        public async Task given_valid_parameters_create_contact_should_success
           ([Frozen] Mock<IContactRepository> contactRepositoy,
           CreateContactHandler handler)
        {
            contactRepositoy.Setup(r => r.IsExist("Test", "Test", "Test")).ReturnsAsync(false);

            var exception = await Record.ExceptionAsync(async () => await handler.HandleAsync(new CreateContact("Test", "Test", "Test")));

            exception.Should().BeNull();
        }
    }
}
