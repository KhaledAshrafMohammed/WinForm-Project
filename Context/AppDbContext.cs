
namespace ELDOKKAN.Context;
public class AppDbContext : DbContext
{
    // public AppDbContext(DbContextOptions<AppDbContext> options)
    //     : base(options)
    // {
    // }

    public DbSet<Admin> Admins { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderDetails> OrderDetails { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;


    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["World"].ConnectionString);
    //}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile(@"D:\ITI\Advance C#\Project\Context\AppSettings.json", optional: false, reloadOnChange: true)
               .Build();


        base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")!);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration<Admin>(new AdminEntityTypeConfiguration())
            .ApplyConfiguration<Category>(new CategoryEntityTypeConfiguration())
            .ApplyConfiguration<Product>(new ProductEntityTypeConfiguration())
            .ApplyConfiguration<Order>(new OrderEntityTypeConfiguration())
            .ApplyConfiguration<Customer>(new CustomerEntityTypeConfiguration())
            .ApplyConfiguration<OrderDetails>(new OrderDetailsEntityTypeConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
