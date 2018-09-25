using KI_Fun.Backend.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend
{
    class Country
    {
        double _money=0;
        HashSet<Province> _provinces;
        public HashSet<Province> Provinces { get => _provinces; }

        public HashSet<Army> Armies { get; protected set; }
        IPlayer _ownerPlayer;
        public IPlayer Owner { get => _ownerPlayer; }

        public Country(IPlayer owner)
        {
            _provinces = new HashSet<Province>();
            _ownerPlayer = owner;
            Armies = new HashSet<Army>();
        }
    }
}
