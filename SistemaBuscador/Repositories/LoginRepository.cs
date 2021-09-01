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
            //string query1 = "select count(*) from usuarios where usuario = '";
            //string query2 = "' and password = '";
            //string query3 = "'";
            //string query = query1 + usuario + query2 + password + query3;

            using SqlConnection sql = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("sp_check_usuario", sql);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@usuario", usuario));
            cmd.Parameters.Add(new SqlParameter("@password", password));


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
