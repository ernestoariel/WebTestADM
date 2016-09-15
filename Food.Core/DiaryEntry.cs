using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Core
{
    public class DiaryEntry
    {
        [Key]
        public int DiaryEntryId { get; set; }
        public int Quantity { get; set; }
        public int DiaryId { get; set; }
        public Diary Diary { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
        public int MeasureId { get; set; }
        public Measure Measure { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
