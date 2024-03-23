using Microsoft.EntityFrameworkCore;

using Models;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options) { }

    public virtual DbSet<Activity> Activities { get; set; }
}