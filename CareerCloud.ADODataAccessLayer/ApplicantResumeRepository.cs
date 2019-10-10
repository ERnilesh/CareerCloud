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
   public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        public void Add(params ApplicantResumePoco[] items)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (ApplicantResumePoco item in items)
                {
                    cmd.CommandText = @"insert into [dbo].[Applicant_Resumes] ([Id],[Applicant],[Resume],[Last_Updated])
                            values(@Id,@Applicant,@Resume,@Last_Updated)";
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", item.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
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

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = @"select [Id],[Applicant], [Resume], [Last_Updated] from [dbo].[Applicant_Resumes]";
                con.Open();
                int x = 0;
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantResumePoco[] appPocos = new ApplicantResumePoco[100];
                while (rdr.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.Resume = rdr.GetString(2);
                    if(!rdr.IsDBNull(3))
                    poco.LastUpdated = rdr.GetDateTime(3);
                    appPocos[x] = poco;
                    x++;
                }
                return appPocos.Where(a => a != null).ToList();

            }
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                foreach (ApplicantResumePoco item in items)
                {
                    cmd.CommandText = $"delete from Applicant_Resumes where ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    con.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            string csn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(csn))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (ApplicantResumePoco item in items)
                {
                    cmd.CommandText = @"update [dbo].[Applicant_Resumes] set [Applicant]=@Applicant,
                    [Resume]=@Resume, 
                    [Last_Updated]=@Last_Updated
                    where [Id] = @Id";
                    cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                    cmd.Parameters.AddWithValue("@Resume", item.Resume);
                    cmd.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);
                    cmd.Parameters.AddWithValue("@Id", item.Id);
                    con.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    con.Close();

                }
            }

        }
    }
}
