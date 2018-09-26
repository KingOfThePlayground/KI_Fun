using KI_Fun.Backend.API;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KI_Fun.Backend.Messages;

namespace KI_Fun.Backend.Player
{
    abstract class BasePlayer
    {
        static int counter = 0;
        private string _name;

        public ConcurrentQueue<Message> MessageQueue { get; private set; }

        public BasePlayer()
        {
            _name = this.GetType().Name + counter++;
            MessageQueue = new ConcurrentQueue<Message>();
        }

        public abstract void MakeMove(GameApi api);

        public override string ToString()
        {
            return _name;
        }
    }
}
