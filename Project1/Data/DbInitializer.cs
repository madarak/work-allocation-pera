using Project1.Models;
using System;
using System.Linq;

namespace Project1.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AllocationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Courses.Any())
            {
                return;
            }
        }
    }
}