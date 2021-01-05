using Microsoft.Data.SqlClient;
using ProyectoMeliApi.DTO;
using ProyectoMeliApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMeliApi.Repository
{
    public class MutantRepository
    {
        private SqlConnection connection;
        public MutantRepository()
        {
            connection = new SqlConnection(DBHelper.GetMeliConnectionString());
        }

        public DTOStats GetStats()
        {
            string queryString = "SELECT COUNT(EsMutante) Humanos, SUM(CASE WHEN EsMutante = 1 THEN 1 ELSE 0 END) Mutantes FROM AdnProcesados ";
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
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();
            }

            return stats;
        }

        public DTOAdn GetAdn(string[] adn)
        {
            return GetAdn(new DTOAdn(adn));
        }

        public DTOAdn GetAdn(DTOAdn adn)
        {
            string queryString = "SELECT EsMutante FROM AdnProcesados WHERE ADN = '" + adn.AdnFormateado + "'";
            DTOAdn retorno = new DTOAdn(adn.Adn);
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(queryString, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read() && !reader.IsDBNull(0))
                    retorno.EsMutante = reader.GetBoolean(0);

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Close();
            }

            return retorno;
        }

        public bool InsertAdn(DTOAdn adn)
        {
            bool retorno = false;

            if (adn.EsMutante.HasValue)
            {
                if (GetAdn(adn).EsMutante.HasValue)
                    retorno = true;
                else
                {
                    string query = "INSERT INTO AdnProcesados (ADN, EsMutante) VALUES ('" + adn.AdnFormateado + "'," + (adn.EsMutante.Value ? "1" : "0") + ")";

                    try
                    {
                        connection.Open();

                        SqlCommand command = new SqlCommand(query, connection);
                        int result = command.ExecuteNonQuery();

                        if (result < 0)
                            Console.WriteLine("Error insertando el adn!");
                        else
                            retorno = true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return retorno;
        }
    }
}
