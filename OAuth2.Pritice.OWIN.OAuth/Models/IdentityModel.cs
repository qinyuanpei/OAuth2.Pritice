using MySql.Data.Entity;
using System;
using System.Data.Entity;
using System.Linq;

namespace OAuth2.Pritice.OWIN.OAuth.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class IdentityModel : DbContext
    {
        public IdentityModel()
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<UserModel>().MapToStoredProcedures();
            //modelBuilder.Entity<ClientModel>().MapToStoredProcedures();
        }

        public virtual DbSet<UserModel> Users { get; set; }

        public virtual DbSet<ClientModel> Clients { get; set; }
    }
}