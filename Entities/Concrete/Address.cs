using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Address : IEntity
    {
        public int AddressId { get; set; }
        public string AdressName { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Neighborhood { get; set; }
        public string Avenue { get; set; }
        public string Street { get; set; }
        public int BuildingNo { get; set; }

        public int ApartmentNo { get; set; }
        public int UserId { get; set; }

    }
}
