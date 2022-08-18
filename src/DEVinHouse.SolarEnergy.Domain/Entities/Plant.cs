using DEVinHouse.SolarEnergy.Domain.Entities.Shared;

namespace DEVinHouse.SolarEnergy.Domain.Entities
{
    public class Plant : Entity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public bool Active { get; set; }
        public ICollection<Generation>? Generations { get; set; }

        public Plant(string userId, string name, string address, string brand, string model, bool active) 
        {
            UserId = userId;
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

        public void UpdatePlant(string name, string address, string brand, string model, bool active)
        {
            Name = name;
            Address = address;
            Brand = brand;
            Model = model;
            Active = active;
        }
    }
}