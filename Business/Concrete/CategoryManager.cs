using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager:ICategoryService
    {   //buralara validator attribute eklenecek unutma=> recapte var
        private readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new Result(true, Messages.CategoryDeleted);
        }
        public IResult Update(Category brand)
        {
            _categoryDal.Update(brand);
            return new Result(true);
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new DataResult<List<Category>>(_categoryDal.GetAll(), true, Messages.CategoriesListed);
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(b => b.CategoryId == categoryId));
        }

        
    }
}
