using KI_Fun.Backend.API;
using KI_Fun.Backend.Player;
using System.Collections.Generic;

namespace KI_Fun.Backend
{
    class Province : Wrapped<ProvinceApi>
    {
        public const double ARMY_BUILDING_SPEED = 0.02d;
        public const double ARMY_BUILDING_COST = 500d;

        public HashSet<Army> ArmiesInProvince { get; private set; }

        readonly int _x;
        readonly int _y;

        public int X { get => _x; }

        public int Y { get => _y; }

        public double ArmyBuildingProgress { get; private set; }
        private bool _isBuildingArmy;
        public bool IsBuildingArmy { get => _isBuildingArmy; set { _isBuildingArmy = value; ArmyBuildingProgress = 0d; } }

        public override bool IsNeighbouring(BasePlayer player)
        {
            return IsNeighbouring(player, Owner.AllProvinces, X, Y);
        }

        public Province(Game game, int x, int y) : base(game)
        {
            _x = x;
            _y = y;
            ArmiesInProvince = new HashSet<Army>();
            Api = new ProvinceApi(this);
        }


        public bool ProcessArmyBuilding()
        {
            if (!IsBuildingArmy)
                return false;
            ArmyBuildingProgress += ARMY_BUILDING_SPEED;
            if (ArmyBuildingProgress >= 1)
            {
                IsBuildingArmy = false;
                return true;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return (X, Y).ToString();
        }
    }
}
