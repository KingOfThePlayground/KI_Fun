using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KI_Fun.Backend.API;

namespace KI_Fun.Backend.Player
{
    class DummyPlayer : BasePlayer
    {
        public Country Country { get; set; }

        public void MakeMove(GameApi api)
        {
            throw new NotImplementedException();
        }
    }
}
