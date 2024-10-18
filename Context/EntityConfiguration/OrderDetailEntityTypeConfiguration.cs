namespace ELDOKKAN.Models.EntityConfiguration;
class OrderDetailsEntityTypeConfiguration : IEntityTypeConfiguration<OrderDetails>
{
    public void Configure(EntityTypeBuilder<OrderDetails> builder)
    {
       builder.HasKey(od => new { od.OrderID, od.ProductID });

       builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderID);

        builder.HasOne(od => od.Product)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(od => od.ProductID);
    }
}