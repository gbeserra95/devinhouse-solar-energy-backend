using DEVinHouse.SolarEnergy.Domain.Entities.Shared;

namespace DEVinHouse.SolarEnergy.Domain.Entities
{
    public class Generation : Entity
    {
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal MonthlyConsumption { get; set; }
        public decimal DailyAverage { get; set; }
        public int PlantId { get; set; }

        public Generation(DateTime date, decimal monthlyConsumption, int plantId)
        {
            Date = date;
            MonthlyConsumption = monthlyConsumption;  
            PlantId = plantId;

            GetDailyAverageConsumption(date, monthlyConsumption);       
        }

        private void GetDailyAverageConsumption(DateTime date, decimal monthlyConsumption)
        {
            var days = DateTime.DaysInMonth(date.Year, date.Month);

            DailyAverage = monthlyConsumption/days;
        }

        public void UpdateGeneration(DateTime date, decimal monthlyConsumption)
        {
            Date = date;
            MonthlyConsumption = monthlyConsumption;
        }

    }
}