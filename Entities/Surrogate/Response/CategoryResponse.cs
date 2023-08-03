using Core.Entity.Abstract;

namespace Entities.Surrogate.Response
{
    public class CategoryResponse : ISurrogate
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public int CategoryStatus { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
