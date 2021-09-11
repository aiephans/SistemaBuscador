using SistemaBuscador.Models;
using SistemaBuscador.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public class UsuariosRepository
    {
        public void InsertUsuario(string nombre, string apellido, string nombreUsuario, int rolId, string password)
        {
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_insertar_usuario", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@nombres", nombre));
            cmd.Parameters.Add(new SqlParameter("@apellidos", apellido));
            cmd.Parameters.Add(new SqlParameter("@nombreUsuario", nombreUsuario));
            cmd.Parameters.Add(new SqlParameter("@rolId", rolId));
            cmd.Parameters.Add(new SqlParameter("@password",Security.Encrypt(password)));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdateUsuario(int id,string nombre, string apellidos, int rolId)
        {
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_actualiza_usuario", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Parameters.Add(new SqlParameter("@nombres", nombre));
            cmd.Parameters.Add(new SqlParameter("@apellidos", apellidos));
            cmd.Parameters.Add(new SqlParameter("@rolId", rolId));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdatePassword(int id,string password)
        {
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_actualiza_password", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Parameters.Add(new SqlParameter("@password", password));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeleteUsuario(int id)
        {
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_eliminar_usuario", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public bool NombreUsuarioExiste(string usuario)
        {
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            bool respuesta = false;
          
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_check_nombre_usuario", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@nombreUsuario", usuario));
            sql.Open();
            int count = (int)cmd.ExecuteScalar();
            if (count > 0)
            {
                respuesta = true;
            }
            return respuesta;
        }

        public List<UsuarioListaModel> ObtenerUsuarios()
        {
            var respuesta = new List<UsuarioListaModel>();
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_obtener_usuarios", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sql.Open();
            using (var reader = cmd.ExecuteReader()) 
            {
                while (reader.Read())
                {
                    var nuevoUsuario = new UsuarioListaModel()
                    {
                        Id = (int)reader["id"],
                        Nombres = reader["nombres"].ToString(),
                        Apellidos = reader["apellidos"].ToString(),
                        NombreUsuario = reader["nombreUsuario"].ToString(),
                        RolId = (int)reader["rolId"]
                    };

                    respuesta.Add(nuevoUsuario);
                }
            }
            return respuesta;
        }

        public UsuarioEdicionModel ObtenerUsuarioPorId(int id)
        {
            var respuesta = new UsuarioEdicionModel();
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_obtener_usuario_por_id", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            sql.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var nuevoUsuario = new UsuarioEdicionModel()
                    {
                        Id = (int)reader["id"],
                        Nombres = reader["nombres"].ToString(),
                        Apellidos = reader["apellidos"].ToString(),
                        NombreUsuario = reader["nombreUsuario"].ToString(),
                        RolId = (int)reader["rolId"],
                        Password = reader["password"].ToString()
                    };

                    respuesta = nuevoUsuario;
                }
            }
            return respuesta;
        }
    }
}
