using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.Player
{
    abstract class Message
    {
        static ConcurrentQueue<Message> LogQueue = new ConcurrentQueue<Message>();
        public MessageType MessageType { get; private set; }

        public Message(MessageType messageType)
        {
            MessageType = messageType;
        }

        public override string ToString()
        {
            return MessageType.ToString();
        }
    }

    enum MessageType { MarchAccessRequest, WarDeclaration }
}
