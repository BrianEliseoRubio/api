using System.Data.SqlClient;
using Webapi.Conexion;
using Webapi.Modelo;

namespace Webapi.Datos
{
    public class Drol
    {
        Conexionbd cn = new Conexionbd();
        public async Task<List<Mrol>> Mostrarempleados()
        {
            var lista = new List<Mrol>();
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("spObtenerRoles", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var item = await cmd.ExecuteReaderAsync())
                    {
                        while (await item.ReadAsync())
                        {
                            var mrol = new Mrol();
                            mrol.IdRol = (int)item["IdRol"];
                            mrol.Nombre = (string)item["Nombre"];
                            mrol.Bonos = (decimal)item["Bonos"];
                            lista.Add(mrol);
                        }
                    }
                }
            }
            return lista;
        }
    }
}
