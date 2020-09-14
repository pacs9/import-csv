using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace importar_motoristas.Data
{
    public class ADOContext : IDisposable
    {
        SqlConnection Connection;

        private SqlConnection CreateConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionMotoristas"].ConnectionString;
            Connection = new SqlConnection(connectionString);            

            return Connection;
        }        

        public SqlConnection Open()
        {
            try
            {
                Connection = CreateConnection();

                Connection.Open();

                return Connection;
            }
            catch (Exception ex)
            {
                Connection.Close();
                Connection.Dispose();

                return null;
            }
        }

        public void Dispose()
        {
            if (Connection != null && Connection.State != System.Data.ConnectionState.Closed)
            {
                Connection.Close();
                Connection.Dispose();
            }
        }
    }
}
