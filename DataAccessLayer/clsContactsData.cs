using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class clsContactsData
    {
        public static bool GetContactById(int Id, ref string FirstName, ref string LastName, 
            ref string Email, ref string Phone, ref string Address, ref DateTime DateOfBirth, ref int CountryId, ref string ImagePath) 
        {
            bool IsFound = false;

            SqlConnection Connection = new SqlConnection(clsDataBaseAccess.Access);

            string Query = "select * from Contacts where ContactID = @ContactID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ContactID", Id);

            try {
                Connection.Open();

                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {

                    IsFound = true;

                    FirstName = (string)Reader["FirstName"];
                    LastName = (string)Reader["LastName"];
                    Email = (string)Reader["Email"];
                    Phone = (string)Reader["Phone"];
                    Address = (string)Reader["Address"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    CountryId = (int)Reader["CountryID"];
                    ImagePath = Reader["ImagePath"] == DBNull.Value ?  null : (string)Reader["ImagePath"];




                }
                else {
                    IsFound = false;
                }
                Reader.Close();

            } 
            catch (Exception E) 
            { 
                IsFound =false;
            } 
            finally {
                Connection.Close();   
            }
            return IsFound;
        }
    }
}
