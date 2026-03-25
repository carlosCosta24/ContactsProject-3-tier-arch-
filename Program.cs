using System;
using System.Data;
using BusinessLayer;
namespace ContactsProject
{
    internal class Program
    {
        static void ContactCard(clsContact ContactInfo) {

            Console.WriteLine("=================================");
            Console.WriteLine($" ContactID: {ContactInfo.ID}");
            Console.WriteLine($" FirstName: {ContactInfo.FirstName}");
            Console.WriteLine($" LastName: {ContactInfo.LastName}");
            Console.WriteLine($" Email: {ContactInfo.Email}");
            Console.WriteLine($" Phone: {ContactInfo.Phone}");
            Console.WriteLine($" Address: {ContactInfo.Address}");
            Console.WriteLine($" Date Of Birth: {ContactInfo.DateOfBirth}");
            Console.WriteLine($" CountryID: {ContactInfo.CountryID}");
            Console.WriteLine($" ImagePath: {(ContactInfo.ImagePath == null ? "Empty" : ContactInfo.ImagePath)}");

            Console.WriteLine("=================================");


        }
        static void TestFindContact(int ID) {

            clsContact Contact = clsContact.Find(ID);

            if (Contact != null)
            {
                ContactCard(Contact);
            }
            else {
                Console.WriteLine("Contact [" + ID + "] doesn't exist");
            }
            
        
        }

        static void Main(string[] args)
        {
            TestFindContact(7);
            //TestAddContact();
            //TestUpdateContact();
            //TestListContact();
            Console.ReadKey();

        }
    }
}
