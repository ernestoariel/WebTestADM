﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Services
{
    public interface IFoodService
    {
        IEnumerable<Core.Food> GetAll();
    }
}