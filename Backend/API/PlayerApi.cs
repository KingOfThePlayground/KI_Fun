using KI_Fun.Backend.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.API
{
    class PlayerApi : Api
    {
        public PlayerApi(Wrapped inner) : base(inner)
        {
        }

        public override string ToString()
        {
            return _inner.ToString();
        }
    }
}
