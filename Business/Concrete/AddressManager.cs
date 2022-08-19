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
    
    public class AddressManager : IAddressService
    {
        private readonly IAddressDal _addressDal;

        public AddressManager(IAddressDal addressDal)
        {
            _addressDal = addressDal;
        }
        [LogAspect(typeof(FileLogger))]
        [CacheRemoveAspect("IAddressService.Get")]
        [ValidationAspect(typeof(AddressValidation))]
        public IResult Add(Address address)
        {
            _addressDal.Add(address);
            return new SuccessResult(Messages.AddressAdded);
        }

        [CacheRemoveAspect("IAddressService.Get")]
        [LogAspect(typeof(FileLogger))]

        [ValidationAspect(typeof(AddressValidation))]
        public IResult Update(Address address)
        {
            _addressDal.Update(address);
            return new SuccessResult(Messages.AddressUpdated);
        }

        [CacheRemoveAspect("IAddressService.Get")]
        [LogAspect(typeof(FileLogger))]

        public IResult Delete(Address address)
        {
            _addressDal.Delete(address);
            return new SuccessResult(Messages.AddressDeleted);
        }

        //bu admin rolü istemeli
        [SecuredOperation("admin")]
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]

        public IDataResult<List<Address>> GetAll()
        {
            return new SuccessDataResult<List<Address>>(_addressDal.GetAll());
        }
        [CacheAspect]

        [LogAspect(typeof(FileLogger))]

        public IDataResult<Address> GetByUserId(int userId)
        {
            return new SuccessDataResult<Address>(_addressDal.Get(a=>a.UserId==userId));
        }

        
    }
}
