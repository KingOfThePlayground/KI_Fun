using KI_Fun.Backend.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.Messages
{
    class Battle : Message
    {
        public ProvinceApi Province { get; private set; }
        public ApiCollection<ArmyApi> FriendlyArmies { get; private set; }
        public ApiCollection<ArmyApi> EnemeyArmies { get; private set; }
        public int FriendlyLosses { get; private set; }
        public int EnemyLosses { get; private set; }

        public Battle(ProvinceApi province, ApiCollection<ArmyApi> friendlyArmies, ApiCollection<ArmyApi> enemeyArmies, int friendlyLosses, int enemyLosses) : base(MessageType.Battle)
        {
            Province = province;
            FriendlyArmies = friendlyArmies;
            EnemeyArmies = enemeyArmies;
            FriendlyLosses = friendlyLosses;
            EnemyLosses = enemyLosses;
        }
    }
}
