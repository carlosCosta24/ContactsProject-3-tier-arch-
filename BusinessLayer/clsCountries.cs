using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer
{
    public class clsCountries
    {
       

            public enum enMode { AddNew = 0, Update = 1 };
            enMode Mode = enMode.AddNew;
            public int ID { get; set; }
            public string CountryName { get; set; }


            public clsCountries()
            {

                this.ID = -1;
                this.CountryName = "";
                Mode = enMode.AddNew;

            }

            private clsCountries(int ID, string CountryName)
            {

                this.ID = ID;
                this.CountryName = CountryName;
                Mode = enMode.Update;
            }
            public static clsCountries FindCountryByID(int ID)
            {

                string CountryName = "";
                int CountryId = -1;

                if (clsCountriesData.GetCountryByID(ID, ref CountryName))
                {

                    return new clsCountries(ID, CountryName);
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
