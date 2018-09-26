using KI_Fun.Backend.API;
using KI_Fun.Backend.Messages;
using KI_Fun.Backend.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend
{
    class Country : Wrapped<CountryApi>
    {
        public const double ARMY_COST_PER_SOLDIER = 0.5d;
        public const double PROVINCE_REVENUE = 200d;

        public double Money { get; set; }
        public HashSet<Province> CountryProvinces { get; private set; }
        public Province[,] AllProvinces { get => _game.Provinces; }

        public HashSet<Army> Armies { get; protected set; }
        public BasePlayer Player { get; private set; }

        public HashSet<Country> War { get; protected set; }
        public HashSet<Country> MarchAccess { get; protected set; }

        public Country(Game game, Player.BasePlayer player) : base(game)
        {
            CountryProvinces = new HashSet<Province>();
            Player = player;
            Owner = this;
            Armies = new HashSet<Army>();
            War = new HashSet<Country>();
            MarchAccess = new HashSet<Country>();
            MarchAccess.Add(this);
            Api = new API.CountryApi(this);
            Money = 1000d;
        }

        public bool IsAllowedInCountry(Country provinceOwnerCountry)
        {
            return MarchAccess.Contains(provinceOwnerCountry) || War.Contains(provinceOwnerCountry);
        }

        public void CalculateMoney()
        {
            foreach (Army a in Armies)
            {
                Money -= a.Size * ARMY_COST_PER_SOLDIER;
            }

            Money += CountryProvinces.Count * PROVINCE_REVENUE;
        }

        public bool RetrieveOffer(DiplomaticMessage offer)
        {
            return _game.RetrieveOffer(offer);
        }

        public override string ToString()
        {
            return Player.ToString();
        }
    }
}
