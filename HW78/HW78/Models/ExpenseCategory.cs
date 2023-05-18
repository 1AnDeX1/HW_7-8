using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HW78.Models
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Expense> Expenses { get; set; }
    }
}
