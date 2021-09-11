using SistemaBuscador.Utilidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public class LoginRepository
    {
        public bool UserExist(string usuario,string password)
        {
            string connectionString = "server=localhost;database=cib4023600db;Integrated Security=true;";
            bool respuesta = false;
            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_check_usuario", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@nombreUsuario", usuario));
            cmd.Parameters.Add(new SqlParameter("@password",Security.Encrypt(password)));


            sql.Open();
            int count = (int)cmd.ExecuteScalar();

            if (count > 0)
            {
                respuesta = true;
            }

            return respuesta;

        }
    }
}
