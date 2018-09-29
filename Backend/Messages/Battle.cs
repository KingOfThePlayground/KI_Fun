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
        private Country _countryA;
        private Country _countryB;

        public Battle(Province province, List<Army> friendlyArmies, List<Army> enemyArmies, int friendlyLosses, int enemyLosses) : base(MessageType.Battle)
        {
            Province = province.Api;
            FriendlyArmies = new ApiCollection<ArmyApi>(friendlyArmies);
            EnemeyArmies = new ApiCollection<ArmyApi>(enemyArmies);
            FriendlyLosses = friendlyLosses;
            EnemyLosses = enemyLosses;
            _countryA = friendlyArmies[0].Owner;
            _countryB = enemyArmies[0].Owner;
            _countryA.Player.MessageQueue.Enqueue(this);
        }

        public override string ToString()
        {
            return $"In der Provinz {Province} hat eine Schlacht zwischen {_countryA.Player} ({FriendlyLosses} Soldaten verloren) und {_countryB.Player} ({EnemyLosses} Soldaten verloren) stattgefunden.";
        }
    }
}
