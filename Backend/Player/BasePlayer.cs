using KI_Fun.Backend.API;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.Player
{
    abstract class BasePlayer
    {
        static int counter = 0;
        private string _name;

        public ConcurrentQueue<Message> MessageQueue;

        public BasePlayer()
        {
            _name = this.GetType().Name + counter++;
        }

        public abstract void MakeMove(GameApi api);

        public Country Country { get; set; }

        public override string ToString()
        {
            return _name;
        }
    }
}
