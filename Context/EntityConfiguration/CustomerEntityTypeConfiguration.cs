namespace ELDOKKAN.Models.EntityConfiguration;
class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(customer => customer.CustomerID);

        builder.Property(c => c.Email).IsRequired().HasMaxLength(40);

        builder.Property(c => c.Password).IsRequired().HasMaxLength(40);

        builder.HasMany(c => c.Orders)
               .WithOne(o => o.Customer)
               .HasForeignKey(o => o.CustomerID);
    }
}