using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HW78.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        [Required(ErrorMessage = "Поле 'Cost' обязательно для заполнения")]
        public decimal Cost { get; set; }
        [Required(ErrorMessage = "Поле 'Date' обязательно для заполнения")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Поле 'Comment' обязательно для заполнения")]
        public string Comment { get; set; }

        [ForeignKey("CategoryId")]
        public ExpenseCategory? Category { get; set; }
    }
}
