using KI_Fun.Backend.API;
using KI_Fun.Backend.Player;
using System;
using System.Collections.Generic;

namespace KI_Fun.Backend
{
    class Province : Wrapped<ProvinceApi>
    {
        public HashSet<Army> ArmiesInProvince { get; private set; }

        readonly int _x;
        readonly int _y;

        public Siege Siege { get; set; }

        public int X { get => _x; }

        public int Y { get => _y; }

        public override bool IsNeighbouring(BasePlayer player)
        {
            return IsNeighbouring(player, X, Y);
        }

        public Province(int x, int y)
        {
            _x = x;
            _y = y;
            ArmiesInProvince = new HashSet<Army>();
            Api = new ProvinceApi(this);
            Siege = null;
        }
    }
}
