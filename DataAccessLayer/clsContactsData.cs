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

            try
            {
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

                    ImagePath = Reader["ImagePath"] == DBNull.Value ? null : (string)Reader["ImagePath"];
                    // handel null values in db
                    /*
                     * if(Reader["ImagePath"] != BBNull.value){
                     *  ImagePath = (string)Reader["ImagePath"];
                     * }else{
                     *  ImagePath = "";
                     * }
                    */


                }
                else
                {
                    IsFound = false;
                }
                Reader.Close();

            }
            catch (Exception E)
            {
                IsFound = false;
            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }



        public static int AddNewContact(string FirstName, string LastName, string Email, string Phone, string Address,
             int CountryID, DateTime DateOfBirth, string ImagePath)
        {

            int ContactID = -1;

            SqlConnection Connection = new SqlConnection(clsDataBaseAccess.Access);
            string Query = @"insert into Contacts (FirstName,LastName,Email,Phone,Address,DateOfBirth
           ,CountryID,ImagePath) values (@FirstName,@LastName,@Email,@Phone,@Address,@DateOfBirth
           ,@CountryID,@ImagePath) select scope_identity()";


            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@Email", Email);
            Command.Parameters.AddWithValue("@Phone", Phone);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@CountryID", CountryID);
            if (ImagePath != "")
            {

                Command.Parameters.AddWithValue("@FirstName", FirstName);
            }
            else
            {
                Command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }

            try
            {
                Connection.Open();

                Object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertID))
                {

                    ContactID = InsertID;
                }

            }
            catch (Exception E)
            {
                // save to log file 
            }
            finally
            {
                Connection.Close();
            }

            return ContactID;
        }

    public static bool UpdateContact(int Id, string FirstName, string LastName,
             string Email, string Phone, string Address, DateTime DateOfBirth, int CountryId, string ImagePath) {

            int RowsAffected = 0;
            SqlConnection Connection = new SqlConnection(clsDataBaseAccess.Access);

            string Query = @"update Contacts set 
            FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email,
            Phone = @Phone,
            Address = @Address,
            DateOfBirth = @DateOfBirth,
            CountryID = @CountryID,
            ImagePath = @ImagePath
            where ContactID = @ContactID";

            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ContactID", Id);
            Command.Parameters.AddWithValue("@FirstName", FirstName);
            Command.Parameters.AddWithValue("@LastName", LastName);
            Command.Parameters.AddWithValue("@Email", Email);
            Command.Parameters.AddWithValue("@Phone", Phone);
            Command.Parameters.AddWithValue("@Address", Address);
            Command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            Command.Parameters.AddWithValue("@CountryID", CountryId);
            if (!string.IsNullOrWhiteSpace(ImagePath))
            {

                Command.Parameters.AddWithValue("@ImagePath", ImagePath);
            }
            else {
                Command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }



            try {
                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception E) {
                //Console.WriteLine("Error: " + E.Message);
                return false;
            }
            finally {
                Connection.Close();
            }

            return (RowsAffected > 0);



        }
    } 
}