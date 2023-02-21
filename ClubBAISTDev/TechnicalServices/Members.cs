using ClubBAISTDev.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB;";
            MyDataSource.Open();

            SqlCommand Command = new()
            {
                Connection = MyDataSource,
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
            MyDataSource.Close();
            return Success;
        }

        public Member GetMember(int memberId)
        {
            Member CurrentMember = new();

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB;";
            MyDataSource.Open();

            SqlCommand Command = new()
            {
                Connection = MyDataSource,
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
            MyDataSource.Close();
            return CurrentMember;
        }

        public List<Member> GetMembers()
        {
            List<Member> AllMembers = new();

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB;";
            MyDataSource.Open();

            SqlCommand Command = new()
            {
                Connection = MyDataSource,
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
            MyDataSource.Close();
            return AllMembers;
        }
    }
}
