using KI_Fun.Backend.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend
{
    class Country : Wrapped
    {
        double _money=0;
        public HashSet<Province> CountryProvinces { get; private set; }
        public Province[,] AllProvinces { get; private set; }

        public HashSet<Army> Armies { get; protected set; }
        public BasePlayer Player { get; private set; }

        public HashSet<Country> War { get; protected set; }
        public HashSet<Country> MarchAccess { get; protected set; }

        public Country(Player.BasePlayer player, Province[,] allProvinces)
        {
            AllProvinces = allProvinces;
            CountryProvinces = new HashSet<Province>();
            Player = player;
            Owner = this;
            Armies = new HashSet<Army>();
            War = new HashSet<Country>();
            MarchAccess = new HashSet<Country>();
            MarchAccess.Add(this);
            Api = new API.CountryApi(this);
        }
    }
}
