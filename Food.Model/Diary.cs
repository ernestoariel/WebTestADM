using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Model
{
    /// <summary>
    /// the diet in one day
    /// </summary>
    public class Diary
    {
        public int Id { get; set; }
        public DateTime CurrentDate { get; set; }
        public ApiUser ApiUser { get; set; }
    }
}
