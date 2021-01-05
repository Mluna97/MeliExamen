using Microsoft.Data.SqlClient;
using ProyectoMeliApi.DTO;
using ProyectoMeliApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMeliApi.Repository
{
    public class StatsRepository
    {
        private SqlConnection connection;
        public StatsRepository()
        {
            connection = new SqlConnection(DBHelper.GetMeliConnectionString());
        }
        ~StatsRepository() { connection.Dispose(); }

        public DTOStats Get()
        {
            string queryString = "SELECT COUNT(EsMutante) Humanos, SUM(CASE WHEN EsMutante = 1 THEN 1 ELSE 0 END) Mutantes FROM AdnProcesados";
            DTOStats stats = new DTOStats();

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read() && !reader.IsDBNull(0))
                {
                    stats.CountHumanDNA = reader.GetInt32(0);
                    stats.CountMutantDNA = reader.GetInt32(1);
                }

                reader.Close();
            }
            catch (Exception e)
            {
                throw (e);
            }
            finally
            {
                connection.Close();
            }

            return stats;
        }
    }
}
