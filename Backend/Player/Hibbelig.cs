using KI_Fun.Backend.API;
using System.Collections.Generic;

namespace KI_Fun.Backend.Player
{
    class Hibbelig : BasePlayer
    {
        public override void MakeMove(GameApi api)
        {
            IEnumerable<ArmyApi> armies = api.Country.Armies;
            foreach (ArmyApi a in armies)
            {
                if (a.MovingDirection == Direction.None)
                {
                    if (a.IsArmyMovePossible(this, Direction.East))
                    {
                        a.TrySendArmy(this, Direction.East);
                    }
                    else if (a.IsArmyMovePossible(this, Direction.North))
                    {
                        a.TrySendArmy(this, Direction.North);
                    }
                    else if (a.IsArmyMovePossible(this, Direction.West))
                    {
                        a.TrySendArmy(this, Direction.West);
                    }
                    else if (a.IsArmyMovePossible(this, Direction.South))
                    {
                        a.TrySendArmy(this, Direction.South);
                    }
                }
            }
        }
    }
}
