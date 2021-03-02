using System;
using System.Collections.Generic;
using System.Text;
using SulsApp.Models;

namespace SulsApp.Services
{
   public class ProblemsService:IProblemsService
    {
        private readonly ApplicationDbContext db;

        public ProblemsService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateProblem(string name, int points)
        {
            var problem = new Problem()
            {
                Name = name,
                Points = points
            };

            this.db.Problems.Add(problem);
            this.db.SaveChanges();
        }
    }
}
