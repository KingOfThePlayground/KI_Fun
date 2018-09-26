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
        HashSet<Province> _provinces;
        public HashSet<Province> Provinces { get => _provinces; }

        public HashSet<Army> Armies { get; protected set; }
        BasePlayer _player;
        public BasePlayer Player { get => _player; }

        public HashSet<Country> War { get; protected set; }
        public HashSet<Country> MarchAccess { get; protected set; }

        public Country(Player.BasePlayer player)
        {
            _provinces = new HashSet<Province>();
            _player = player;
            Owner = this;
            Armies = new HashSet<Army>();
            War = new HashSet<Country>();
            MarchAccess = new HashSet<Country>();
            MarchAccess.Add(this);
            Api = new API.CountryApi(this);
        }
    }
}
