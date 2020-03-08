using System;
using System.Data.SqlClient;

namespace websocket
{
    class DataBaseConnector
    {
        static string User = Environment.GetEnvironmentVariable("USER");
        static string Password = Environment.GetEnvironmentVariable("PASSWORD");
        static string Host = Environment.GetEnvironmentVariable("HOST");
        static string Database = Environment.GetEnvironmentVariable("DATABASE");

        SqlConnection myConnection = new SqlConnection("user id=" + User + ";" +
                                       "password=" + Password + ";server=" + Host + ";" +
                                       "Trusted_Connection=yes;" +
                                       "database=" + Database + "; " +
                                       "connection timeout=30");

        /// <summary>
        /// do the actual connection
        /// </summary>
        public DataBaseConnector()
        {
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public int getNumOfRows()
        {
            string sql = "SELECT COUNT(*) FROM dbo.Customers";
            SqlCommand cmd = new SqlCommand(sql, myConnection);
            int count = (int)cmd.ExecuteScalar();
            return count;
        }

        /// <summary>
        /// select some stuff
        /// </summary>
        /// <returns></returns>
        public string demoSelect()
        {
            string cleanedStr = "";
            string demoStr = "[";
            int numRows = getNumOfRows();
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand(
                    "select * from dbo.Customers",
                    myConnection
                );
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    demoStr += "{";
                    Object[] values = new Object[myReader.FieldCount];
                    int fieldCount = myReader.GetValues(values);
                    for (int i = 0; i < fieldCount; i++)
                    {
                        demoStr += "\"" + myReader.GetName(i) + "\": \"" + myReader[i] + "\"";
                        if (i < fieldCount - 1)
                        {
                            demoStr += ",";
                        }
                    }
                    if (numRows > 1)
                    {
                        demoStr += "},";
                    }
                    else
                    {
                        demoStr += "}";
                    }
                    numRows--;
                }
                myReader.Close();
                demoStr += "]";
                cleanedStr = demoStr.Replace(" ", String.Empty).Replace("\n", String.Empty).Replace("\t", String.Empty).Replace("\r", String.Empty);
                Console.WriteLine(cleanedStr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return  cleanedStr;
        }
   }
}
