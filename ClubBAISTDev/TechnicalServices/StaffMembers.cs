using ClubBAISTDev.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB;";
            MyDataSource.Open();

            SqlCommand Command = new()
            {
                Connection = MyDataSource,
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
            MyDataSource.Close();
            return Success;
        }

        public StaffMember GetStaffMember(int staffMemberId)
        {
            StaffMember CurrentStaffMember = new();

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB;";
            MyDataSource.Open();

            SqlCommand Command = new()
            {
                Connection = MyDataSource,
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
            MyDataSource.Close();
            return CurrentStaffMember;
        }
    }
}
