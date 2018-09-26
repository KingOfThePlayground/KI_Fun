using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.API
{
    class ProvinceApi : Api
    {
        Province _province;

        public int X { get => _province.X; }
        public int Y { get => _province.Y; }
        public CountryApi Owner{ get => (CountryApi)_province.Owner.Api; }


        public ProvinceApi(Province inner) : base(inner)
        {
            _province = inner;
        }
    }
}
