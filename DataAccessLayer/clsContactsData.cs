using System;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

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
             string Email, string Phone, string Address, DateTime DateOfBirth, int CountryId, string ImagePath)
        {

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
            else
            {
                Command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            }



            try
            {
                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();
            }
            catch (Exception E)
            {
                //Console.WriteLine("Error: " + E.Message);
                return false;
            }
            finally
            {
                Connection.Close();
            }

            return (RowsAffected > 0);



        }


        public static bool DeleteContact(int ID) {
            
            int RowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataBaseAccess.Access);

            string Query = @"delete from Contacts where ContactID = @ContactID";

            SqlCommand Command = new SqlCommand(Query, connection);

            Command.Parameters.AddWithValue("@ContactID", ID);


            try
            {

                connection.Open();

                RowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception E)
            {

                return false;
            }
            finally { 
                connection.Close();
            }

            return (RowsAffected > 0);
        
        
        }

        public static DataTable GetAllContacts() { 
        
            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(clsDataBaseAccess.Access);

            string Query = @"select * from Contacts";
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    dt.Load(Reader);
                }
                Reader.Close();
                Connection.Close();
            }
            catch (Exception E)
            {

                // save to logs 
            }
            finally { 
                Connection.Close(); 
            }
            return dt;
        }

        public static bool IsContactExist(int ID) { 
        
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataBaseAccess.Access);
            string Query = @"select found = 1 from Contacts where ContactID = @ContactID";
            SqlCommand Command = new SqlCommand(Query, Connection);

            Command.Parameters.AddWithValue("@ContactID", ID);

            try
            {

                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.HasRows)
                {
                    isFound = true;
                    Connection.Close();
                }
                else
                {
                    isFound = false;

                }

            }
            catch (Exception E)
            {

                isFound = false;
            }
            finally { 
                
                Connection.Close();

            
            }
            return isFound;
        
        
        }
    }
    public class clsCountryData {

        public static bool GetCountryByID(int ID, ref string Country) {

            bool isFound = false;

            SqlConnection Connection = new SqlConnection(clsDataBaseAccess.Access);
            string Query = @"select Countries.* from Countries where CountryID = @CountryID";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("CountryID",ID);

            try {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();

                if (Reader.Read())
                {
                    isFound = true;

                    Country = (string)Reader["CountryName"];

                }
                else { 
                
                    isFound = false;

                }
                Connection.Close();

            } catch(Exception E) {
                
                isFound = false;
                // Save to log files
            
            } finally {
                Connection.Close();
            }
            return isFound;

        }

        public static int AddNewCountry(string CountryName) {


            int CountryID = -1;

            SqlConnection Connection = new SqlConnection(clsDataBaseAccess.Access);

            string Query = @"insert into Countries (CountryName) values (@CountryName) select scope_identity()";

            SqlCommand Command = new SqlCommand(Query, Connection);

            
            Command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {

                Connection.Open();

                object Result = Command.ExecuteScalar();

                if (Result != null && int.TryParse(Result.ToString(), out int InsertedID))
                {

                    CountryID = InsertedID;

                }

            }
            catch (Exception E)
            {

                // log to logs


            }
            finally {
                Connection.Close();
            
            }
            return CountryID;

        }

        public static bool UpdateCountry(int ID, string CountryName) { 
        
            int RowsAffected = 0;

            SqlConnection Connection = new SqlConnection(clsDataBaseAccess.Access);
            string Query = @"update Countries set CountryName = @CountryName where CountryID = @CountryID ";
            SqlCommand Command = new SqlCommand(Query,Connection);
            Command.Parameters.AddWithValue("@CountryID", ID);
            Command.Parameters.AddWithValue("@CountryName", CountryName);


            try
            {

                Connection.Open();

                RowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception E)
            {

                return false;
            }
            finally { 
                
                Connection.Close();
            }
            return (RowsAffected > 0);

        
        }

        public static bool DeleteCountry(int ID) {

            int RowsAffected = 0;
            SqlConnection Connection = new SqlConnection(clsDataBaseAccess.Access);
            string Query = @"delete from Countries where CountryID = @CountryID";
            SqlCommand Command = new SqlCommand(Query, Connection);
            Command.Parameters.AddWithValue("@CountryID", ID);

            try
            {

                Connection.Open();
                RowsAffected = Command.ExecuteNonQuery();

            }
            catch (Exception E)
            {
                // save to logs
                return false;
            }
            finally { 
                
                Connection.Close();
            
            }
            return (RowsAffected > 0);
        
        }

        public static DataTable GetAllCountries() { 
        
        
            DataTable CountriesTable = new DataTable();
            SqlConnection Connection = new SqlConnection(clsDataBaseAccess.Access);
            string Query = @"select * from Countries";
            SqlCommand Command = new SqlCommand(Query, Connection);

            try
            {

                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    CountriesTable.Load(Reader);
                }
                Reader.Close();
                Connection.Close();

            }
            catch (Exception E) {

                // save to logs
            }
            finally
            {
                Connection.Close();
            }

            return CountriesTable;
        
        }
    }
}