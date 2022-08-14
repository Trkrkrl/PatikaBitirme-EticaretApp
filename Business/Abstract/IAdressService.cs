using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAddressService
    {
        IDataResult<List<Address>> GetAll();
        
        IDataResult<Address> GetByUserId(int userId);
        IResult Add(Address address);
        IResult Delete(Address address);
        IResult Update(Address address);
    }
}
