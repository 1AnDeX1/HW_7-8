using HW78.Data;
using HW78.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HW78.Controllers
{
    public class ExpenseController : Controller
    {
        private ExpenseContext _context;

        public ExpenseController(ExpenseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var expenses = _context.Expenses.ToList();
            return View(expenses);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var categories = _context.ExpenseCategories.ToList();
            ViewBag.Categories = categories;
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                _context.Expenses.Add(expense);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = _context.ExpenseCategories.ToList();
            return View(expense);
        }

        public IActionResult Edit(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            var categories = _context.ExpenseCategories.ToList();
            ViewBag.Categories = categories;
            return View(expense);
        }

        [HttpPost]
        public IActionResult Edit(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(expense);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            var categories = _context.ExpenseCategories.ToList();
            ViewBag.Categories = categories;
            return View(expense);
        }

        public IActionResult Delete(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            return View(expense);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense == null)
            {
                return NotFound();
            }

            try
            {
                _context.Expenses.Remove(expense);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                // Обработка исключения DbUpdateException
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
        [HttpPost]
        public IActionResult Delete(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            _context.Expenses.Remove(expense);
            try
            {
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    // Выводите информацию об ошибке или обрабатывайте её
                    Console.WriteLine(innerException.Message);
                    innerException = innerException.InnerException;
                }

                // Возвращайте представление с ошибкой или выполняйте другую логику
                return View("Error");
            }

        }

        public IActionResult ExpensesByCategory()
        {
            var expensesByCategory = _context.Expenses
                .Include(e => e.Category)
                .GroupBy(e => e.Category.Name)
                .Select(g => new ExpenseCategoryViewModel
                {
                    CategoryName = g.Key,
                    TotalCost = g.Sum(e => e.Cost),
                    Expenses = g.ToList()
                })
                .ToList();

            return View(expensesByCategory);
        }

        public IActionResult MonthlyStatistics()
        {
            // Получаем список расходов за все месяцы
            var allExpenses = _context.Expenses
                .Include(e => e.Category)
                .ToList();

            // Группируем расходы по месяцам
            var groupedExpenses = allExpenses.GroupBy(e => new { e.Date.Month, e.Date.Year });

            // Создаем модель представления
            var viewModel = new List<MonthlyStatisticsViewModel>();

            // Для каждого месяца создаем статистику и добавляем в модель представления
            foreach (var group in groupedExpenses)
            {
                var monthlyStatistics = new MonthlyStatisticsViewModel
                {
                    Month = new DateTime(group.Key.Year, group.Key.Month, 1).ToString("MMMM yyyy"),
                    CategoryStatistics = group.GroupBy(e => e.Category)
                                              .Select(g => new CategoryStatisticsViewModel
                                              {
                                                  CategoryName = g.Key.Name,
                                                  TotalCost = g.Sum(e => e.Cost)
                                              })
                                              .ToList()
                };

                viewModel.Add(monthlyStatistics);
            }

            return View(viewModel);
        }



    }

}
