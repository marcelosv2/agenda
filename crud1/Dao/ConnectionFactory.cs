using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Configuration;

namespace crud1.Dao
{
    public class ConnectionFactory
    {
        public static NpgsqlConnection Connection;

        public static NpgsqlConnection getConnetion() {
            try {
                if (Connection == null)
                {
                    Connection = new NpgsqlConnection();
                    Connection.ConnectionString = ConfigurationManager.ConnectionStrings["conexao"].ToString();
                }
            } catch (Exception e) {
                throw e;
            }
            Connection.Open();
            return Connection;
        }
    }


}