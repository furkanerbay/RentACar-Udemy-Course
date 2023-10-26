using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;

public class BrandConfiguration : IEntityTypeConfiguration<Brand> 
    //ApplyConfigurationsFromAssembly'daki buradaki interface'i implemente eden classları bulup onları konfigurasyon olarak ekleyecek.
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brands").HasKey(b => b.Id); //Hangi tabloya karşılık gelecek

        builder.Property(b => b.Id).HasColumnName("Id").IsRequired(); //Kolonları ayarlamak. Veritabanında Id karsılık gelır ve required
        builder.Property(b => b.Name).HasColumnName("Name").IsRequired();
        builder.Property(b => b.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(b => b.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(b => b.DeletedDate).HasColumnName("DeletedDate");

        builder.HasIndex(indexExpression: b => b.Name, name: "UK_Brands_Name").IsUnique();//Marka isimlerinin tekrar edilememesi.
            //Unique key bir indexdir.

        builder.HasMany(b => b.Models);
            //İlişki için. Bir markanın bir sürü modeli vardır.

        builder.HasQueryFilter(b => !b.DeletedDate.HasValue);//Soft delete olanları getirmemiz gerekiyor Buna da global query filtresi deniyor
    }
}
