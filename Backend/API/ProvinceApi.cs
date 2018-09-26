using KI_Fun.Backend.Player;
using System;

namespace KI_Fun.Backend.API
{
    class ProvinceApi : Api
    {
        public const double ARMY_BUILDING_SPEED = Province.ARMY_BUILDING_SPEED;
        public const double ARMY_BUILDING_COST = Province.ARMY_BUILDING_COST;
        Province _province { get => (Province)_inner; }

        public int X { get => _province.X; }
        public int Y { get => _province.Y; }
        public CountryApi Owner { get => _province.Owner.Api; }

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

        public double GetArmyBuildProgress(BasePlayer player)
        {
            if (_inner.Owner.Player != player && !_province.IsNeighbouring(player))
                throw new AccessViolationException("Zugriff auf fremde Provinz.");
            else
                return _province.ArmyBuildingProgress;
        }

        public bool TryBuildArmy(BasePlayer player)
        {
            if (_inner.Owner.Player != player)
                throw new AccessViolationException("Zugriff auf fremde Provinz.");

            if (_province.IsBuildingArmy || _province.Owner.Money < ARMY_BUILDING_COST)
                return false;

            _province.IsBuildingArmy = true;
            _province.Owner.Money -= ARMY_BUILDING_COST;

            return true;
        }

        public ProvinceApi(Province inner) : base(inner)
        {
        }
    }
}
