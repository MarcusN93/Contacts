using Contacts.Shared.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Contacts.Shared.Services
{
    public class ContactService : IContactService

    {

        private readonly IFileService _fileService = new FileService();
        private List<IContact> _contactList = new List<IContact>();
        private readonly string _filePath = @"C:\CSharp-Projects\Contacts\Contacts\Contacts.json";

        /// <summary>
        /// This function verifies that the contact does not already exist by comparing email adresses before adding the contact to the list.
        /// </summary>
        /// <param name="contact"> IContact object </param>
        /// <returns> returns true if contact was added, else returns false </returns>
        public bool AddContactToList(IContact contact)
        {
            try
            {
                if (!_contactList.Any(x => x.Email == contact.Email))
                {
                    GetContactsFromList();
                    _contactList.Add(contact);

                    string json = JsonConvert.SerializeObject(_contactList, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });

                    var result = _fileService.SaveContentToFile(_filePath, json);
                    return result;
                }
            }
            catch (Exception ex) { Debug.WriteLine("ContactService - AddContactToList:: " + ex.Message); }
            return false;
        }

        /// <summary>
        /// This function gets a contact by comparing email adress and returns the contact
        /// </summary>
        /// <param name="email"> string </param>
        /// <returns> IContact object </returns>
        public IContact GetContactByEmail(string email)
        {
            try
            {
                GetContactsFromList(); 

                var contact = _contactList.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                if (contact != null)
                {
                    return contact;
                }
                else
                {
                    return null!;
                }
            }
            catch (Exception ex) { Debug.WriteLine("ContactService - GetContactByEmail:: " + ex.Message); }
            return null!;
        }

        /// <summary>
        /// Gets the contactlist, checks if the list is empty
        /// </summary>
        /// <returns> If the list is not empty it returns the list </returns>
        public IEnumerable<IContact> GetContactsFromList()
        {
            try
            {
                var content = _fileService.GetContentFromFile(_filePath);
                if (!string.IsNullOrEmpty(content))
                {
                    _contactList = JsonConvert.DeserializeObject<List<IContact>>(content, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All })!;
                    return _contactList;
                }
            }
            catch (Exception ex) { Debug.WriteLine("ContactService - GetContactsFromList:: " + ex.Message); }
            return null!;
        }

        /// <summary>
        /// find the index in the list that the contact's email is located and then remove the contact located at that index
        /// </summary>
        /// <param name="contact"> IContact object </param>
        /// <returns> returns true if remove was successful, else returns false </returns>
        public bool RemoveContact(IContact contact)
        {
            try
            {
                var content = _fileService.GetContentFromFile(_filePath);
                if (!string.IsNullOrEmpty(content))
                {
                    GetContactsFromList();
                    int index = _contactList.FindIndex(x => x.Email.Equals(contact.Email, StringComparison.OrdinalIgnoreCase));
                    _contactList.RemoveAt(index);
                    string json = JsonConvert.SerializeObject(_contactList, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                    var result = _fileService.SaveContentToFile(_filePath, json);
                    return result;
                }
            }
            catch (Exception ex) { Debug.WriteLine("ContactService - GetContactsFromList:: " + ex.Message); }
            return false;
        }
    }
}
