using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClientApp
{
    class DemoContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }


        public static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options
                .UseLoggerFactory(ConsoleLoggerFactory)
                .UseSqlServer(@"Server=AMANK-XPS-9360\SQLEXPRESS;Database=OrmDemo;Integrated Security=true");
    }
}
