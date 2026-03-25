using System;
using System.Data;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsContact
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
        public string Phone { get; set; }   
        public string Address { get; set; } 
        public DateTime DateOfBirth { get; set; }   
        public string ImagePath { get; set; }
        public int CountryID { get; set; }

        public clsContact() { 
            
            this.ID = -1;
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Address = "";
            this.DateOfBirth = DateTime.Now;
            this.CountryID = -1;
            this.ImagePath = "";

        }

        private clsContact(int ID, string FirstName, string LastNmae, 
            string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastNmae;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;

        }

        public static clsContact Find(int ID) {

            string FirstName = "",
            LastName = "",
            Email = "",
            Phone = "",
            Address = "",
            ImagePath = "";
            int CountryID = -1;

            DateTime  DateOfBirth = DateTime.Now;

            if (clsContactsData.GetContactById(ID, ref FirstName, ref LastName, ref Email,
                ref Phone, ref Address, ref DateOfBirth, ref CountryID, ref ImagePath))
            {

                return new clsContact(ID, FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath);

            }
            else { 
                return null;    
            }


        
        }


    }
}
