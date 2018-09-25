using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.API
{
    class CountryApi : Api
    {
        Country _country;

        public IEnumerable<ArmyApi> Armies { get => new ApiCollection<ArmyApi>(_country.Armies); }
        public CountryApi(Country inner) : base(inner)
        {
            _country = inner;
        }
    }
}
