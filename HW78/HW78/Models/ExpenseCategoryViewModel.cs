using Microsoft.AspNetCore.Mvc;

namespace HW78.Models
{
    public class ExpenseCategoryViewModel
    {
        public string CategoryName { get; set; }
        public decimal TotalCost { get; set; }
        public List<Expense> Expenses { get; set; }
    }

}
