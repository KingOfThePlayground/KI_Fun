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
                    if (api.IsArmyMovePossible(a, Direction.East))
                    {
                        api.TrySendArmy(a, Direction.East);
                    }
                    else if (api.IsArmyMovePossible(a, Direction.North))
                    {
                        api.TrySendArmy(a, Direction.North);
                    }
                    else if (api.IsArmyMovePossible(a, Direction.West))
                    {
                        api.TrySendArmy(a, Direction.West);
                    }
                    else if (api.IsArmyMovePossible(a, Direction.South))
                    {
                        api.TrySendArmy(a, Direction.South);
                    }
                }
            }
        }
    }
}
