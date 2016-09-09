using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.Common;
using Food.Model;

namespace Food.Dal
{
    public class DiaryContext : DbContext
    {
        public DiaryContext() : base (Settings.ConnectionString)
        {
            
        }

        public DbSet<Diary> Diaries { get; set; }

        public DbSet<DiaryEntry> DiaryEntries { get; set; }
    }
}
