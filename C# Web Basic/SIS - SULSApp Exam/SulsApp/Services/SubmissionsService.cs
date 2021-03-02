using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SulsApp.Models;

namespace SulsApp.Services
{
   public class SubmissionsService:ISubmissionsService
    {
        private readonly ApplicationDbContext db;
        private readonly Random random;
        public SubmissionsService(ApplicationDbContext db,Random random)
        {
            this.db = db;
            this.random = random;
        }
        public void Create(string userId, string problemId, string code)
        {
            var problem = this.db.Problems.FirstOrDefault(x => x.Id == problemId);
            var submission = new Submission()
            {
                UserId = userId,
                ProblemId = problemId,
                Code = code,
                CreatedOn = DateTime.UtcNow,
                AchievedResult = random.Next(0, problem.Points + 1)
            };

            this.db.Submissions.Add(submission);
            this.db.SaveChanges();
        }

        public void Delete(string id)
        {
            var submission = this.db.Submissions.Find(id);
            this.db.Remove(submission);
            this.db.SaveChanges();
        }
    }
}
