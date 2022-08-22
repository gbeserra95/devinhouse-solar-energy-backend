using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using DEVinHouse.SolarEnergy.Domain.Entities.Shared;

namespace DEVinHouse.SolarEnergy.Domain.Entities
{
    public class Generation : Entity
    {
        [JsonIgnore]
        public virtual User User { get; set; }
        public string? UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal MonthlyConsumption { get; set; }
        public decimal DailyAverage { get; set; }
        public virtual Plant Plant { get; set; }
        
        [JsonIgnore]
        public int? PlantId { get; set; }

        public Generation(string userId, DateTime date, decimal monthlyConsumption, int? plantId)
        {
            UserId = userId;
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