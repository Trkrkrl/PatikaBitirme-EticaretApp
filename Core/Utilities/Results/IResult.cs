using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    
        public interface IResult//Result ve IDataResultta kullanılacak
        {//Temel voidlerimiz için başlangıç,  service kodlarında voidler IResult ile değişecek

            bool Success { get; }
            string Message { get; }
        }
    
}
