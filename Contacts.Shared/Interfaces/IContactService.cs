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

        /// <summary>
        /// Gets the contactlist, checks if the list is empty
        /// </summary>
        /// <returns> If the list is not empty it returns the list </returns>
        IEnumerable<IContact> GetContactsFromList();

        /// <summary>
        /// This function gets a contact by comparing email adress and returns the contact
        /// </summary>
        /// <param name="email"> string </param>
        /// <returns> IContact object </returns>
        IContact GetContactByEmail(string email);

        /// <summary>
        /// find the index in the list that the contact's email is located and then remove the contact located at that index
        /// </summary>
        /// <param name="contact"> IContact object </param>
        /// <returns> returns true if remove was successful, else returns false </returns>
        bool RemoveContact(IContact contact);
    }
}
