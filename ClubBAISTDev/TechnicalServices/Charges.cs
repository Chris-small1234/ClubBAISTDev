using ClubBAISTDev.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAISTDev.TechnicalServices
{
    public class Charges
    {
        public List<Charge> GetChargesByMember(int MemberId)
        {
            List<Charge> AllCharges = new();

            SqlConnection DataSource = new();
            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();
            DataSource.ConnectionString = DatabaseUsersConfiguration.GetConnectionString("CBCS");
            //DataSource.ConnectionString = @"Persist Security Info=false;Database=csmall8;User ID=csmall8;Password=wtF5689!@#;server=dev1.baist.ca";
            DataSource.Open();

            SqlCommand Command = new()
            {
                Connection = DataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetChargesByMember"
            };

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@MemberId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = MemberId
            };
            Command.Parameters.Add(CommandParameter);

            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    Charge CurrentCharge = new();
                    CurrentCharge.ChargeId = (int)DataReader["ChargeId"];
                    CurrentCharge.MemberId = (int)DataReader["MemberId"];
                    CurrentCharge.Amount = (decimal)DataReader["Amount"];
                    CurrentCharge.WhenCharged = (DateTime)DataReader["WhenCharged"];
                    CurrentCharge.WhenBooked = (DateTime)DataReader["WhenBooked"];
                    AllCharges.Add(CurrentCharge);
                }
            }
            DataReader.Close();
            DataSource.Close();
            return AllCharges;
        }
    }
}
