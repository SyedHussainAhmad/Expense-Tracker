﻿using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public decimal MonthlyIncome { get; set; }
    }
}
