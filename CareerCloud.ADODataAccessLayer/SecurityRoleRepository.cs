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
    public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        public void Add(params SecurityRolePoco[] items)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandText = @"insert into [dbo].[Security_Roles] ([Id],[Role],[Is_Inactive])
                            values(@Id,@Role,@Is_Inactive)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Role", item.Role);
                    cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
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

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"select [Id],[Role],[Is_Inactive] from [dbo].[Security_Roles]";
                con.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                SecurityRolePoco[] appPocos = new SecurityRolePoco[100];
                while (rdr.Read())
                {
                    SecurityRolePoco poco = new SecurityRolePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Role = rdr.GetString(1);
                    poco.IsInactive = rdr.GetBoolean(2);
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();

            }
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

            public void Remove(params SecurityRolePoco[] items)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandText = $"delete from Security_Roles where ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    con.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (SecurityRolePoco item in items)
                {
                    cmd.CommandText = @"update [dbo].[Security_Roles] set [Role]=@Role,
                    [Is_Inactive]=@Is_Inactive 
                    where [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Is_Inactive ", item.IsInactive);
                    cmd.Parameters.AddWithValue("@Role", item.Role);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    con.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    con.Close();

                }
            }
        }
    }
    }

