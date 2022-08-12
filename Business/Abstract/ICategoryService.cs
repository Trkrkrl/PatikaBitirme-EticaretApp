using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll();


        IDataResult<Category> GetById(int categoryId);
        IResult Add(Category brand);
        IResult Update(Category brand);
        IResult Delete(Category brand);

    }
}
