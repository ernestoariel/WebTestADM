using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Core
{
    /// <summary>
    /// the diet in one day
    /// </summary>
    public class Diary
    {
        [Key]
        public int DiaryId { get; set; }
        public DateTime CurrentDate { get; set; }
        public ApiUser ApiUser { get; set; }

        public virtual ICollection<DiaryEntry> DiaryEntries { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
