using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.SeriLog.Logger;
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
        [CacheRemoveAspect("ICategoryService.Get")]
        [ValidationAspect(typeof(CategoryValidator))]
        [SecuredOperation("admin")]
        [LogAspect(typeof(FileLogger))]

        public IResult Add(Category category)
        {
            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }
        [CacheRemoveAspect("ICategoryService.Get")]
        [SecuredOperation("admin")]
        [LogAspect(typeof(FileLogger))]

        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new Result(true, Messages.CategoryDeleted);
        }
        [CacheRemoveAspect("ICategoryService.Get")]
        [ValidationAspect(typeof(CategoryValidator))]
        [SecuredOperation("admin")]
        [LogAspect(typeof(FileLogger))]

        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new Result(true,Messages.CategoryUpdated);
        }
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]

        public IDataResult<List<Category>> GetAll()
        {
            return new DataResult<List<Category>>(_categoryDal.GetAll(), true, Messages.CategoriesListed);
        }
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]

        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(b => b.CategoryId == categoryId),Messages.CategoriesListed);
        }

        
    }
}
