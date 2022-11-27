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
    }
}
