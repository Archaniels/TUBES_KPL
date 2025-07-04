﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUBES_KPL.API.Services
{
    public interface IGenericService<T>
    {
        List<T> GetAll();
        T? GetById(int id);
        void Update(int id, T item);
    }
}
