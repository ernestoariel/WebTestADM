using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Food.Services.DTOs;

namespace Food.Services
{
    public interface IFoodService
    {
        IEnumerable<FoodDTO> GetAll();
        IEnumerable<FoodDTO> Search(int page, int pageSize, bool inDiaryEntries, int? foodId);
    }
}
