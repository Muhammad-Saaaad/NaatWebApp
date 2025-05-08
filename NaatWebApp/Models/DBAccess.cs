using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Runtime.Remoting.Services;
using System.Data;

namespace NaatWebApp.Models
{
	public class DBAccess
	{
		static string connstr = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=CMSdb;Data Source=SAAD\\VE_SERVER";
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