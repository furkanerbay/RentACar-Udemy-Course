using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Persistence.EntityConfigurations;

public class TransmissionConfiguration : IEntityTypeConfiguration<Transmission>
//ApplyConfigurationsFromAssembly'daki buradaki interface'i implemente eden classları bulup onları konfigurasyon olarak ekleyecek.
{
    public void Configure(EntityTypeBuilder<Transmission> builder)
    {
        builder.ToTable("Transmissions").HasKey(b => b.Id); //Hangi tabloya karşılık gelecek

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired(); //Kolonları ayarlamak. Veritabanında Id karsılık gelır ve required
        builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(indexExpression: b => b.Name, name: "UK_Transmissions_Name").IsUnique();//Marka isimlerinin tekrar edilememesi.
            //Unique key bir indexdir.

        builder.HasMany(b => b.Models);
        //İlişki için. Bir markanın bir sürü modeli vardır.

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);//Soft delete olanları getirmemiz gerekiyor Buna da global query filtresi deniyor
    }
}
