using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Model:Entity<Guid>
{
    public Guid BrandId { get; set; }
    public Guid FuelId { get; set; }
    public Guid TransmissionId { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string ImageUrl { get; set; }
    public virtual Brand? Brand { get; set; } //Bir modelin bir tanesi markası olur
    public virtual Fuel? Fuel { get; set; } //Bir modelin bir tane yakıt tipi olur
    public virtual Transmission? Transmission { get; set; } //Bir modelin bir tane vites tipi olur
    public virtual ICollection<Car> Cars { get; set; } //Bir modelin bir çok arabasi olur
    public Model()
    {
        Cars = new HashSet<Car>();
    }
    public Model(Guid id,Guid brandId,Guid fuelId,Guid transmissionId, string name,decimal dailyPrice,string imageUrl):this()
    {
        Id = id;
        BrandId = brandId;
        FuelId = fuelId;
        TransmissionId = transmissionId;
        Name = name;
        DailyPrice = dailyPrice;
        ImageUrl = imageUrl;    }
}
