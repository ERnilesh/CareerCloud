using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.Configuration;
namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        public void Add(params SecurityLoginPoco[] items)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandText = @"insert into [dbo].[Security_Logins] ([Id],[Login],[Password],[Created_Date],
                    [Password_Update_Date],[Agreement_Accepted_Date],[Is_Locked],[Is_Inactive],[Email_Address],[Phone_Number],[Full_Name],[Force_Change_Password],[Prefferred_Language])
                    values(@Id,@Login,@Password,@Created_Date,@Password_Update_Date,@Agreement_Accepted_Date,@Is_Locked,
                    @Is_Inactive,@Email_Address,@Phone_Number,@Full_Name,@Force_Change_Password,@Prefferred_Language)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Password", item.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
                    con.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    con.Close();

                }

            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"select [Id],[Login],[Password],[Created_Date],[Password_Update_Date],
                [Agreement_Accepted_Date],[Is_Locked],[Is_Inactive],[Email_Address],[Phone_Number],[Full_Name],
                [Force_Change_Password],[Prefferred_Language],[Time_Stamp] from [dbo].[Security_Logins]";
                con.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SecurityLoginPoco[] appPocos = new SecurityLoginPoco[100];
                while (rdr.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetString(1);
                    poco.Password = rdr.GetString(2);
                    poco.Created = rdr.GetDateTime(3);
                    if(!rdr.IsDBNull(4))
                    poco.PasswordUpdate = rdr.GetDateTime(4);
                    if (!rdr.IsDBNull(5))
                    poco.AgreementAccepted = rdr.GetDateTime(5);
                    poco.IsLocked = rdr.GetBoolean(6);
                    poco.IsInactive = rdr.GetBoolean(7);
                    poco.EmailAddress = rdr.GetString(8);
                    poco.PhoneNumber = rdr.IsDBNull(9) ? String.Empty : rdr.GetString(9);
                    poco.FullName = rdr.IsDBNull(10) ? String.Empty : rdr.GetString(10);
                    poco.ForceChangePassword = rdr.GetBoolean(11);
                    poco.PrefferredLanguage = rdr.IsDBNull(12) ? String.Empty : rdr.GetString(12);
                    poco.TimeStamp = (byte[])rdr[13];
                    appPocos[x] = poco;
                    x++;

                }
                return appPocos.Where(a => a != null).ToList();

            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandText = $"delete from Security_Logins where ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    con.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (SecurityLoginPoco item in items)
                {
                    cmd.CommandText = @"update [dbo].[Security_Logins] set [Login]=@Login,
                    [Password]=@Password, 
                    [Created_Date]=@Created_Date, 
                    [Password_Update_Date]=@Password_Update_Date, 
                    [Agreement_Accepted_Date]=@Agreement_Accepted_Date,
                    [Is_Locked]=@Is_Locked,
                    [Is_Inactive]=@Is_Inactive,
                    [Email_Address]=@Email_Address,
                    [Phone_Number]=@Phone_Number,
                    [Full_Name]=@Full_Name,
                    [Force_Change_Password]=@Force_Change_Password,
                    [Prefferred_Language]=@Prefferred_Language
                    where [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Password", item.Password);
                    cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                    cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                    cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                    cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                    cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                    cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                    cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    con.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
    }
}
