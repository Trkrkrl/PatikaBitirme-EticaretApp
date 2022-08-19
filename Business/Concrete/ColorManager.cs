using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.SeriLog.Logger;
using Core.Utilities.MessageBrokers.RabbitMQ;
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
    public class ColorManager:IColorService
    {
        private readonly IColorDal _colorDal;
       
        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
            
        }

        [ValidationAspect(typeof(ColorValidator))]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IColorService.Get")]
        [LogAspect(typeof(FileLogger))]


        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessDataResult<Color>(Messages.ColorAdded);
        }
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IColorService.Get")]
        [LogAspect(typeof(FileLogger))]


        public IResult Delete(Color color)
        {
            _colorDal.Delete(color);
            return new Result(true, Messages.ColorDeleted);
        }



        [SecuredOperation("admin")]
        [CacheRemoveAspect("IColorService.Get")]
        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new Result(true, Messages.ColorUpdated);
        }

        [CacheAspect]
        [LogAspect(typeof(FileLogger))]

        public IDataResult<List<Color>> GetAll()
        {
           

            return new DataResult<List<Color>>(_colorDal.GetAll(), true, Messages.ColorsListed);
        }
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]

        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.ColorId == colorId));
        }

    }
}
