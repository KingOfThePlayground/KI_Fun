using KI_Fun.Backend.Player;
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

        public bool TryGetArmies(BasePlayer player, out ApiCollection<ArmyApi> armies)
        {
            if (_province.IsNeighbouring(player))
            {
                armies = new ApiCollection<ArmyApi>(_province.ArmiesInProvince);
                return true;
            }
            armies = null;
            return false;
        }

        public ProvinceApi(Province inner) : base(inner)
        {
            _province = inner;
        }
    }
}
