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
    public class StaffMembers
    {
        public bool Login(int staffMemberId, string staffMemberPassword)
        {
            bool Success = false;
            StaffMember AuthenticatedMember = new();

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
                CommandText = "LoginStaffMember"
            };

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@StaffMemberId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = staffMemberId
            };
            Command.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@StaffMemberPassword",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = staffMemberPassword
            };
            Command.Parameters.Add(CommandParameter);

            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                DataReader.Read();
                AuthenticatedMember.StaffMemberId = (int)DataReader["StaffMemberId"];
                AuthenticatedMember.StaffMemberName = (string)DataReader["StaffMemberName"];
                AuthenticatedMember.StaffTypeName = (string)DataReader["StaffTypeName"];
                Success = true;
            }
            DataReader.Close();
            DataSource.Close();
            return Success;
        }

        public StaffMember GetStaffMember(int staffMemberId)
        {
            StaffMember CurrentStaffMember = new();

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
                CommandText = "GetStaffMember"
            };

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@StaffMemberId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = staffMemberId
            };
            Command.Parameters.Add(CommandParameter);

            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                DataReader.Read();
                CurrentStaffMember.StaffMemberId = (int)DataReader["StaffMemberId"];
                CurrentStaffMember.StaffMemberName = (string)DataReader["StaffMemberName"];
                CurrentStaffMember.StaffTypeName = (string)DataReader["StaffTypeName"];
            }
            DataReader.Close();
            DataSource.Close();
            return CurrentStaffMember;
        }
    }
}
