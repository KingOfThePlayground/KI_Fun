using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.API
{
    class GameApi : Api
    {
        private Game _game;

        public GameApi(Game game) : base(game)
        {
            _game = game;
        }

        public bool IsArmyMoveAllowed(ArmyApi armyApi, Direction direction)
        {
            Army army = armyApi.Inner as Army;
            return false;
        }
    }
}
