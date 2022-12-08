using ClubBAISTDev.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAISTDev.TechnicalServices
{
    public class DailyTeeSheets
    {
        public DailyTeeSheet GetDailyTeeSheet(DateTime selectedDate)
        {
            DailyTeeSheet TeeSheet = new();

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB;";
            MyDataSource.Open();

            SqlCommand Command = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetDailyTeeSheet"
            };

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@SelectedDate",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = selectedDate.Date
            };
            Command.Parameters.Add(CommandParameter);

            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                DataReader.Read();
                TeeSheet.DailyTeeSheetId = (int)DataReader["DailyTeeSheetId"];
                TeeSheet.TeeSheetDate = (DateTime)DataReader["TeeSheetDate"];
                TeeSheet.TeeSheetDayOfWeek = (string)DataReader["TeeSheetDayOfWeek"];
            }
            DataReader.Close();
            MyDataSource.Close();
            return TeeSheet;
        }

        public bool CreateDailyTeeSheet(DateTime TeeSheetDate, string TeeSheetDayOfWeek)
        {
            bool Confirmation = false;

            SqlConnection DataSource = new();

            DataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB";

            DataSource.Open();

            SqlCommand AddCommand = new();
            AddCommand.Connection = DataSource;
            AddCommand.CommandType = CommandType.StoredProcedure;
            AddCommand.CommandText = "CreateDailyTeeSheet";

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@TeeSheetDate",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = TeeSheetDate
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@TeeSheetDayOfWeek",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = TeeSheetDayOfWeek
            };
            AddCommand.Parameters.Add(CommandParameter);

            AddCommand.ExecuteNonQuery();
            DataSource.Close();

            Confirmation = true;
            return Confirmation;
        }
    }
}
