using Microsoft.Data.SqlClient;
using ProyectoMeliApi.DTO;
using ProyectoMeliApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoMeliApi.Repository
{
    public class AdnRepository
    {
        private SqlConnection connection;
        public AdnRepository()
        {
            connection = new SqlConnection(DBHelper.GetMeliConnectionString());
        }
        ~AdnRepository() { connection.Dispose(); }

        public DTOAdn Get(string[] adn)
        {
            return Get(new DTOAdn(adn));
        }

        public DTOAdn Get(DTOAdn adn)
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
                throw (e);
            }
            finally
            {
                connection.Close();
            }

            return retorno;
        }

        public bool Insert(DTOAdn adn)
        {
            bool retorno = false;

            if (adn.EsMutante.HasValue)
            {
                if (Get(adn).EsMutante.HasValue)
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
                        throw (e);
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
