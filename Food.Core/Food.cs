using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Food.Core
{
    public class Food
    {
        
        [Key]
        public int FoodId { get; set; }
        
        [Index("IX_Food_Description", IsClustered = false, IsUnique = true)]
        [StringLength(100)]
        public string Description { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual ICollection<Measure> Measures { get; set; }
    }
}
