using DEVinHouse.SolarEnergy.Domain.Entities.Shared;

namespace DEVinHouse.SolarEnergy.Domain.Entities
{
    public class Unit : Entity
    {
        public string Nickname { get; private set; }
        public string Address { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public bool Active { get; private set; }

        public Unit(string nickname, string address, string brand, string model, bool active) 
        {
            Nickname = nickname;
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
            Nickname = nickname;
            Address = address;
            Brand = brand;
            Model = model;
            Active = active;
        }
    }
}