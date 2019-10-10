using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.EntityFrameworkDataAccess
{
    class CareerCloudContext : DbContext
    {
                public CareerCloudContext(bool createProxy = true) : base(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString)
        {
            Configuration.ProxyCreationEnabled = createProxy;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyProfilePoco>()
                .HasMany(c => c.CompanyDescriptions)
                .WithRequired(d => d.CompanyProfiles)
                .HasForeignKey(d => d.Company)
                .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SystemLanguageCodePoco>()
                .HasMany(c => c.CompanyDescriptions)
                .WithRequired(d => d.SystemLanguageCodes)
                .HasForeignKey(d => d.LanguageId)
                .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(c => c.ApplicantEducations)
                .WithRequired(d => d.ApplicantProfiles)
                .HasForeignKey(d => d.Applicant)
                .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(c => c.ApplicantJobApplications)
                .WithRequired(d => d.ApplicantProfiles)
                .HasForeignKey(d => d.Applicant)
                .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<CompanyJobPoco>()
                .HasMany(c => c.ApplicantJobApplications)
                .WithRequired(d => d.CompanyJobs)
                .HasForeignKey(d => d.Job)
                .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SecurityLoginPoco>()
                .HasMany(c => c.ApplicantProfiles)
                .WithRequired(d => d.SecurityLogins)
                .HasForeignKey(d => d.Login)
                .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(c => c.ApplicantProfiles)
                .WithRequired(d => d.SystemCountryCodes)
                .HasForeignKey(d => d.Country)
                .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(c => c.ApplicantResumes)
                .WithRequired(d => d.ApplicantProfiles)
                .HasForeignKey(d => d.Applicant)
                .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(c => c.ApplicantSkills)
                .WithRequired(d => d.ApplicantProfiles)
                .HasForeignKey(d => d.Applicant)
                .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasMany(c => c.ApplicantWorkHistory)
                .WithRequired(d => d.ApplicantProfiles)
                .HasForeignKey(d => d.Applicant)
                .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SystemCountryCodePoco>()
                .HasMany(c => c.ApplicantWorkHistory)
                .WithRequired(d => d.SystemCountryCodes)
                .HasForeignKey(d => d.CountryCode)
                .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompanyJobPoco>()
               .HasMany(c => c.CompanyJobEducations)
               .WithRequired(d => d.CompanyJobs)
               .HasForeignKey(d => d.Job)
               .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompanyJobPoco>()
               .HasMany(c => c.CompanyJobSkills)
               .WithRequired(d => d.CompanyJobs)
               .HasForeignKey(d => d.Job)
               .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompanyProfilePoco>()
               .HasMany(c => c.CompanyJobs)
               .WithRequired(d => d.CompanyProfiles)
               .HasForeignKey(d => d.Company)
               .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompanyJobPoco>()
               .HasMany(c => c.CompanyJobsDescriptions)
               .WithRequired(d => d.CompanyJobs)
               .HasForeignKey(d => d.Job)
               .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompanyProfilePoco>()
              .HasMany(c => c.CompanyLocations)
              .WithRequired(d => d.CompanyProfiles)
              .HasForeignKey(d => d.Company)
              .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SecurityLoginPoco>()
               .HasMany(c => c.SecurityLoginsLog)
               .WithRequired(d => d.SecurityLogins)
               .HasForeignKey(d => d.Login)
               .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SecurityLoginPoco>()
               .HasMany(c => c.SecurityLoginsRoles)
               .WithRequired(d => d.SecurityLogins)
               .HasForeignKey(d => d.Login)
               .WillCascadeOnDelete(true);
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SecurityRolePoco>()
               .HasMany(c => c.SecurityLoginsRoles)
               .WithRequired(d => d.SecurityRoles)
               .HasForeignKey(d => d.Role)
               .WillCascadeOnDelete(true);
           // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicantEducationPoco>().Ignore(o => o.TimeStamp);
            modelBuilder.Entity<ApplicantJobApplicationPoco>().Ignore(o => o.TimeStamp);
            modelBuilder.Entity<ApplicantProfilePoco>().Ignore(o => o.TimeStamp);
            modelBuilder.Entity<ApplicantSkillPoco>().Ignore(o => o.TimeStamp);
            modelBuilder.Entity<ApplicantWorkHistoryPoco>().Ignore(o => o.TimeStamp);
            modelBuilder.Entity<CompanyDescriptionPoco>().Ignore(o => o.TimeStamp);
            modelBuilder.Entity<CompanyJobSkillPoco>().Ignore(o => o.TimeStamp);
            modelBuilder.Entity<CompanyJobPoco>().Ignore(o => o.TimeStamp);
            modelBuilder.Entity<CompanyJobEducationPoco>().Ignore(o => o.TimeStamp);
            modelBuilder.Entity<CompanyJobDescriptionPoco>().Ignore(o => o.TimeStamp);
            modelBuilder.Entity<CompanyLocationPoco>().Ignore(o => o.TimeStamp);
            modelBuilder.Entity<CompanyProfilePoco>().Ignore(o => o.TimeStamp);
            modelBuilder.Entity<SecurityLoginPoco>().Ignore(o => o.TimeStamp);
            modelBuilder.Entity<SecurityLoginsRolePoco>().Ignore(o => o.TimeStamp);
            base.OnModelCreating(modelBuilder);


           


        }
        DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        DbSet<CompanyJobDescriptionPoco> CompanyJobsDescriptions { get; set; }
        DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }
        DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }


    }
}
