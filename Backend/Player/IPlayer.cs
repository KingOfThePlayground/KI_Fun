using KI_Fun.Backend.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.Player
{
    interface IPlayer
    {
        void MakeMove(GameAPI api);
    }
}
