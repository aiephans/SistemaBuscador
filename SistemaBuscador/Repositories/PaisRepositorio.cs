using SistemaBuscador.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public class PaisRepositorio
    {
        public List<PaisListaModelo> ObtenerPaises()
        {
            var respuesta = new List<PaisListaModelo>();
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_listar_paises", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sql.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var nuevoPais = new PaisListaModelo()
                    {
                        Id = (int)reader["id"],
                        Nombre = reader["nombre"].ToString()
                    };

                    respuesta.Add(nuevoPais);
                }
            }
            return respuesta;
        }

        public void InsertPais(string nombre)
        {
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_insertar_pais", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public PaisListaModelo ObtenerPaisPorId(int id)
        {
            var respuesta = new PaisListaModelo();
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_buscar_pais_por_id", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            sql.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var nuevoPais = new PaisListaModelo()
                    {
                        Id = (int)reader["id"],
                        Nombre = reader["nombre"].ToString()
                    };

                    respuesta = nuevoPais;
                }
            }
            return respuesta;
        }

        public void ActualizarPais(int id, string nombre)
        {
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_acutalizar_pais", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            cmd.Parameters.Add(new SqlParameter("@nombre", nombre));
            sql.Open();
            cmd.ExecuteNonQuery();
        }

        public void EliminarPais(int id)
        {
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_elimiar_pais", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@id", id));
            sql.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
