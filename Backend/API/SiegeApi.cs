using KI_Fun.Backend.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.API
{
    class SiegeApi : Api
    {
        protected Siege _siege {get => (Siege)_inner; }

        public bool TryGetDuration(BasePlayer player, out double duration)
        {
            if (_siege.IsNeighbouring(player))
            {
                duration = _siege.Duration;
                return false;
            }

            duration = 0;
            return false;
        }

        public SiegeApi(Wrapped inner) : base(inner) { }
    }
}
