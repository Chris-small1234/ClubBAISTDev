using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
