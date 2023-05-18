using HW78.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HW78.Data
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> options) : base(options) { }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-GB7F144\\MSSQLSERVER02;Initial Catalog=HW78;Integrated Security=True;TrustServerCertificate=True");
        }
    }

}
