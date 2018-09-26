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

        public Battle(Province province, IEnumerable<Army> friendlyArmies, IEnumerable<Army> enemeyArmies, int friendlyLosses, int enemyLosses) : base(MessageType.Battle)
        {
            Province = province.Api;
            FriendlyArmies = new ApiCollection<ArmyApi> (friendlyArmies);
            EnemeyArmies = new ApiCollection<ArmyApi>(enemeyArmies);
            FriendlyLosses = friendlyLosses;
            EnemyLosses = enemyLosses;
        }
    }
}
