﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.DbInitilizer
{
    public interface IDbInializer
    {
        Task Inilialize(); 
    }
}
