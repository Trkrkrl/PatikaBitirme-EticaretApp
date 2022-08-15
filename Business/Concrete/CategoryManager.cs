using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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

        [ValidationAspect(typeof(CategoryValidator))]

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
        [ValidationAspect(typeof(CategoryValidator))]

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
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
