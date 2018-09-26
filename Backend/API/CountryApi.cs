using System;
using System.Collections.Generic;

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

        public int FieldWidth { get => _country.AllProvinces.GetLength(0); }
        public int FieldHeight { get => _country.AllProvinces.GetLength(1); }

        public ProvinceApi GetProvinceAt(int x, int y)
        {
            if (x >= 0 && x < FieldWidth && y >= 0 && y < FieldHeight)
                return (ProvinceApi)_country.AllProvinces[x, y].Api;
            else
                throw new ArgumentOutOfRangeException("Die Koordinaten sind außerhalb der Karte.");
        }
    }
}
