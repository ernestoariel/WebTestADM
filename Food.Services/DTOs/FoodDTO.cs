using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Services.DTOs
{
    public class FoodDTO
    {
        public int FoodId { get; set; }
        public string Description { get; set; }
        public ICollection<MeasureDTO> MeasureDtos { get; set; }
    }
}
