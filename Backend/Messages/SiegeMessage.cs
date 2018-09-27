using KI_Fun.Backend.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.Messages
{
    class SiegeMessage : Message
    {
        SiegeApi _siege;

        public SiegeMessage(MessageType messageType, Siege siege) : base(messageType)
        {
            _siege = siege.Api;
        }
    }

}
