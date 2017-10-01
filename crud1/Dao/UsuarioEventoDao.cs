using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace crud1.Dao
{
    public class UsuarioEventoDao
    {
        public void insertUsuarioEvento(long userId, long eventoId)
        {
            NpgsqlConnection con = null;
            try
            {
                con = ConnectionFactory.getConnetion();
                string sql = "insert into usuarios_eventos (usuarioid, eventoid) values (@usuarioid, @eventoId) ";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new NpgsqlParameter("@usuarioid", userId));
                command.Parameters.Add(new NpgsqlParameter("@eventoId", eventoId));
                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
    }
}