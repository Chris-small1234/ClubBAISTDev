using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClubBAISTDev.Domain;

namespace ClubBAISTDev.TechnicalServices
{
    public class Players
    {
        public bool CreatePlayer(int TeeTimeId, int MemberId, string PlayerName)
        {
            bool Confirmation = false;

            SqlConnection DataSource = new();

            DataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB";

            DataSource.Open();

            SqlCommand AddCommand = new();
            AddCommand.Connection = DataSource;
            AddCommand.CommandType = CommandType.StoredProcedure;
            AddCommand.CommandText = "CreatePlayer";

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@TeeTimeId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = TeeTimeId
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@MemberId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = MemberId
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@PlayerName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = PlayerName
            };
            AddCommand.Parameters.Add(CommandParameter);

            AddCommand.ExecuteNonQuery();
            DataSource.Close();

            Confirmation = true;
            return Confirmation;
        }

        public List<Player> GetPlayers(int TeeTimeId)
        {
            List<Player> Players = new();

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB;";
            MyDataSource.Open();

            SqlCommand Command = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetPlayersByTeeTimeId"
            };

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@TeeTimeId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = TeeTimeId
            };
            Command.Parameters.Add(CommandParameter);

            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    Player NewPlayer = new();
                    NewPlayer.TeeTimeId = (int)DataReader["TeeTimeId"];
                    NewPlayer.MemberId = (int)DataReader["MemberId"];
                    NewPlayer.PlayerName = (string)DataReader["PlayerName"];
                    Players.Add(NewPlayer);
                }
            }
            DataReader.Close();
            MyDataSource.Close();
            return Players;
        }
    }
}
