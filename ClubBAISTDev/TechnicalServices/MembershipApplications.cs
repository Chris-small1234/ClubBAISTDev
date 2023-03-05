using ClubBAISTDev.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClubBAISTDev.TechnicalServices
{
    public class MembershipApplications
    {
        public bool CreateMembershipApplication(MembershipApplication NewMembershipApplication)
        {
            bool Confirmation = false;

            SqlConnection DataSource = new();


            DataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB";

            DataSource.Open();

            SqlCommand AddCommand = new();
            AddCommand.Connection = DataSource;
            AddCommand.CommandType = CommandType.StoredProcedure;
            AddCommand.CommandText = "CreateMembershipApplication";

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@FirstName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.FirstName
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@LastName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.LastName
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@MembershipAddress",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.Address
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@PostalCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.PostalCode
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@PhoneNumber",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.PhoneNumber
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@AlternatePhoneNumber",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.AlternatePhoneNumber
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.Email
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@DateOfBirth",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.DateOfBirth
            };
            AddCommand.Parameters.Add(CommandParameter);
            CommandParameter = new()
            {
                ParameterName = "@Occupation",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.Occupation
            };
            AddCommand.Parameters.Add(CommandParameter);
            CommandParameter = new()
            {
                ParameterName = "@CompanyName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.CompanyName
            };
            AddCommand.Parameters.Add(CommandParameter);
            CommandParameter = new()
            {
                ParameterName = "@CompanyAddress",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.CompanyAddress
            };
            AddCommand.Parameters.Add(CommandParameter);
            CommandParameter = new()
            {
                ParameterName = "@CompanyPostalCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.CompanyPostalCode
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@CompanyPhoneNumber",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.CompanyPhoneNumber
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@MemberReference1",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.MemberReference1
            };
            AddCommand.Parameters.Add(CommandParameter);

            CommandParameter = new()
            {
                ParameterName = "@MemberReference2",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = NewMembershipApplication.MemberReference2
            };
            AddCommand.Parameters.Add(CommandParameter);

            if (NewMembershipApplication.Approved == true)
            {
                CommandParameter = new()
                {
                    ParameterName = "@Approved",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = 1
                };
            } else
            {
                CommandParameter = new()
                {
                    ParameterName = "@Approved",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = 0
                };
            }
            AddCommand.Parameters.Add(CommandParameter);

            AddCommand.ExecuteNonQuery();
            DataSource.Close();

            Confirmation = true;
            return Confirmation;
        }

        public List<MembershipApplication> GetAllMembershipApplications()
        {
            List<MembershipApplication> AllMembershipApplications = new();

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB;";
            MyDataSource.Open();

            SqlCommand Command = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetMemberApplications"
            };


            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    MembershipApplication AMembershipApplication = new();
                    AMembershipApplication.MembershipApplicationId = (int)DataReader["MemberApplicationId"];
                    AMembershipApplication.FirstName = (string)DataReader["FirstName"];
                    AMembershipApplication.LastName = (string)DataReader["LastName"];
                    AllMembershipApplications.Add(AMembershipApplication);
                }
            }
            DataReader.Close();
            MyDataSource.Close();
            return AllMembershipApplications;
        }

        public MembershipApplication GetMembershipApplication(int membershipApplicationId)
        {
            MembershipApplication CurrentMembershipApplication = new();

            SqlConnection MyDataSource = new();
            MyDataSource.ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=ClubBAISTDev;server=(localDB)\MSSQLLocalDB;";
            MyDataSource.Open();

            SqlCommand Command = new()
            {
                Connection = MyDataSource,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetMemberApplication"
            };

            SqlParameter CommandParameter;

            CommandParameter = new()
            {
                ParameterName = "@MembershipApplicationId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = membershipApplicationId
            };
            Command.Parameters.Add(CommandParameter);

            SqlDataReader DataReader;
            DataReader = Command.ExecuteReader();

            if (DataReader.HasRows)
            {
                DataReader.Read();
                CurrentMembershipApplication.MembershipApplicationId = (int)DataReader["MemberApplicationId"];
                CurrentMembershipApplication.FirstName = (string)DataReader["FirstName"];
                CurrentMembershipApplication.LastName = (string)DataReader["LastName"];
                CurrentMembershipApplication.Address = (string)DataReader["MembershipAddress"];
                CurrentMembershipApplication.PostalCode = (string)DataReader["PostalCode"];
                CurrentMembershipApplication.PhoneNumber = (string)DataReader["PhoneNumber"];
                if (DataReader["AlternatePhoneNumber"] is not System.DBNull)
                {
                    CurrentMembershipApplication.AlternatePhoneNumber = (string)DataReader["AlternatePhoneNumber"];
                }
                CurrentMembershipApplication.Email = (string)DataReader["Email"];
                CurrentMembershipApplication.DateOfBirth = (DateTime)DataReader["DateOfBirth"];
                if (DataReader["Occupation"] is not System.DBNull)
                {
                    CurrentMembershipApplication.Occupation = (string)DataReader["Occupation"];
                }
                if (DataReader["CompanyName"] is not System.DBNull)
                {
                    CurrentMembershipApplication.CompanyName = (string)DataReader["CompanyName"];
                }
                if (DataReader["CompanyAddress"] is not System.DBNull)
                {
                    CurrentMembershipApplication.CompanyAddress = (string)DataReader["CompanyAddress"];
                }
                if (DataReader["CompanyPostalCode"] is not System.DBNull)
                {
                    CurrentMembershipApplication.CompanyPostalCode = (string)DataReader["CompanyPostalCode"];
                }
                if (DataReader["CompanyPhoneNumber"] is not System.DBNull)
                {
                    CurrentMembershipApplication.CompanyPhoneNumber = (string)DataReader["CompanyPhoneNumber"];
                }
                CurrentMembershipApplication.MemberReference1 = (int)DataReader["MemberReference1"];
                CurrentMembershipApplication.MemberReference2 = (int)DataReader["MemberReference2"];
                if (DataReader["ReviewedBy"] is not System.DBNull)
                {
                    CurrentMembershipApplication.ReviewedBy = (int)DataReader["ReviewedBy"];
                }
            }
            DataReader.Close();
            MyDataSource.Close();
            return CurrentMembershipApplication;
        }
    }
}
