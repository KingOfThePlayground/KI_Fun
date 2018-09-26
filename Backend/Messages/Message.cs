using System.Collections.Concurrent;

namespace KI_Fun.Backend.Messages
{
    abstract class Message
    {
        private static ConcurrentQueue<Message> logQueue;
        public static ConcurrentQueue<Message> LogQueue { set { if (logQueue == null) logQueue = value; } }

        public MessageType MessageType { get; private set; }

        public Message(MessageType messageType)
        {
            MessageType = messageType;
            logQueue.Enqueue(this);
        }

        public override string ToString()
        {
            return MessageType.ToString();
        }
    }

    enum MessageType { Military=0x10, ArmyCompleted, Battle, SiegeStarted, SiegeSuccessful, SiegePaused, SiegeBroken, Diplomatic=0x1, PeaceOffer, PeaceOfferAccepted, MarchAccessRequest, MarchAccessAccepted, OwnMarchAccessRevoked, OtherMarchAccessRevoked, WarDeclaration}
}
