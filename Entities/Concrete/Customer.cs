using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Customer:IEntity
    {
        [Key]
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public string ContactName { get; set; }
        public int AdressId { get; set; }

    }
}
