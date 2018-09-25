using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KI_Fun.Backend.API;

namespace KI_Fun.Backend.Player
{
    class DummyPlayer : IPlayer
    {
        static int counter = 1;
        public Country Country { get; set; }

        String _name;

        public DummyPlayer()
        {
            _name = "Player " + counter++;
        }
        public void MakeMove(GameApi api)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
