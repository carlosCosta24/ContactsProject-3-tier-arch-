using DataAccessLayer;
using System.Data;

namespace BusinessLayer
{
    public class clsCountries
    {


        public enum enMode { AddNew = 0, Update = 1 };
        enMode Mode = enMode.AddNew;
        public int ID { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CountryPhoneCode { get; set; }



        public clsCountries()
        {

            this.ID = -1;
            this.CountryName = "";
            this.CountryCode = "";
            this.CountryPhoneCode = "";
            Mode = enMode.AddNew;

        }

        private clsCountries(int ID, string CountryName, string Code, string PhoneCode)
        {

            this.ID = ID;
            this.CountryName = CountryName;
            this.CountryCode = Code;
            this.CountryPhoneCode = PhoneCode;
            Mode = enMode.Update;
        }
        public static clsCountries FindCountry(int ID)
        {

            int CountryID = -1;
            string CountryName = "";
            string CountryCode = "";
            string PhoneCode = "";

            if (clsCountriesData.FindCountry(ID, ref CountryName, ref CountryCode, ref PhoneCode))
            {

                return new clsCountries(ID, CountryName, CountryCode, PhoneCode);
            }
            else
            {
                return null;
            }

        }
        public static clsCountries FindCountry(string Name)
        {
            int CountryID = -1;
            string Code = "";
            string PhoneCode = "";

            if (clsCountriesData.FindCountry(ref CountryID, Name, ref Code, ref PhoneCode))
            {

                return new clsCountries(CountryID, Name, Code, PhoneCode);
            }
            else
            {
                return null;
            }

        }

        public bool Save()
        {

            switch (Mode)
            {

                case enMode.AddNew:
                    if (_AddNewCountry())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateCountry();

            }
            return false;



        }

        private bool _AddNewCountry()
        {


            this.ID = clsCountriesData.AddNewCountry(this.CountryName);
            return (this.ID != -1);

        }

        private bool _UpdateCountry()
        {

            return clsCountriesData.UpdateCountry(this.ID, this.CountryName);

        }

        public static bool DeleteCountry(int ID)
        {

            return clsCountriesData.DeleteCountry(ID);
        }
        public static DataTable GetAllCountries()
        {


            return clsCountriesData.GetAllCountries();

        }

        public static bool IsExist(int ID)
        {

            return clsCountriesData.IsExist(ID);


        }


    }
}
