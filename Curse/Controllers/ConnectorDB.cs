using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace Curse
{
    public static class ConnectorDB
    {
        private static readonly string connectSetting = "Server=127.0.0.1;Database=Ukrbank;port=3306;User Id=root;password=Mk134476";


        public static string QueryForString(string SQLCommand)
        {
            string linquiryStr = "";
            MySqlConnection Connection = new MySqlConnection(connectSetting);
            try
            {
                Connection.Open();
                MySqlCommand command = new MySqlCommand(SQLCommand, Connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    linquiryStr = reader[0].ToString();
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Error: \r\n{0}" + ex.ToString(), "Error");
            }
            finally
            {
                Connection.Close();
            }
            return linquiryStr;
        }

        public static List<string> QueryForList(string SQLCommand)
        {
            List<string> returnList = new List<string>();
            MySqlConnection Connection = new MySqlConnection(connectSetting);
            try
            {
                Connection.Open();
                MySqlCommand command = new MySqlCommand(SQLCommand, Connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // здесь 0 - это индекс столбца, который вы хотите добавить в список
                    returnList.Add(reader.GetString(0));
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Error: \r\n{0}" + ex.ToString(), "Error");
            }
            finally
            {
                Connection.Close();
            }
            return returnList;
        }

        public static string QueryForStringList(string SQLCommand)
        {
            string returnLine = "";

            foreach (string str in QueryForList(SQLCommand))
            {
                returnLine += $"{str}\n";
            }

            return returnLine;
        }

        public static string AddInDataBase(string SQLCommand)
        {
            string TempString = "";
            MySqlConnection connection = new MySqlConnection(connectSetting);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(SQLCommand, connection);
                command.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: \r\n{0}" + ex.ToString(), "Error");
            }
            finally
            {
                connection.Close();
            }
            return TempString;
        }

        public static DataTable QueryForDataTable(string SQLCommand)
        {
            DataTable linquiryArray = new DataTable();                               //строка массива
            MySqlConnection Connection = new MySqlConnection(connectSetting);
            try
            {
                //Connection.ConnectionString = mysqlCSB.ConnectionString;
                MySqlCommand command = new MySqlCommand(SQLCommand, Connection);
                Connection.Open();
                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        linquiryArray.Load(dr);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.WriteLine("Error: \r\n{0}" + ex.ToString(), "Error");
            }
            finally
            {
                Connection.Close();
            }
            return linquiryArray;
        }
    }
}
