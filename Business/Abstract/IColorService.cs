﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IColorService
    {
        IDataResult<List<Color>> GetAll();
        IResult Add(Color color);
        IResult Delete(int colorId);
        IResult Update(Color color);

    }
}
