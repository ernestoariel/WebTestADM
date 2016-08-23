using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Model
{
    public class DiaryEntry
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Diary Diary { get; set; }
        public Food Food { get; set; }
        public Measure Measure { get; set; }
    }
}
