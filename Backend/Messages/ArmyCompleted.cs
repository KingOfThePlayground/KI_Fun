using KI_Fun.Backend.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.Messages
{
    class ArmyCompleted : Message
    {
        public ProvinceApi Province { get; private set; }
        public ArmyApi Army { get; private set; }

        public ArmyCompleted(Province province, Army army) : base(MessageType.ArmyCompleted)
        {
            Province = province.Api;
            Army = army.Api;
        }

        public override string ToString()
        {
            return $"In der Provinz {Province} wurde eine Armee fertiggestellt";
        }
    }
}
