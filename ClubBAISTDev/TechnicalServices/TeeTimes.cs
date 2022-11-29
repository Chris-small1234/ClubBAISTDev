using ClubBAISTDev.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAISTDev.TechnicalServices
{
    public class TeeTimes
    {
        public List<TeeTime> GetTeeTimes(int dailyTeeSheetId)
        {
            List<TeeTime> TodayTeeTimes = new();

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB;";
            MyDataSource.Open();

            SqlCommand Command = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetTeeTimesByDailyTeeSheetId"
            };

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@DailyTeeSheetId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = dailyTeeSheetId
            };
            Command.Parameters.Add(CommandParameter);

            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    TeeTime NewTeeTime = new();
                    NewTeeTime.TeeTimeId = (int)DataReader["TeeTimeId"];
                    NewTeeTime.TimeSlot = (DateTime)DataReader["TimeSlot"];
                    NewTeeTime.NumberOfPlayers = (int)DataReader["NumberOfPlayers"];
                    NewTeeTime.Phone = (string)DataReader["Phone"];
                    NewTeeTime.NumberOfCarts = (int)DataReader["NumberOfCarts"];
                    NewTeeTime.TeeDate = (DateTime)DataReader["TeeDate"];
                    NewTeeTime.SetTeeTime = (DateTime)DataReader["TeeTime"];
                    if (DataReader["EmployeeName"] is not System.DBNull)
                    {
                        NewTeeTime.EmployeeName = (string)DataReader["EmployeeName"];
                    }
                    NewTeeTime.MemberId = (int)DataReader["MemberId"];
                    NewTeeTime.DailyTeeSheetId = (int)DataReader["DailyTeeSheetId"];
                    TodayTeeTimes.Add(NewTeeTime);
                }
            }
            DataReader.Close();
            MyDataSource.Close();
            return TodayTeeTimes;
        }
    }
}
