using Contacts.Shared.Interfaces;
using Contacts.Shared.Models;
using Contacts.Shared.Services;

namespace Contacts.Test
{
    public class CustomerService_Tests
    {
        [Fact]
        public void AddToList_Should_AddContactToContactList_ThenReturnTrue()
        {
            //Arrange
            IContact contact = new Contact { FirstName = "John", LastName = "Smith", Email = "john@domain.com", PhoneNumber = "0701234567", Address = "Examplestreet 1", City = "Example City" };
            IContactService contactService = new ContactService();

            //Act
            bool result = contactService.AddContactToList(contact);

            //Assert
            Assert.True(result);
        }
    }
}
