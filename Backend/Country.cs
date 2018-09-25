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
        double _money;
        HashSet<Province> _provinces;
        HashSet<Army> _armies;
        IPlayer _ownerPlayer;
        public IPlayer Player { get => _ownerPlayer; }
    }
}
