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
    public class StandingTeeTimeRequests
    {
        public bool CreateStandingTeeTimeRequest(StandingTeeTimeRequest RequestedStandingTeeTime)
        {
            bool Confirmation = false;

            SqlConnection DataSource = new();

            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();
            DataSource.ConnectionString = DatabaseUsersConfiguration.GetConnectionString("CBCS");
            //DataSource.ConnectionString = @"Persist Security Info=false;Database=csmall8;User ID=csmall8;Password=wtF5689!@#;server=dev1.baist.ca";

            DataSource.Open();

            SqlCommand AddCommand = new();
            AddCommand.Connection = DataSource;
            AddCommand.CommandType = CommandType.StoredProcedure;
            AddCommand.CommandText = "CreateStandingTeeTimeRequest";

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@MemberId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedStandingTeeTime.MemberId
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@TeeTime",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedStandingTeeTime.RequestedTeeTime
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@TeeTimeDayOfWeek",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedStandingTeeTime.DayOfWeek
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@StartDate",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedStandingTeeTime.StartDate
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@EndDate",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedStandingTeeTime.EndDate
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@Approved",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedStandingTeeTime.Approved
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@Player1Name",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedStandingTeeTime.Player1Name
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@Player2Name",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedStandingTeeTime.Player2Name
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@Player3Name",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedStandingTeeTime.Player3Name
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@Player4Name",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedStandingTeeTime.Player4Name
            };
            AddCommand.Parameters.Add(CommandParameter);

            AddCommand.ExecuteNonQuery();
            DataSource.Close();

            Confirmation = true;
            return Confirmation;
        }

        public bool CancelStandingTeeTimeRequest(int MemberId)
        {
            bool Confirmation = false;

            SqlConnection DataSource = new();
            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();
            DataSource.ConnectionString = DatabaseUsersConfiguration.GetConnectionString("CBCS");
            //DataSource.ConnectionString = @"Persist Security Info=false;Database=csmall8;User ID=csmall8;Password=wtF5689!@#;server=dev1.baist.ca";

            DataSource.Open();

            SqlCommand AddCommand = new();
            AddCommand.Connection = DataSource;
            AddCommand.CommandType = CommandType.StoredProcedure;
            AddCommand.CommandText = "CancelStandingTeeTimeRequest";

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@MemberId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = MemberId
            };
            AddCommand.Parameters.Add(CommandParameter);

            AddCommand.ExecuteNonQuery();
            DataSource.Close();

            Confirmation = true;
            return Confirmation;
        }

        public List<StandingTeeTimeRequest> GetStandingTeeTimeRequests()
        {
            List<StandingTeeTimeRequest> AllStandingTeeTimeRequests = new();

            SqlConnection DataSource = new();
            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();
            DataSource.ConnectionString = DatabaseUsersConfiguration.GetConnectionString("CBCS");
            //MyDataSource.ConnectionString = @"Persist Security Info=false;Database=csmall8;User ID=csmall8;Password=wtF5689!@#;server=dev1.baist.ca";
            DataSource.Open();

            SqlCommand Command = new()
            {
                Connection = DataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetStandingTeeTimeRequests"
            };


            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    StandingTeeTimeRequest AStandingTeeTimeRequest = new();
                    AStandingTeeTimeRequest.MemberId = (int)DataReader["MemberId"];
                    AStandingTeeTimeRequest.RequestedTeeTime = (DateTime)DataReader["TeeTime"];
                    AStandingTeeTimeRequest.DayOfWeek = (string)DataReader["TeeTimeDayOfWeek"];
                    AStandingTeeTimeRequest.StartDate = (DateTime)DataReader["StartDate"];
                    AStandingTeeTimeRequest.EndDate = (DateTime)DataReader["EndDate"];
                    AStandingTeeTimeRequest.Approved = (bool)DataReader["Approved"];
                    AStandingTeeTimeRequest.Player1Name = (string)DataReader["Player1Name"];
                    AStandingTeeTimeRequest.Player2Name = (string)DataReader["Player2Name"];
                    AStandingTeeTimeRequest.Player3Name = (string)DataReader["Player3Name"];
                    AStandingTeeTimeRequest.Player4Name = (string)DataReader["Player4Name"];
                    AllStandingTeeTimeRequests.Add(AStandingTeeTimeRequest);
                }
            }
            DataReader.Close();
            DataSource.Close();
            return AllStandingTeeTimeRequests;
        }

        public StandingTeeTimeRequest GetStandingTeeTimeRequestByMemberId(int MemberId)
        {
            StandingTeeTimeRequest CurrentStandingTeeTimeRequest = new();

            SqlConnection DataSource = new();
            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();
            DataSource.ConnectionString = DatabaseUsersConfiguration.GetConnectionString("CBCS");
            //MyDataSource.ConnectionString = @"Persist Security Info=false;Database=csmall8;User ID=csmall8;Password=wtF5689!@#;server=dev1.baist.ca";
            DataSource.Open();

            SqlCommand Command = new()
            {
                Connection = DataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetStandingTeeTimeRequest"
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
                DataReader.Read();
                CurrentStandingTeeTimeRequest.MemberId = (int)DataReader["MemberId"];
                CurrentStandingTeeTimeRequest.RequestedTeeTime = (DateTime)DataReader["TeeTime"];
                CurrentStandingTeeTimeRequest.DayOfWeek = (string)DataReader["TeeTimeDayOfWeek"];
                CurrentStandingTeeTimeRequest.StartDate = (DateTime)DataReader["StartDate"];
                CurrentStandingTeeTimeRequest.EndDate = (DateTime)DataReader["EndDate"];
                CurrentStandingTeeTimeRequest.Player2Name = (string)DataReader["Player2Name"];
                CurrentStandingTeeTimeRequest.Player3Name = (string)DataReader["Player3Name"];
                CurrentStandingTeeTimeRequest.Player4Name = (string)DataReader["Player4Name"];
            }
            DataReader.Close();
            DataSource.Close();
            return CurrentStandingTeeTimeRequest;
        }
    }
}
