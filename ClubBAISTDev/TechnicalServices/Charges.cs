using ClubBAISTDev.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAISTDev.TechnicalServices
{
    public class Charges
    {
        public List<Charge> GetChargesByMember(int MemberId)
        {
            List<Charge> AllCharges = new();

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB;";
            MyDataSource.Open();

            SqlCommand Command = new()
            {
                Connection = MyDataSource,
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
            MyDataSource.Close();
            return AllCharges;
        }
    }
}
