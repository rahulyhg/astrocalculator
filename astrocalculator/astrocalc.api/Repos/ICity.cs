﻿using astrocalc.api.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace astrocalc.api.Repos
{
    public interface ICity:IGet<City>, IQueryable
    {
    }
}
