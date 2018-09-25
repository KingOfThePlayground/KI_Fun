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

        public bool TrySendArmy(ArmyApi armyApi, Direction direction)
        {
            if (IsArmyMovePossible(armyApi, direction))
            {
                Army army = (Army)armyApi.Inner;
                army.MoveQueue.Enqueue(direction);
                return true;
            }
            else
            return false;
        }

        public void EnqueMarchOrder(ArmyApi armyApi, Direction direction)
        {
            Army army = (Army)armyApi.Inner;
            army.MoveQueue.Enqueue(direction);
        }

        public bool TryGetMoveTarget(ArmyApi armyApi, Direction direction, out Province target)
        {
            return _game.TryGetMoveTarget(((Army)armyApi.Inner).InProvince, direction, out target);
        }

        public bool IsArmyMovePossible(ArmyApi armyApi, Direction direction)
        {
            Army army = armyApi.Inner as Army;
            if (_game.TryGetMoveTarget(army.InProvince, direction, out Province target))
                return _game.IsArmyAllowedInProvince(army, target);
            else
                return false;
        }
    }
}
