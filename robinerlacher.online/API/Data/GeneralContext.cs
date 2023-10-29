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
#if DEBUG
            optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=ApplicationsDatabase;Trusted_Connection=true;TrustServerCertificate=true");
#else
            optionsBuilder.UseSqlServer("Persist Security Info=False; Server=db977547513.hosting-data.io; Initial Catalog=db977547513; Integrated Security=False; MultipleActiveResultSets=True; User ID=dbo977547513; Password=oKi5ybWTpJ0SNvIS7g1iu_FuPSZqmb-cIJbhBqyl697-y");
#endif
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserLoginHistory> UserLoginHistory { get; set; }
        public DbSet<UserSecurityQuestion> UserSecurityQuestions { get; set; }
        public DbSet<Application> Applications { get; set; }
    }
}
