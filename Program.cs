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

        static void CountryCard(clsCountries Country)
        {

            Console.WriteLine("=================================");
            Console.WriteLine($" ContactID: {Country.ID}");
            Console.WriteLine($" FirstName: {Country.CountryName}");
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

        static void TestAddContact() { 
            clsContact Carlos = new clsContact();

            Carlos.FirstName = "Carlos";
            Carlos.LastName = "Costa";
            Carlos.Email = "CarlosCosta@gmail.com";
            Carlos.Phone = "1234567890";
            Carlos.Address = "dkjfsal";
            Carlos.DateOfBirth = new DateTime(1999, 8, 8);
            Carlos.CountryID = 5;
            Carlos.ImagePath = "";

            if (Carlos.Save()) { 
                Console.WriteLine("Contact hase been saved successfulluy ID = " + Carlos.ID);

            }
        }
        static void TestUpdateContact(int ID) {

            clsContact ContactToUpdate = clsContact.Find(ID);
            if (ContactToUpdate != null)
            {

                ContactToUpdate.FirstName = "Ricardo";
                ContactToUpdate.LastName = "Costa";
                ContactToUpdate.Email = "RicardoCosta@gmail.com";
                ContactToUpdate.Phone = "1234567890";
                ContactToUpdate.Address = "Aracaju";
                ContactToUpdate.DateOfBirth = new DateTime(1988, 8, 8);
                ContactToUpdate.CountryID = 5;
                ContactToUpdate.ImagePath = "";

                if (ContactToUpdate.Save())
                {
                    Console.WriteLine("Contact Updated successfully");
                    ContactCard(ContactToUpdate);
                }
            }
            else {
                Console.WriteLine("Not Found");
            }
        }

        static void TestDeleteContact(int ID){

            if (clsContact.IsContactExist(ID))
            {

                if (clsContact.DeleteContact(ID))
                {

                    Console.WriteLine("Contact deleted successfully :-) ");
                }
                else
                {
                    Console.WriteLine("Contact not deleted :-( ");
                }

            }
            else {
                Console.WriteLine("Contact doesn't exist");
            }

        
        }

        static void TestListContact() {
            DataTable DT = clsContact.GetAllContacts();
            Console.WriteLine("Contacts data: ");

                Console.WriteLine("FirstName | LastName | LastName");
            foreach (DataRow Row in DT.Rows) 
            {
                

                Console.WriteLine($"\t{Row["ContactID"]} | {Row["FirstName"]} | {Row["LastName"]}");
            }
            

        
        }
        static void TestIsContactExist(int ID) {

            if (clsContact.IsContactExist(ID))
            {

                Console.WriteLine($"Contact with ID: {ID} exist :-)");
            }
            else
            {
                Console.WriteLine("Contact doesn't exist");
            }
        }
        //Country test
        static void TestFindCountry(int ID) {
            clsCountries Country = clsCountries.FindCountryByID(ID);

            if (Country != null)
            {
                CountryCard(Country);
            }
            else {
                Console.WriteLine($"Country with ID: {ID}, doesn't exist");
            }

        }
        //Find by name

        static void TestAddCountry() {
            clsCountries Country = new clsCountries();
            Country.ID = 6;
            Country.CountryName = "Brazil";

            if (Country.Save()) {

                Console.WriteLine($"Country has been Add successfully: with ID: {Country.ID}");
            
            }
        }
        static void TestUpdateCountry(int ID) {
        
            clsCountries Country = clsCountries.FindCountryByID(ID);
            if (Country != null)
            {

                Country.CountryName = "USA";

                if (Country.Save())
                {

                    Console.WriteLine("Country updated successfully");
                    CountryCard(Country);

                }
            }
            else {

                Console.WriteLine("Country is not Found");
            }
        
        }
        static void TestDeleteCountry(int ID) {


            if (clsCountries.IsExist(ID))
            {
                if (clsCountries.DeleteCountry(ID))
                {

                    Console.WriteLine("Country has been deleted successfully:-)");
                }
                else
                {
                    Console.WriteLine("Country hasn't been deleted :-(");

                }

            }
            else
            {
                
                Console.WriteLine("Country doesn't exist");
            }



        }

        static void TestListCountry() {

            DataTable CountriesTable = clsCountries.GetAllCountries();

            Console.WriteLine("CountryID , CountryName");
            foreach (DataRow Row in CountriesTable.Rows) 
            {

                Console.WriteLine($"\t{Row["CountryID"]} ,  {Row["CountryName"]}");
            }
        
        
        
        }

        static void TestIsCountryExist(int ID) {

            if (clsCountries.IsExist(ID))
            {

                Console.WriteLine($"Country with ID: {ID} exist");
            }
            else { 
                Console.WriteLine("Country doesn't exist");
            }
        
        
        }
        //Exist by name

        static void Main(string[] args)
        {
            //TestFindContact(7);
            //TestAddContact();
            //TestUpdateContact(1);
            //TestDeleteContact(5);
            //TestListContact();
            //TestIsContactExist(80);
            //TestFindCountry(2);
            //TestAddCountry();
            //TestUpdateCountry(1);
            //TestDeleteCountry(5);
            TestListCountry();
            //TestIsCountryExist(6);
            Console.ReadKey();

        }

        
    }
}
