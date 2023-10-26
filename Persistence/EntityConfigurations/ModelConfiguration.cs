using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfigurations;

public class ModelConfiguration : IEntityTypeConfiguration<Model>
//ApplyConfigurationsFromAssembly'daki buradaki interface'i implemente eden classları bulup onları konfigurasyon olarak ekleyecek.
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.ToTable("Models").HasKey(b => b.Id); //Hangi tabloya karşılık gelecek

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired(); //Kolonları ayarlamak. Veritabanında Id karsılık gelır ve required
        builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
        builder.Property(b => b.BrandId).HasColumnName("BrandId").IsRequired();
        builder.Property(b => b.FuelId).HasColumnName("FuelId").IsRequired();
        builder.Property(b => b.TransmissionId).HasColumnName("TransmissionId").IsRequired();
        builder.Property(b => b.DailyPrice).HasColumnName("DailyPrice").IsRequired();
        builder.Property(b => b.ImageUrl).HasColumnName("ImageUrl").IsRequired();


        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(indexExpression: b => b.Name, name: "UK_Models_Name").IsUnique();//Marka isimlerinin tekrar edilememesi.
            //Unique key bir indexdir.

        builder.HasOne(b => b.Brand);
        //Bir modelin bir markası olur.

        builder.HasOne(b => b.Fuel);
        builder.HasOne(b => b.Transmission);

        builder.HasMany(b => b.Cars);

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);//Soft delete olanları getirmemiz gerekiyor Buna da global query filtresi deniyor
    }
}
