using KI_Fun.Backend.API;
using KI_Fun.Backend.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend
{
    class Siege : Wrapped<SiegeApi>
    {
        public Siege(Country besieger, Province province)
        {
            Api = new SiegeApi(this);
            Province = province;
            Besieger = Owner = besieger;
            Duration = 0;
        }

        public override bool IsNeighbouring(BasePlayer player)
        {
            return IsNeighbouring(player, Province.X, Province.Y);
        }

        public Province Province { get; private set; }
        public Country Besieger { get; private set; }
        public double Duration { get; private set; }

        public void AddTime(double timePassed)
        {
            Duration += timePassed;
        }
    }
}
