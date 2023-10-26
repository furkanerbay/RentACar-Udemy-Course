using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfigurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
//ApplyConfigurationsFromAssembly'daki buradaki interface'i implemente eden classları bulup onları konfigurasyon olarak ekleyecek.
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("Cars").HasKey(b => b.Id); //Hangi tabloya karşılık gelecek

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired(); //Kolonları ayarlamak. Veritabanında Id karsılık gelır ve required
        builder.Property(b => b.ModelId).HasColumnName("ModelId").IsRequired();
        builder.Property(b => b.Kilometer).HasColumnName("Kilometer").IsRequired();
        builder.Property(b => b.CarState).HasColumnName("State").IsRequired();
        builder.Property(b => b.ModelYear).HasColumnName("ModelYear").IsRequired();

        builder.HasOne(b => b.Model);

        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        //builder.HasIndex(indexExpression: b => b.Name, name: "UK_Models_Name").IsUnique//Marka isimlerinin tekrar edilememesi.
            //Burada herhangi bir tekrar etmesini istemediğimiz  veri olmadığından bunu kaldırıyoruz.

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);//Soft delete olanları getirmemiz gerekiyor Buna da global query filtresi deniyor
    }
}