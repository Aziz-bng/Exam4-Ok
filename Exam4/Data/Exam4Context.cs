using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Exam4.Models;

namespace Exam4.Data
{
    public class Exam4Context : DbContext
    {
        public Exam4Context (DbContextOptions<Exam4Context> options)
            : base(options)
        {
        }

        public DbSet<Exam4.Models.Exams> Exams { get; set; } = default!;
        public DbSet<Exam4.Models.Questions> Questions { get; set; } = default!;
        public DbSet<Exam4.Models.Options> Options { get; set; } = default!;
    }
}
