using DEVinHouse.SolarEnergy.Domain.Entities.Shared;

namespace DEVinHouse.SolarEnergy.Domain.Entities
{
    public class Plant : Entity
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public bool Active { get; private set; }
        public ICollection<Generation>? Generations { get; private set; }

        public Plant(string name, string address, string brand, string model, bool active) 
        {
            Name = name;
            Address = address;
            Brand = brand;
            Model = model;
            Active = active;
        }

        public void ToggleActiveStatus()
        {
            Active = !Active;
        }

        public void UpdateUnit(string nickname, string address, string brand, string model, bool active)
        {
            Name = nickname;
            Address = address;
            Brand = brand;
            Model = model;
            Active = active;
        }
    }
}