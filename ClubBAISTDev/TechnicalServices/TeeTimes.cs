﻿using ClubBAISTDev.Domain;
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
    public class TeeTimes
    {
        public List<TeeTime> GetTeeTimes(int dailyTeeSheetId)
        {
            List<TeeTime> TodayTeeTimes = new();

            SqlConnection DataSource = new();
            //DataSource.ConnectionString = @"Persist Security Info=false;Database=csmall8;User ID=csmall8;Password=wtF5689!@#;server=dev1.baist.ca";
            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();
            DataSource.ConnectionString = DatabaseUsersConfiguration.GetConnectionString("CBCS");
            DataSource.Open();

            SqlCommand Command = new()
            {
                Connection = DataSource,
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
                    NewTeeTime.NumberOfPlayers = (int)DataReader["NumberOfPlayers"];
                    NewTeeTime.Phone = (string)DataReader["Phone"];
                    NewTeeTime.NumberOfCarts = (int)DataReader["NumberOfCarts"];
                    NewTeeTime.SetTeeTime = (DateTime)DataReader["TeeTime"];
                    if (DataReader["EmployeeName"] is not System.DBNull)
                    {
                        NewTeeTime.EmployeeName = (string)DataReader["EmployeeName"];
                    }
                    NewTeeTime.MemberId = (int)DataReader["MemberId"];
                    NewTeeTime.DailyTeeSheetId = (int)DataReader["DailyTeeSheetId"];
                    NewTeeTime.MemberName = (string)DataReader["MemberName"];
                    TodayTeeTimes.Add(NewTeeTime);
                }
            }
            DataReader.Close();
            DataSource.Close();
            return TodayTeeTimes;
        }

        public bool CreateTeeTime (TeeTime RequestedTeeTime)
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
            AddCommand.CommandText = "CreateTeeTime";

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@NumberOfPlayers",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedTeeTime.NumberOfPlayers
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@Phone",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedTeeTime.Phone
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@NumberOfCarts",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedTeeTime.NumberOfCarts
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@TeeTime",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedTeeTime.SetTeeTime
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@EmployeeName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedTeeTime.EmployeeName
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@MemberId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedTeeTime.MemberId
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@DailyTeeSheetId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = RequestedTeeTime.DailyTeeSheetId
            };
            AddCommand.Parameters.Add(CommandParameter);

            AddCommand.ExecuteNonQuery();
            DataSource.Close();

            Confirmation = true;
            return Confirmation;
        }

        public TeeTime GetTeeTime (DateTime SelectedTime)
        {
            TeeTime MatchingTeeTime = new();

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
                CommandText = "GetTeeTimeByDateTime"
            };

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@SelectedTime",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = SelectedTime
            };
            Command.Parameters.Add(CommandParameter);

            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                DataReader.Read();
                MatchingTeeTime.MemberId = (int)DataReader["MemberId"];
                MatchingTeeTime.TeeTimeId = (int)DataReader["TeeTimeId"];
                MatchingTeeTime.SetTeeTime = (DateTime)DataReader["TeeTime"];
            }
            DataReader.Close();
            DataSource.Close();
            return MatchingTeeTime;
        }

        public int GetNewTeeTimeId(int MemberId, DateTime SelectedTime)
        {
            int TeeTimeId = 0;

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
                CommandText = "GetNewTeeTimeId"
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

            CommandParameter = new()
            {
                ParameterName = "@TeeTime",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = SelectedTime
            };
            Command.Parameters.Add(CommandParameter);

            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                DataReader.Read();
                TeeTimeId = (int)DataReader["TeeTimeId"];
            }
            DataReader.Close();
            DataSource.Close();
            return TeeTimeId;
        }
    }
}
