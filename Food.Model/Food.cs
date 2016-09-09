using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Model
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }
        public string Description { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
