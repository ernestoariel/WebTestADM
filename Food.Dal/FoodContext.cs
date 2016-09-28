using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.Common;
using Food.Core;
using Food.Dal.TestData;
using Food = Food.Core.Food;

namespace Food.Dal
{
    public class FoodContext : DbContext
    {
        public FoodContext() : base(Settings.ConnectionString)
        {
        }

        public DbSet<Core.Food> Foods { get; set; }

        public DbSet<Measure> Measures { get; set; }

        public DbSet<Diary> Diaries { get; set; }

        public DbSet<DiaryEntry> DiaryEntries { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Measure>()
                .HasRequired( c=>c.Food)
                .WithMany( p => p.Measures)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DiaryEntry>()
                .HasRequired( c => c.Food)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DiaryEntry>()
                .HasRequired( c => c.Measure)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
