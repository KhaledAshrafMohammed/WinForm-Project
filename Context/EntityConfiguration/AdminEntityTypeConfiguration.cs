namespace ELDOKKAN.Models.EntityConfiguration;
class AdminEntityTypeConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasKey(ad => ad.AdminID);
        builder.Property(ad=>ad.Email).IsRequired();
        builder.Property(ad => ad.Password).IsRequired();
    }
}