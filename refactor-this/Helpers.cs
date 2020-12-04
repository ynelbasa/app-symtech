using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace refactor_this
{
    public class Helpers
    {
        private const string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\refactor-this.mdf;Integrated Security=True;Connect Timeout=30";

        public static SqlConnection NewConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}