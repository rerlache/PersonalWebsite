using System.Runtime;

namespace API.Data
{
    public class GeneralContext : DbContext
    {
        public GeneralContext(DbContextOptions<GeneralContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ApplicationsDatabase;Trusted_Connection=true;TrustServerCertificate=true");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistory { get; set; }
        public DbSet<UserSecurityQuestion> UserSecurityQuestions { get; set; }
        public DbSet<Application> Applications { get; set; }
    }
}
