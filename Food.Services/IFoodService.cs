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
        IEnumerable<FoodDTO> Search(int? foodId = null, int page = 0, int pageSize = 5, bool inDiaryEntries = false);
        int AddFood(FoodDTO foodDto);
        int UpdateFood(FoodDTO foodDto);
    }
}
