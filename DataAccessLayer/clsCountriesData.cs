using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsCountriesData
    {
     

            public static bool GetCountryByID(int ID, ref string Country)
            {

                bool isFound = false;

                SqlConnection Connection = new SqlConnection(clsDataBaseAccess.Access);
                string Query = @"select Countries.* from Countries where CountryID = @CountryID";
                SqlCommand Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("CountryID", ID);

                try
                {
                    Connection.Open();
                    SqlDataReader Reader = Command.ExecuteReader();

                    if (Reader.Read())
                    {
                        isFound = true;

                        Country = (string)Reader["CountryName"];

                    }
                    else
                    {

                        isFound = false;

                    }
                    Reader.Close();


                }
                catch (Exception E)
                {

                    isFound = false;
                    // Save to log files

                }
                finally
                {
                    Connection.Close();
                }
                return isFound;

            }

            public static int AddNewCountry(string CountryName)
            {


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
                finally
                {
                    Connection.Close();

                }
                return CountryID;

            }

            public static bool UpdateCountry(int ID, string CountryName)
            {

                int RowsAffected = 0;

                SqlConnection Connection = new SqlConnection(clsDataBaseAccess.Access);
                string Query = @"update Countries set CountryName = @CountryName where CountryID = @CountryID ";
                SqlCommand Command = new SqlCommand(Query, Connection);
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
                finally
                {

                    Connection.Close();
                }
                return (RowsAffected > 0);


            }

            public static bool DeleteCountry(int ID)
            {

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
                finally
                {

                    Connection.Close();

                }
                return (RowsAffected > 0);

            }

            public static DataTable GetAllCountries()
            {


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
                catch (Exception E)
                {

                    // save to logs
                }
                finally
                {
                    Connection.Close();
                }

                return CountriesTable;

            }

            public static bool IsExist(int ID)
            {

                bool IsExist = false;
                SqlConnection Connection = new SqlConnection(clsDataBaseAccess.Access);
                string Query = @"select fount = 1 from Countries where CountryID = @CountryID";
                SqlCommand Command = new SqlCommand(Query, Connection);
                Command.Parameters.AddWithValue("@CountryID", ID);

                try
                {
                    Connection.Open();
                    SqlDataReader Reader = Command.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        IsExist = true;
                        Reader.Close();

                    }


                }
                catch (Exception E)
                {

                    IsExist = false;

                    //save to logs
                }
                finally
                {

                    Connection.Close();

                }

                return IsExist;

            }
        }
    
}
