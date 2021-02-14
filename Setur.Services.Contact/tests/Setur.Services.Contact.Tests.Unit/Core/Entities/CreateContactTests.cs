using FluentAssertions;
using Setur.Services.Contact.Core.Entities;
using Setur.Services.Contact.Core.Exceptions;
using Setur.Services.Contact.Core.ValueObjects;
using System;
using Xunit;
using entity = Setur.Services.Contact.Core.Entities;

namespace Setur.Services.Contact.Tests.Unit.Core.Entities
{
    public class CreateContactTests
    {
        private entity.Contact Act(AggregateId id, string name, string surname, string companyName, DateTime createdAt)
            => new entity.Contact(id, name, surname, companyName, createdAt);

        [Fact]
        public void given_valid_params_contact_should_be_created()
        {
            // Arrange
            var id = new AggregateId();
            string name = "Ahmet";
            string surname = "Korkmaz";
            string companyName = "CITS";
            var createdAt = DateTime.Now;

            //Act
            var contact = Act(id, name, surname, companyName, createdAt);

            //Assert
            contact.Should().NotBeNull();
            contact.Id.Should().Be(id);
            contact.Name.Should().Be(name);
            contact.Surname.Should().Be(surname);
            contact.CompanyName.Should().Be(companyName);
            contact.CreatedAt.Should().Be(createdAt);
        }

        [Fact]
        public void given_empty_name_contact_should_throw_an_exception()
        {
            // Arrange
            var id = new AggregateId();
            string name = string.Empty;
            string surname = "Korkmaz";
            string companyName = "CITS";
            var createdAt = DateTime.Now;

            //Act
            var exception = Record.Exception(() => Act(id, name, surname, companyName, createdAt));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidNameException>();
        }

        [Fact]
        public void given_empty_surname_contact_should_throw_an_exception()
        {
            // Arrange
            var id = new AggregateId();
            string name = "Ahmet";
            string surname = string.Empty;
            string companyName = "CITS";
            var createdAt = DateTime.Now;

            //Act
            var exception = Record.Exception(() => Act(id, name, surname, companyName, createdAt));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidSurnameException>();
        }

        [Fact]
        public void given_empty_company_name_contact_should_throw_an_exception()
        {
            // Arrange
            var id = new AggregateId();
            string name = "Ahmet";
            string surname = "Korkmaz";
            string companyName = string.Empty;
            var createdAt = DateTime.Now;

            //Act
            var exception = Record.Exception(() => Act(id, name, surname, companyName, createdAt));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidCompanyNameException>();
        }

        [Fact]
        public void given_empty_created_at_contact_should_throw_an_exception()
        {
            // Arrange
            var id = new AggregateId();
            string name = "Ahmet";
            string surname = "Korkmaz";
            string companyName = "CITS";
            var createdAt = DateTime.MinValue;

            //Act
            var exception = Record.Exception(() => Act(id, name, surname, companyName, createdAt));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<InvalidCreatedAtException>();
        }

        [Fact]
        public void added_same_contact_info_should_throw_an_exception()
        {
            // Arrange
            var id = new AggregateId();
            string name = "Ahmet";
            string surname = "Korkmaz";
            string companyName = "CITS";
            var createdAt = DateTime.Now;

            string infoContent = "5453771435";

            var contact = Act(id, name, surname, companyName, createdAt);
            contact.AddContactInfo(new ContactInfo(infoContent, InfoType.PhoneNumber, DateTime.Now));

            //Act
            var exception = Record.Exception(() => contact.AddContactInfo(new ContactInfo(infoContent, InfoType.PhoneNumber, DateTime.Now)));

            //Assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ContactInfoAlreadyExistException>();
        }
    }
}
