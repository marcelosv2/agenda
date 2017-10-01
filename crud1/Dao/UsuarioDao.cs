using crud1.Models;
using crud1.Util;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace crud1.Dao
{
    public class UsuarioDao
    {

        public Usuario getUsuerByLoginSenha(string login, string senha) {
            NpgsqlConnection con = new NpgsqlConnection();
            Usuario ret;
            try
            {
                con = ConnectionFactory.getConnetion();
                NpgsqlCommand command = new NpgsqlCommand("SELECT id, nome, email, password from usuarios where email = @email and password = @password", con);
                command.Parameters.Add(new NpgsqlParameter("@email", login));
                command.Parameters.Add(new NpgsqlParameter("@password", senha));
                NpgsqlDataReader dr = command.ExecuteReader();
                ret = new Usuario();
                while (dr.Read())
                {
                    ret.Id = (long)dr[0];
                    if (dr[1] != null) ret.Nome = dr[1].ToString();
                    if (dr[2] != null) ret.Email = dr[1].ToString();
                    if (dr[3] != null) ret.Password = dr[2].ToString();
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
            return ret;

        }


        public List<Usuario> getUserListByIds(string ids)
        {
            NpgsqlConnection con = null;
            List<Usuario> ret = new List<Usuario>();
            try
            {
                con = ConnectionFactory.getConnetion();
                string sql = "SELECT usuarios.id, nome, endereco, numero, cidade, telefone, celular, email, nascimento from usuarios where id in (" + ids + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.CommandType = CommandType.Text;
                NpgsqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    Usuario u = new Usuario();
                    u.Id = (long)dr[0];
                    if (dr[1] != null) u.Nome = dr[1].ToString();
                    if (dr[2] != null) u.Endereco = dr[2].ToString();
                    if (dr[3] != null) u.Numero = dr[3].ToString();
                    if (dr[4] != null) u.Cidade = dr[4].ToString();
                    if (dr[5] != null) u.Telefone = dr[5].ToString();
                    if (dr[6] != null) u.Celular = dr[6].ToString();
                    if (dr[7] != null) u.Email = dr[7].ToString();
                    if (!DBNull.Value.Equals(dr[8])) u.Nascimento = (DateTime)dr[8];
                    ret.Add(u);
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
            return ret;
        }



        public List<Usuario> getUserListByEventoId(long id)
        {
            NpgsqlConnection con = null;
            List<Usuario> ret = new List<Usuario>();
            try
            {
                con = ConnectionFactory.getConnetion();
                string sql = "SELECT usuarios.id, nome, endereco, numero, cidade, telefone, celular, email, nascimento from usuarios, usuarios_eventos where usuarios.id = usuarios_eventos.usuarioid and usuarios_eventos.eventoId = @id";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new NpgsqlParameter("@id", id));
                NpgsqlDataReader dr = command.ExecuteReader();
               
                while (dr.Read())
                {
                    Usuario u = new Usuario();
                    u.Id = (long)dr[0];
                    if (dr[1] != null) u.Nome = dr[1].ToString();
                    if (dr[2] != null) u.Endereco = dr[2].ToString();
                    if (dr[3] != null) u.Numero = dr[3].ToString();
                    if (dr[4] != null) u.Cidade = dr[4].ToString();
                    if (dr[5] != null) u.Telefone = dr[5].ToString();
                    if (dr[6] != null) u.Celular = dr[6].ToString();
                    if (dr[7] != null) u.Email = dr[7].ToString();
                    if (!DBNull.Value.Equals(dr[8])) u.Nascimento = (DateTime)dr[8];
                    ret.Add(u);
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
            return ret;
        }

        public List<Usuario> getUserListNaoConvidadosByEventoId(long id)
        {
            NpgsqlConnection con = null;
            List<Usuario> ret = new List<Usuario>();
            try
            {
                con = ConnectionFactory.getConnetion();
                string sql = "SELECT usuarios.id, nome, endereco, numero, cidade, telefone, celular, email, nascimento from usuarios where usuarios.id not in ( select usuarioId from usuarios_eventos where eventoId = @id )";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.CommandType = CommandType.Text;
                command.Parameters.Add(new NpgsqlParameter("@id", id));
                NpgsqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    Usuario u = new Usuario();
                    u.Id = (long)dr[0];
                    if (dr[1] != null) u.Nome = dr[1].ToString();
                    if (dr[2] != null) u.Endereco = dr[2].ToString();
                    if (dr[3] != null) u.Numero = dr[3].ToString();
                    if (dr[4] != null) u.Cidade = dr[4].ToString();
                    if (dr[5] != null) u.Telefone = dr[5].ToString();
                    if (dr[6] != null) u.Celular = dr[6].ToString();
                    if (dr[7] != null) u.Email = dr[7].ToString();
                    if (!DBNull.Value.Equals(dr[8])) u.Nascimento = (DateTime)dr[8];
                    ret.Add(u);
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
            return ret;
        }

        public void updateUser(Usuario u)
        {
            NpgsqlConnection con = new NpgsqlConnection();
            try
            {
                con = ConnectionFactory.getConnetion();
                NpgsqlCommand cmd = new NpgsqlCommand
                ("update usuarios set nome = @nome, endereco = @endereco, numero = @numero, cidade = @cidade, telefone= @telefone, celular = @celular, email = @email, nascimento=@nascimento, password=@password where id = @id ", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new NpgsqlParameter("@nome", u.Nome));
                cmd.Parameters.Add(new NpgsqlParameter("@endereco", u.Endereco));
                cmd.Parameters.Add(new NpgsqlParameter("@numero", u.Numero));
                cmd.Parameters.Add(new NpgsqlParameter("@cidade", u.Cidade));
                cmd.Parameters.Add(new NpgsqlParameter("@telefone", u.Telefone));
                cmd.Parameters.Add(new NpgsqlParameter("@celular", u.Celular));
                cmd.Parameters.Add(new NpgsqlParameter("@email", u.Email));
                cmd.Parameters.Add(new NpgsqlParameter("@nascimento", u.Nascimento));
                cmd.Parameters.Add(new NpgsqlParameter("@password", u.Password));
                cmd.Parameters.Add(new NpgsqlParameter("@id", u.Id));

                cmd.ExecuteNonQuery();
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

        public void insertUser(Usuario u)
        {
            NpgsqlConnection con = new NpgsqlConnection();
            try
            {
                con = ConnectionFactory.getConnetion();
                NpgsqlCommand cmd = new NpgsqlCommand
                    ("Insert into usuarios (nome, endereco, numero, cidade, telefone, celular, email, nascimento, password ) " +
                                   "values (@nome, @endereco, @numero, @cidade, @telefone, @celular, @email, @nascimento, @password)", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new NpgsqlParameter("@nome",u.Nome));
                cmd.Parameters.Add(new NpgsqlParameter("@endereco", u.Endereco));
                cmd.Parameters.Add(new NpgsqlParameter("@numero", u.Numero));
                cmd.Parameters.Add(new NpgsqlParameter("@cidade", u.Cidade));
                cmd.Parameters.Add(new NpgsqlParameter("@telefone", u.Telefone));
                cmd.Parameters.Add(new NpgsqlParameter("@celular", u.Celular));
                cmd.Parameters.Add(new NpgsqlParameter("@email", u.Email));
                cmd.Parameters.Add(new NpgsqlParameter("@nascimento", u.Nascimento));
                cmd.Parameters.Add(new NpgsqlParameter("@password", u.Password));

                cmd.ExecuteNonQuery();
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

        public int getUserListByNamePageCount(string nome)
        {
            NpgsqlConnection con = new NpgsqlConnection();
            int ret = 0;
            try
            {
                con = ConnectionFactory.getConnetion();
                NpgsqlCommand command = new NpgsqlCommand("SELECT count(*) from usuarios where nome like @nome", con);
                command.Parameters.Add(new NpgsqlParameter("@nome", nome));
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    ret = dr.GetInt32(0);
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
            return ret;
        }

        public List<Usuario> getUserListByNamePage(int offset, string name)
        {
            NpgsqlConnection con = new NpgsqlConnection();
            List<Usuario> ret = new List<Usuario>();
            try
            {
                con = ConnectionFactory.getConnetion();
                NpgsqlCommand command = new NpgsqlCommand("SELECT id, nome, endereco, numero, cidade, telefone, celular, email, nascimento from usuarios where nome like @nome limit @rows offset @offset", con);
                command.Parameters.Add(new NpgsqlParameter("@nome", name));
                command.Parameters.Add(new NpgsqlParameter("@rows", Constants.N_ROWS));
                command.Parameters.Add(new NpgsqlParameter("@offset", offset));
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Usuario u = new Usuario();
                    u.Id = (long)dr[0];
                    if (dr[1] != null) u.Nome = dr[1].ToString();
                    if (dr[2] != null) u.Endereco = dr[2].ToString();
                    if (dr[3] != null) u.Numero = dr[3].ToString();
                    if (dr[4] != null) u.Cidade = dr[4].ToString();
                    if (dr[5] != null) u.Telefone = dr[5].ToString();
                    if (dr[6] != null) u.Celular = dr[6].ToString();
                    if (dr[7] != null) u.Email = dr[7].ToString();
                    if (!DBNull.Value.Equals(dr[8])) u.Nascimento = (DateTime)dr[8];
                    ret.Add(u);
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
            return ret;
        }

        public int getUserListPageCount()
        {
            NpgsqlConnection con = new NpgsqlConnection();
            int ret = 0;
            try
            {
                con = ConnectionFactory.getConnetion();
                NpgsqlCommand command = new NpgsqlCommand("SELECT count(*) from usuarios", con);
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    ret = dr.GetInt32(0);
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
            return ret;
        }

        public List<Usuario> getUserListPage(int offset)
        {
            NpgsqlConnection con = new NpgsqlConnection();
            List<Usuario> ret = new List<Usuario>();
            try
            {
                con = ConnectionFactory.getConnetion();
                NpgsqlCommand command = new NpgsqlCommand("SELECT id, nome, endereco, numero, cidade, telefone, celular, email, nascimento from usuarios limit @rows offset @offset", con);
                command.Parameters.Add(new NpgsqlParameter("@rows", Constants.N_ROWS));
                command.Parameters.Add(new NpgsqlParameter("@offset", offset));
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Usuario u = new Usuario();
                    u.Id = (long)dr[0];
                    if (dr[1] != null) u.Nome = dr[1].ToString();
                    if (dr[2] != null) u.Endereco = dr[2].ToString();
                    if (dr[3] != null) u.Numero = dr[3].ToString();
                    if (dr[4] != null) u.Cidade = dr[4].ToString();
                    if (dr[5] != null) u.Telefone = dr[5].ToString();
                    if (dr[6] != null) u.Celular = dr[6].ToString();
                    if (dr[7] != null) u.Email = dr[7].ToString();
                    if (!DBNull.Value.Equals(dr[8])) u.Nascimento = (DateTime)dr[8];
                    ret.Add(u);
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
            return ret;
        }


        public List<Usuario> getUserList() {
            NpgsqlConnection con = new NpgsqlConnection();
            List<Usuario> ret = new List<Usuario>();
            try
            {
                con = ConnectionFactory.getConnetion();
                NpgsqlCommand command = new NpgsqlCommand("SELECT id, nome, endereco, numero, cidade, telefone, celular, email, nascimento, password from usuarios", con);
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read()) {
                    Usuario u = new Usuario();
                    u.Id = (long)dr[0];
                    if (dr[1] != null) u.Nome = dr[1].ToString();
                    if (dr[2] != null) u.Endereco = dr[2].ToString();
                    if (dr[3] != null) u.Numero = dr[3].ToString();
                    if (dr[4] != null) u.Cidade = dr[4].ToString();
                    if (dr[5] != null) u.Telefone = dr[5].ToString();
                    if (dr[6] != null) u.Celular = dr[6].ToString();
                    if (dr[7] != null) u.Email = dr[7].ToString();
                    if (!DBNull.Value.Equals(dr[8])) u.Nascimento = (DateTime)dr[8];
                    if (dr[9] != null) u.Password = dr[9].ToString();
                    ret.Add(u);
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
            return ret;
        }

        public List<Usuario> getUserListByName(string nome)
        {
            NpgsqlConnection con = new NpgsqlConnection();
            List<Usuario> ret = new List<Usuario>();
            try
            {
                con = ConnectionFactory.getConnetion();
                NpgsqlCommand command = new NpgsqlCommand("SELECT id, nome, endereco, numero, cidade, telefone, celular, email, nascimento from usuarios where nome like @nome", con);

                command.CommandType = CommandType.Text;
                command.Parameters.Add(new NpgsqlParameter("@nome", nome));

                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    Usuario u = new Usuario();
                    u.Id = (long)dr[0];
                    if (dr[1] != null) u.Nome = dr[1].ToString();
                    if (dr[2] != null) u.Endereco = dr[2].ToString();
                    if (dr[3] != null) u.Numero = dr[3].ToString();
                    if (dr[4] != null) u.Cidade = dr[4].ToString();
                    if (dr[5] != null) u.Telefone = dr[5].ToString();
                    if (dr[6] != null) u.Celular = dr[6].ToString();
                    if (dr[7] != null) u.Email = dr[7].ToString();
                    if (!DBNull.Value.Equals(dr[8])) u.Nascimento = (DateTime)dr[8];

                    ret.Add(u);
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
            return ret;
        }

        public Usuario getUserById(int id)
        {
            NpgsqlConnection con = new NpgsqlConnection();
            Usuario ret = new Usuario();
            try
            {
                con = ConnectionFactory.getConnetion();
                NpgsqlCommand command = new NpgsqlCommand("SELECT id, nome, endereco, numero, cidade, telefone, celular, email, nascimento, password from usuarios where id = " + id, con);
                NpgsqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    ret.Id = (long) dr[0];
                    if (dr[1] != null) ret.Nome = dr[1].ToString();
                    if (dr[2] != null) ret.Endereco = dr[2].ToString();
                    if (dr[3] != null) ret.Numero = dr[3].ToString();
                    if (dr[4] != null) ret.Cidade = dr[4].ToString();
                    if (dr[5] != null) ret.Telefone = dr[5].ToString();
                    if (dr[6] != null) ret.Celular = dr[6].ToString();
                    if (dr[7] != null) ret.Email = dr[7].ToString();
                    if (!DBNull.Value.Equals(dr[8])) ret.Nascimento = (DateTime)dr[8];
                    if (dr[9] != null) ret.Password = dr[9].ToString();
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
            return ret;
        }

        public void deleteUsuario(long Id)
        {
            NpgsqlConnection con = null;
            try
            {


                con = ConnectionFactory.getConnetion();
                string sql = "delete from usuarios where id = @id";
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