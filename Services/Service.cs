using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Services.Model;

namespace Services
{
    public class Service
    {

        private string _sqlConnString = String.Empty;

        public Service()
        {
            PrepConnection();
        }

        public void InsertModel(Model.PlayerModel model)
        {
            string query = $"Insert into dbo.WarPlayer (name, turns)" +
                $"VALUES('{model.name}', '{model.turns}')";
            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                conn.Open();

                sqlCommand.ExecuteNonQuery();

                conn.Close();
            }
        }

        public void InsertWinCard(Model.CardModel model)
        {
            string query = $"Insert into dbo.WinCard (TurnNum, CardVal, Suit)" +
            $"VALUES('{model.turnNum}', '{model.cardVal}', '{model.Suit}')";
            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                SqlCommand sqlCommand = new SqlCommand(query, conn);
                conn.Open();

                sqlCommand.ExecuteNonQuery();

                conn.Close();
            }
        }

        public int APIInsertPlayer(Model.PlayerModel model)
        {
            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[ApiInsertPlayer]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@name", model.name).Direction = ParameterDirection.Input;
                    sqlCommand.Parameters.AddWithValue("@turns", model.turns).Direction = ParameterDirection.Input;
                    sqlCommand.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.Output;


                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                    var result = (int)sqlCommand.Parameters["@ReturnValue"].Value;
                    conn.Close();
                    return result;

                }
            }
        }

        public int APIInsertCard(Model.CardModel model)
        {
            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[ApiInsertCard]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@turnNum", model.turnNum).Direction = ParameterDirection.Input;
                    sqlCommand.Parameters.AddWithValue("@cardVal", model.cardVal).Direction = ParameterDirection.Input;
                    sqlCommand.Parameters.AddWithValue("@Suit", model.Suit).Direction = ParameterDirection.Input;
                    sqlCommand.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.Output;


                    conn.Open();
                    sqlCommand.ExecuteNonQuery();
                    var result = (int)sqlCommand.Parameters["@ReturnValue"].Value;
                    conn.Close();
                    return result;

                }
            }
        }

        public PlayerModel APIGetPlayerByID(int id)
        {
            PlayerModel model = new PlayerModel();
            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[PlayerGetById]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", id).Direction = ParameterDirection.Input;

                    conn.Open();
                    var result = sqlCommand.ExecuteReader();


                    while (result.Read())
                    {
                        model.id = (int)result["id"];
                        model.name = (string)result["name"];
                        model.turns = (int)result["turns"];
                    }

                }

            }
            return model;
        }
        public CardModel APIGetWinCardByID(int id)
        {
            CardModel model = new CardModel();
            using (SqlConnection conn = new SqlConnection(_sqlConnString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("[dbo].[CardGetById]", conn))
                {
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@id", id).Direction = ParameterDirection.Input;

                    conn.Open();
                    var result = sqlCommand.ExecuteReader();


                    while (result.Read())
                    {
                        model.id = (int)result["id"];
                        model.Suit = (string)result["Suit"];
                        model.turnNum = (int)result["turnNum"];
                        model.cardVal = (int)result["cardVal"];
                    }

                }
            }
            return model;
        }


        private void PrepConnection()
        {
            SqlConnectionStringBuilder connBldr = new SqlConnectionStringBuilder();
            connBldr.DataSource = $"(localdb)\\MSSQLLocalDB";
            connBldr.IntegratedSecurity = true;
            connBldr.InitialCatalog = $"PROG455SP23";
            _sqlConnString = connBldr.ConnectionString;
        }
    }
}
