using Canducci.Pagination;
using DEVinHouse.SolarEnergy.Domain.Entities;

namespace DEVinHouse.SolarEnergy.Domain.DTOs.Responses
{
    public class GenerationsResponse
    {
        public bool Success { get ; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; }
        public void AddError(string error) => Errors.Add(error);
        public void AddErrors(IEnumerable<string> errors) => Errors.AddRange(errors);
        public int PageCount { get; set; }
        public int TotalItemCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public List<Generation> Generations { get; set; }

        public GenerationsResponse(PaginatedRest<Generation> generations)
        {
            Success = true;
            Message = Message;
            Generations = generations.Items.ToList();
            PageCount = generations.PageCount;
            PageNumber = generations.PageNumber;
            PageSize = generations.PageSize;
            TotalItemCount = generations.TotalItemCount;
            HasPreviousPage = generations.HasPreviousPage;
            HasNextPage = generations.HasNextPage;
            IsFirstPage = generations.IsFirstPage;
            IsLastPage = generations.IsLastPage;
            Errors = new List<string>();
        }
    }
}