using SistemaBuscador.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public class RolRepositorio
    {
        public void InsertRol(string nombre,bool lectura, bool escritura)
        {
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_insertar_rol", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            cmd.Parameters.Add(new SqlParameter("@lectura", lectura));
            cmd.Parameters.Add(new SqlParameter("@escritura", escritura));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public List<RolListaModelo> ObtenerRoles()
        {
            var respuesta = new List<RolListaModelo>();
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_listar_roles", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sql.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var nuevoRol = new RolListaModelo()
                    {
                        Id = (int)reader["id"],
                        Nombre = reader["nombre"].ToString(),
                        Escritura =(bool)reader["escritura"],
                        Lectura = (bool)reader["lectura"]
                    };

                    respuesta.Add(nuevoRol);
                }
            }
            return respuesta;
        }

        public RolListaModelo ObtenerRolPorId(int id)
        {
            var respuesta = new RolListaModelo();
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_buscar_rol_por_id", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            sql.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var nuevoRol = new RolListaModelo()
                    {
                        Id = (int)reader["id"],
                        Nombre = reader["nombre"].ToString(),
                        Escritura = (bool)reader["escritura"],
                        Lectura = (bool)reader["lectura"]
                    };

                    respuesta = nuevoRol;
                }
            }
            return respuesta;
        }


        public void ActualizarRol(int id, string nombre, bool lectura, bool escritura)
        {
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_acutalizar_rol", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            cmd.Parameters.Add(new SqlParameter("@lectura", lectura));
            cmd.Parameters.Add(new SqlParameter("@escritura", escritura));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public void EliminarRol(int id)
        {
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_elimiar_rol", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

    }
}
