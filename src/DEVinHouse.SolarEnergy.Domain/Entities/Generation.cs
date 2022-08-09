using DEVinHouse.SolarEnergy.Domain.Entities.Shared;

namespace DEVinHouse.SolarEnergy.Domain.Entities
{
    public class Generation : Entity
    {
        public int UnitId { get; private set; }
        public Unit Unit { get; private set; }
        public DateOnly Date { get; private set; }
        public double Consumption { get; private set; }

        public Generation(int unitId, DateOnly date, double consumption)
        {
            UnitId = unitId;
            Date = date;
            Consumption = consumption;            
        }
    }
}