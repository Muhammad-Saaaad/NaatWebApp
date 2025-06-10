using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Runtime.Remoting.Services;
using System.Data;

using System.Configuration; // add this to import connnection string from configuration

namespace NaatWebApp.Models
{
	public class DBAccess
	{
        // This configuration manager represent the configuration tag in web.config

        // constring is the name attribute from add tag
		// ConnectionString is the attribute from add tag

        static string connstr = ConfigurationManager.ConnectionStrings["constring"].ConnectionString;
		public SqlConnection conn = new SqlConnection(connstr);
		public SqlCommand cmd = null;
		public SqlDataReader sdr = null;
		public SqlDataAdapter adapter = null;


		public void OpenConnection()
		{
			if(conn.State == ConnectionState.Closed)
			{
				conn.Open();
			}
		}
        public void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }
		public void InsertUpdateDelete(string query)
		{
			cmd = new SqlCommand(query , conn);
			cmd.ExecuteNonQuery();
		}
		public SqlDataReader GetData(string  query)
		{
			cmd = new SqlCommand(query, conn);
			sdr = cmd.ExecuteReader();
			return sdr;
		}

    }
}