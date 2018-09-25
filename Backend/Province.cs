using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend
{
    class Province : Wrapped
    {
        Country _owner;
        public Country Owner { get => _owner; set => _owner = value; }

        public HashSet<Army> ArmiesInProvince { get; private set; }

        readonly int _x;
        readonly int _y;

        public int X { get => _x; }

        public int Y { get => _y; }

        public Province(int x, int y)
        {
            _x = x;
            _y = y;
            ArmiesInProvince = new HashSet<Army>();
        }

        public bool ArmyAllowedInProvince(Army army)
        {
            throw new NotImplementedException();
        }
    }
}
