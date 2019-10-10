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
   public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (SecurityLoginsLogPoco item in items)
                {
                    cmd.CommandText = @"insert into [dbo].[Security_Logins_Log] ([Id],[Login],[Source_IP],[Logon_Date],[Is_Succesful])
                            values(@Id,@Login,@Source_IP,@Logon_Date,@Is_Succesful)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);
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

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"select [Id],[Login],[Source_IP],[Logon_Date],[Is_Succesful] from [dbo].[Security_Logins_Log]";
                con.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SecurityLoginsLogPoco[] appPocos = new SecurityLoginsLogPoco[10000];
                while (rdr.Read())
                {
                    SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login= rdr.GetGuid(1);
                    poco.SourceIP= rdr.GetString(2);
                    poco.LogonDate= rdr.GetDateTime(3);
                    poco.IsSuccesful= rdr.GetBoolean(4);
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();

            }
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                foreach (SecurityLoginsLogPoco item in items)
                {
                    cmd.CommandText = $"delete from Security_Logins_Log where ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    con.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (SecurityLoginsLogPoco item in items)
                {
                    cmd.CommandText = @"update [dbo].[Security_Logins_Log] set [Login]=@Login,
                    [Source_IP]=@Source_IP, 
                    [Logon_Date]=@Logon_Date, 
                    [Is_Succesful]=@Is_Succesful
                    where [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Login", item.Login);
                    cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    con.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
    }
}
