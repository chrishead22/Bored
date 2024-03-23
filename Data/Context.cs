using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

using Models;

public partial class Context : DbContext
{
    public static Context CreateContext()
    {
        DbContextOptionsBuilder<Context> options = new DbContextOptionsBuilder<Context>();
        options.UseSqlServer(ConnectionString);
        return new Context(options.Options);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }

    private static string _connectionString = null;
    public static string ConnectionString
    {
        get
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

                IConfiguration configuration = configBuilder.Build();
                _connectionString = configuration.GetConnectionString("Default");
            }

            return _connectionString;
        }
        set
        {
            _connectionString = value;
        }
    }

    public Context() { }
    public Context(DbContextOptions<Context> options) : base(options) { }

    public virtual DbSet<Activity> Activities { get; set; }
}