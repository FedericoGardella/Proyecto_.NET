﻿using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.IBLs
{
    public interface IBL_TiposSeguros
    {
        TipoSeguro Get(long Id);
        List<TipoSeguro> GetAll();
        TipoSeguro Add(TipoSeguro x);
        TipoSeguro Update(TipoSeguro x);
        void Delete(long Id);
    }
}
