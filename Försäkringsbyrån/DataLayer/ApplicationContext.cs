using Models.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    /// <summary>
    /// Our context class that manages the connection to database. 
    /// </summary>
   public class ApplicationContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<InsuredPerson> InsuredPersons { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<BaseAmount> BaseAmounts { get; set; }
       
        public DbSet<BaseAmountTabel> BaseAmountTables { get; set; }
        public DbSet<AckValueVariable> AckValueVariables { get; set; }
        public DbSet<UserAccess> UserAccesses { get; set; }
        public DbSet<ComissionShare> ComissionShares { get; set; }
        public DbSet<CompanyInsurance> CompanyInsurances { get; set; }
     
        public DbSet<LifeInsurance> LifeInsurances { get; set; }
        public DbSet<OptionalType> OptionalTypes { get; set; }
        public DbSet<OtherPersonInsurance> OtherPersonInsurances { get; set; }
        public DbSet<SAInsurance> SAInsurances { get; set; }
        public DbSet<SalesMen> SalesMens { get; set; }
        public DbSet<VacationPay> VacationPays { get; set; }
        
     
        public ApplicationContext() : base ("ICPDB")
        {   }

        /// <summary>
        /// Resets our database
        /// </summary>
        public void Reset()
        {
            string connectionString = Database.Connection.ConnectionString;
            string commandText = "DECLARE @SQL VARCHAR(MAX)='' SELECT @SQL = @SQL + 'ALTER TABLE ' + QUOTENAME(FK.TABLE_SCHEMA) + '.' + QUOTENAME(FK.TABLE_NAME) + ' DROP CONSTRAINT [' + RTRIM(C.CONSTRAINT_NAME) +'];' + CHAR(13) FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS C INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS FK ON C.CONSTRAINT_NAME = FK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS PK ON C.UNIQUE_CONSTRAINT_NAME = PK.CONSTRAINT_NAME INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE CU ON C.CONSTRAINT_NAME = CU.CONSTRAINT_NAME INNER JOIN (SELECT i1.TABLE_NAME, i2.COLUMN_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS i1 INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE i2 ON i1.CONSTRAINT_NAME = i2.CONSTRAINT_NAME WHERE i1.CONSTRAINT_TYPE = 'PRIMARY KEY') PT ON PT.TABLE_NAME = PK.TABLE_NAME EXEC (@SQL);EXEC sp_msforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT all'; EXEC sp_msforeachtable 'DROP TABLE ?'";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(commandText, conn))
            {
                conn.Open();
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (System.Exception)
                    {
                        // throw;
                    }
                }
                conn.Close();
            }
            Database.Initialize(true);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
        }
    }
}
