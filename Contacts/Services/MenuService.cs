using Contacts.Shared.Interfaces;
using Contacts.Shared.Models;
using Contacts.Shared.Services;
using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace Contacts.Services
{
    internal class MenuService
    {
        private static readonly IContactService _contactService = new ContactService();

        /// <summary>
        /// Shows the main menu with different options for the user.
        /// Uses a switch case for the options with default handling invalid options
        /// </summary>
        public static void ShowMainMenu()
        {
            while (true)
            {

                DisplayMenuTitle("MENU OPTIONS");
                Console.WriteLine($"{"1.",-3} Add New Contact");
                Console.WriteLine($"{"2.",-3} View Contact List");
                Console.WriteLine($"{"3.",-3} Show Contact Details");
                Console.WriteLine($"{"4.",-3} Delete Contact");
                Console.WriteLine($"{"0.",-3} Exit Application");
                Console.WriteLine();
                Console.Write("Enter Menu Option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        AddContactOption();
                        break;
                    case "2":
                        ShowAllContacts();
                        break;
                    case "3":
                        ShowContactDetail();
                        break;
                    case "4":
                        DeleteContact();
                        break;
                    case "0":
                        ExitApplicationOption();
                        break;
                    default:
                        Console.WriteLine("\nInvalid Option Selected. Please try again");
                        Console.ReadKey();
                        break;
                }
            }
        }


        /// <summary>
        /// Prompts the user to create a new contact 
        /// Saves the result into a variable that can be used to verify if the contact already exists
        /// </summary>
        public static void AddContactOption()
        {

            IContact contact = new Contact();

            DisplayMenuTitle("Add New Contact");

            Console.Write("Enter your first name: ");
            contact.FirstName = Console.ReadLine()!;

            Console.Write("Enter your last name: ");
            contact.LastName = Console.ReadLine()!;

            Console.Write("Enter your e-mail: ");
            contact.Email = Console.ReadLine()!;

            Console.Write("Enter your phone number: ");
            contact.PhoneNumber = Console.ReadLine()!;

            Console.Write("Enter your adress: ");
            contact.Address = Console.ReadLine()!;

            Console.Write("Enter what city you live in: ");
            contact.City = Console.ReadLine()!;

            
            var result = _contactService.AddContactToList(contact); 
            if (!result)
            {
                Console.Clear();
                Console.WriteLine("A contact with the same email already exists.");
                Console.WriteLine("The contact was not added to the list.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("The contact was added successfully.");
            }
            DisplayPressAnyKey();
        }

        /// <summary>
        /// If the list is not empty this function will loop through the list and print them out.
        /// </summary>
        private static void ShowAllContacts()
        {
            DisplayMenuTitle("Contact List");
            var contacts = _contactService.GetContactsFromList() ?? Enumerable.Empty<Contact>();
            {
                if (!contacts.Any())
                {

                    Console.WriteLine("No contacts found");
                }
                else
                {
                    foreach (var c in contacts)
                    {

                        Console.WriteLine("========================");
                        Console.WriteLine($"{c.FirstName} {c.LastName} \n<{c.Email}>");
                    }
                    Console.WriteLine("========================");
                }

            }

            DisplayPressAnyKey();
        }

        /// <summary>
        /// Shows a detailed information about a specific contact by entering the email
        /// </summary>
        private static void ShowContactDetail()
        {
            try
            {
                Console.Clear();
                DisplayMenuTitle("View contact details");

                
                Console.WriteLine("Enter the email of the contact you wish to view");
                var email = Console.ReadLine()!.ToUpper();

                var contact = _contactService.GetContactByEmail(email);
                if (contact != null)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("=========================");
                    Console.WriteLine($"{contact.FirstName} {contact.LastName}");
                    Console.WriteLine($"{contact.Email}");
                    Console.WriteLine($"{contact.PhoneNumber}");
                    Console.WriteLine($"{contact.Address}");
                    Console.WriteLine($"{contact.City}");
                    Console.WriteLine("=========================");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Contact not found");
                }
                DisplayPressAnyKey();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine("An error occurred while viewing the contact information");
                DisplayPressAnyKey();


            }
        }

        /// <summary>
        /// Deletes a contact from the list by entering the email
        /// </summary>
        private static void DeleteContact()
        {
            try
            {
                Console.Clear();
                DisplayMenuTitle("Delete a contact");

                Console.WriteLine("Enter the email of the contact you wish to delete");
                var email = Console.ReadLine()!.ToUpper();

                var contact = _contactService.GetContactByEmail(email);
                if (contact != null)
                {
                    Console.Clear();
                    Console.WriteLine("=========================");
                    Console.WriteLine($"{contact.FirstName} {contact.LastName}");
                    Console.WriteLine($"{contact.Email}");
                    Console.WriteLine($"{contact.PhoneNumber}");
                    Console.WriteLine($"{contact.Address}");
                    Console.WriteLine($"{contact.City}");
                    Console.WriteLine("=========================");
                    Console.Write("Are you sure you want to delete this contact? (y/n): ");
                    var response = Console.ReadLine()!.ToUpper();

                    if (response == "Y")
                    {
                        Console.Clear();
                        _contactService.RemoveContact(contact);
                        Console.WriteLine("Contact deleted Successfully");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Something went wrong");
                        Console.WriteLine("Contact was not deleted");
                    }
                    DisplayPressAnyKey();
                }
                else
                {
                    Console.WriteLine("Contact not found");
                    DisplayPressAnyKey();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine("An error occurred while deleting the contact");
                DisplayPressAnyKey();
            }
        }

        /// <summary>
        /// Exits application if user presses on y.
        /// </summary>
        private static void ExitApplicationOption()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to exit? (y/n): ");
            var option = Console.ReadLine() ?? "";

            if (option.Equals("y", StringComparison.CurrentCultureIgnoreCase))
            {
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Displays the menutitle.
        /// </summary>
        /// <param name="title"> string </param>
        private static void DisplayMenuTitle(string title)
        {
            Console.Clear();
            Console.WriteLine($"### {title} ###");
            Console.WriteLine();
        }

        /// <summary>
        /// Displays the Press any key prompt
        /// </summary>
        private static void DisplayPressAnyKey()
        {
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
