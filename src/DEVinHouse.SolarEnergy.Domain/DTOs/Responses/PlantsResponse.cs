using Canducci.Pagination;
using DEVinHouse.SolarEnergy.Domain.Entities;

namespace DEVinHouse.SolarEnergy.Domain.DTOs.Responses
{
    public class PlantsResponse
    {
        public int PageCount { get; set; }
        public int TotalItemCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public List<Plant> Plants { get; set; }

        public PlantsResponse(PaginatedRest<Plant> plants)
        {
            PageCount = plants.PageCount;
            PageNumber = plants.PageNumber;
            PageSize = plants.PageSize;
            TotalItemCount = plants.TotalItemCount;
            HasPreviousPage = plants.HasPreviousPage;
            HasNextPage = plants.HasNextPage;
            IsFirstPage = plants.IsFirstPage;
            IsLastPage = plants.IsLastPage;
            Plants = plants.Items.ToList();
        }
    }
}