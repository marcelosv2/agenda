using crud1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Npgsql;
using System.Data;
using System.Configuration;

namespace crud1.Dao
{
    public class EventoDao
    {

        public List<Evento> GetEventoList()
        {
            NpgsqlConnection con = null;
            List<Evento> eventoList = new List<Evento>();
            try
            {
                con = ConnectionFactory.getConnetion();
                string sql = " SELECT id, descricao, data_do_evento, observacao, categoriaId from eventos";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.CommandType = CommandType.Text;
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Evento e = new Evento();
                    Categoria c = new Categoria();
                    c.Id = (int)dr[4];
                    c.Nome = (c.Id == 1) ? "Pessoal": "Trabalho" ;
                    if (dr[0] != null) e.Id = (long)dr[0];
                    if (dr[1] != null) e.Descricao = dr[1].ToString();
                    if (dr[2] != null) e.Data = (DateTime)dr[2];
                    if (dr[3] != null) e.Observacao = dr[3].ToString();
                    if (dr[4] != null) e.Categoria = c;
                    eventoList.Add(e);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return eventoList;
        }


        public List<Evento> GetEventosByData (string data) {
            List<Evento> eventoList = new List<Evento>();
            NpgsqlConnection con = null;
            try
            {
                con = ConnectionFactory.getConnetion();
                string sql = " SELECT id, descricao, data_do_evento, observacao, categoriaId from eventos WHERE TO_CHAR(data_do_evento, 'YYYY-MM-DD') = @dataEvento";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new NpgsqlParameter("@dataEvento", data));
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Evento e = new Evento();
                    Categoria c = new Categoria();
                    c.Id = (int)dr[4];
                    c.Nome = (c.Id == 1) ? "Pessoal" : "Trabalho";
                    if (dr[0] != null) e.Id = (long)dr[0];
                    if (dr[1] != null) e.Descricao = dr[1].ToString();
                    if (dr[2] != null) e.Data = (DateTime)dr[2];
                    if (dr[3] != null) e.Observacao = dr[3].ToString();
                    if (dr[4] != null) e.Categoria = c;
                    eventoList.Add(e);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            return eventoList;
        }

        public Evento GetEventosById(long id)
        {
            Evento ev = new Evento();
            NpgsqlConnection con = null;
            try
            {
                con = ConnectionFactory.getConnetion();
                string sql = " SELECT id, descricao, data_do_evento, observacao, categoriaId from eventos WHERE id = @id";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new NpgsqlParameter("@id", id));
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Categoria c = new Categoria();
                    c.Id = (int)dr[4];
                    c.Nome = (c.Id == 1) ? "Pessoal" : "Trabalho";
                    if (dr[0] != null) ev.Id = (long)dr[0];
                    if (dr[1] != null) ev.Descricao = dr[1].ToString();
                    if (dr[2] != null) ev.Data = (DateTime)dr[2];
                    if (dr[3] != null) ev.Observacao = dr[3].ToString();
                    if (dr[4] != null) ev.Categoria = c;
                    
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            return ev;
        }

        public void insertEvento(Evento evento)
        {
            NpgsqlConnection con = null;
            try
            {
                con = ConnectionFactory.getConnetion();
                string sql = "insert into eventos (descricao, data_do_evento, observacao, categoriaId) values (@descricao, @data_do_evento, @observacao, @categoriaId) ";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new NpgsqlParameter("@descricao", evento.Descricao));
                command.Parameters.Add(new NpgsqlParameter("@data_do_evento", evento.Data));
                command.Parameters.Add(new NpgsqlParameter("@observacao", evento.Observacao));
                command.Parameters.Add(new NpgsqlParameter("@categoriaId", evento.Categoria.Id));

                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public void updateEvento(Evento evento)
        {
            NpgsqlConnection con = null;
            try
            {
                con = ConnectionFactory.getConnetion();
                string sql = "update eventos set descricao= @descricao, data_do_evento = @data_do_evento, observacao = @observacao, categoriaId = @categoriaId where id = @id";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new NpgsqlParameter("@descricao", evento.Descricao));
                command.Parameters.Add(new NpgsqlParameter("@data_do_evento", evento.Data));
                command.Parameters.Add(new NpgsqlParameter("@observacao", evento.Observacao));
                command.Parameters.Add(new NpgsqlParameter("@categoriaId", evento.Categoria.Id));
                command.Parameters.Add(new NpgsqlParameter("@id", evento.Id));

                command.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public void deleteEvento(long Id)
        {
            NpgsqlConnection con = null;
            try
            {
                con = ConnectionFactory.getConnetion();
                string sql = "delete from eventos where id = @id";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new NpgsqlParameter("@id", Id));

                command.ExecuteNonQuery();
                con.Close();
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