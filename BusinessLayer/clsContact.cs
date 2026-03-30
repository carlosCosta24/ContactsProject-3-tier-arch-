using DataAccessLayer;
using System;
using System.Data;


namespace BusinessLayer
{
    public class clsContact
    {
        public enum enMode { AddNew = 0, Update = 1};
        public enMode Mode = enMode.AddNew;
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
            Mode = enMode.AddNew;

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
            Mode = enMode.Update;

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
        private bool _AddNewContact()
        {

            this.ID = clsContactsData.AddNewContact(this.FirstName, this.LastName,
                this.Email, this.Phone, this.Address, this.CountryID, this.DateOfBirth, this.ImagePath);
            return (this.ID != -1);

        }

        private bool _UpdateContact() {

            return clsContactsData.UpdateContact(this.ID, this.FirstName, this.LastName,
                this.Email, this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
        
        }
        public bool Save()
        {

            switch (Mode)
            {

                case enMode.AddNew:
                    if (_AddNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                 case enMode.Update:
                    return _UpdateContact();

             }

                 return false;
            }
        public static bool DeleteContact(int ID) {

            return clsContactsData.DeleteContact(ID);
        

        }
        public static DataTable GetAllContacts()
        {

            return clsContactsData.GetAllContacts();
        }
        public static bool IsContactExist(int ID)
        {

            return clsContactsData.IsContactExist(ID);
        }

    }
    
}
