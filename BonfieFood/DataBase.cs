using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BonfieFood
{
    public class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=Tema\SQLEXPRESS01;Initial Catalog=BonfieFood;Integrated Security=True");

        // відкрити зв'язок з БД
        public void openConnection()
        {
            if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }

        // закрити зв'язок з БД
        public void closeConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
        }

        // метод getConnection повертає рядок
        public SqlConnection getConnection()
        {
            return sqlConnection;
        }
    }
}