namespace HW78.Models
{
    public class MonthlyStatisticsViewModel
    {
        public string Month { get; set; }
        public List<CategoryStatisticsViewModel> CategoryStatistics { get; set; }
    }

    public class CategoryStatisticsViewModel
    {
        public string CategoryName { get; set; }
        public decimal TotalCost { get; set; }
    }


}
