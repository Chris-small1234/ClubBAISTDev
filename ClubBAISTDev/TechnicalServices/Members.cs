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
    public class Members
    {
        public bool Login(int memberId, string memberPassword)
        {
            bool Success = false;
            Member AuthenticatedMember = new();

            SqlConnection DataSource = new();
            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();
            DataSource.ConnectionString = DatabaseUsersConfiguration.GetConnectionString("CBCS");

            //put local SQL connection string here. Use appsettings.json for Prod.
            //DataSource.ConnectionString = @"Persist Security Info=false;Database=csmall8;User ID=csmall8;Password=wtF5689!@#;server=dev1.baist.ca";
            DataSource.Open();

            SqlCommand Command = new()
            {
                Connection = DataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "LoginMember"
            };

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@MemberId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = memberId
            };
            Command.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@MemberPassword",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = memberPassword
            };
            Command.Parameters.Add(CommandParameter);

            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                DataReader.Read();
                AuthenticatedMember.MemberId = (int)DataReader["MemberId"];
                AuthenticatedMember.MembershipLevel = (string)DataReader["MembershipLevel"];
                AuthenticatedMember.MemberName = (string)DataReader["MemberName"];
                AuthenticatedMember.MemberStanding = (string)DataReader["MemberStanding"];
                Success = true;
            }
            DataReader.Close();
            DataSource.Close();
            return Success;
        }

        public Member GetMember(int memberId)
        {
            Member CurrentMember = new();

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
                CommandText = "GetMember"
            };

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@MemberId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = memberId
            };
            Command.Parameters.Add(CommandParameter);

            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                DataReader.Read();
                CurrentMember.MemberId = (int)DataReader["MemberId"];
                CurrentMember.MembershipLevel = (string)DataReader["MembershipLevel"];
                if (DataReader["MembershipType"] is not System.DBNull)
                {
                    CurrentMember.MembershipType = (string)DataReader["MembershipType"];
                }
                CurrentMember.MemberName = (string)DataReader["MemberName"];
                CurrentMember.MemberEmail = (string)DataReader["MemberEmail"];
                CurrentMember.MemberStanding = (string)DataReader["MemberStanding"];
            }
            DataReader.Close();
            DataSource.Close();
            return CurrentMember;
        }

        public List<Member> GetMembers()
        {
            List<Member> AllMembers = new();

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
                CommandText = "GetMembers"
            };


            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    Member AMember = new();
                    AMember.MemberId = (int)DataReader["MemberId"];
                    AMember.MemberName = (string)DataReader["MemberName"];
                    AllMembers.Add(AMember);
                }
            }
            DataReader.Close();
            DataSource.Close();
            return AllMembers;
        }

        public bool CreateMember(Member NewMember, string MemberPassword)
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
            AddCommand.CommandText = "CreateMember";

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@MemberPassword",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = MemberPassword
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@MembershipLevel",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMember.MembershipLevel
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@MembershipType",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMember.MembershipType
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@MemberName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMember.MemberName
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@MemberEmail",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMember.MemberEmail
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@MemberStanding",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMember.MemberStanding
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@Balance",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMember.Balance
            };
            AddCommand.Parameters.Add(CommandParameter);

            AddCommand.ExecuteNonQuery();
            DataSource.Close();

            Confirmation = true;
            return Confirmation;
        }
    }
}
