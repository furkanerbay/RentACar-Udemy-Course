using Core.Persistence.Repositories;
using Domain.Enums;

namespace Domain.Entities;

public class Car:Entity<Guid>
{
    public Guid ModelId { get; set; } //Model brand ile ilişkili oldugundan brandId yazmıyoruz
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; } //Plaka
    public short MinFindexScore { get; set; } //Model yükseldikce findex score'u yükselir.
    public CarState CarState { get; set; } //Araba durumu -> Bakımda vs.
    public virtual Model? Model { get; set; }
    public Car()
    {
        
    }
    public Car(
       Guid id,
       Guid modelId,
       CarState carState,
       int kilometer,
       short modelYear,
       string plate,
       short minFindexScore
   )
       : this()
    {
        Id = id;
        ModelId = modelId;
        CarState = carState;
        Kilometer = kilometer;
        ModelYear = modelYear;
        Plate = plate;
        MinFindexScore = minFindexScore;
    }

}