﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.Messages
{
    class ArmyCompleted : Message
    {
        public ArmyCompleted() : base(MessageType.ArmyCompleted)
        {
        }
    }
}