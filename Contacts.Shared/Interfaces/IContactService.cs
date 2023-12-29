namespace Contacts.Shared.Interfaces
{
    public interface IContactService
    {
        /// <summary>
        /// Add a contact to a contact list
        /// </summary>
        /// <param name="contact"> a contact of type IContact </param>
        /// <returns> returns true if successful, or false if it fails or contact already exists </returns>
        bool AddContactToList(IContact contact);
        IEnumerable<IContact> GetContactsFromList();
        IContact GetContactByEmail(string email);
        bool RemoveContact(IContact contact);
    }
}
