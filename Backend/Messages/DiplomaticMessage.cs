using KI_Fun.Backend.API;

namespace KI_Fun.Backend.Messages
{
    class DiplomaticMessage : Message
    {
        protected Country _active;
        public CountryApi Active { get => _passive.Api; }
        protected Country _passive;
        public CountryApi Passive { get => _passive.Api; }

        public DiplomaticMessage(MessageType messageType, Country active, Country passive) : base(messageType)
        {
            _active = active;
            _passive = passive;
            passive.Player.MessageQueue.Enqueue(this);
        }

        public override string ToString()
        {
            switch (MessageType)
            {
                case MessageType.WarDeclaration:
                    return $"{_active.Player} hat {_passive.Player} den Krieg erklärt.";
                case MessageType.PeaceOffer:
                    return $"{_active.Player} hat {_passive.Player} ein Friedensangebot gemacht.";
                case MessageType.PeaceOfferAccepted:
                    return $"{_active.Player} hat ein Friedensangebot von {_passive.Player} angenommen.";
                case MessageType.MarchAccessRequest:
                    return $"{_active.Player} hat {_passive.Player} um militärisches Zugangsrecht gebeten.";
                case MessageType.MarchAccessAccepted:
                    return $"{_active.Player} hat {_passive.Player} militärisches Zugangsrecht erteilt.";
                case MessageType.OtherMarchAccessRevoked:
                    return $"{_active.Player} hat sein militärisches Zugangsrecht bei {_passive.Player} widerrufen.";
                case MessageType.OwnMarchAccessRevoked:
                    return $"{_active.Player} hat das an erteilte {_passive.Player} militärisches Zugangsrecht widerrufen.";
                default:
                    return $"{_active.Player} hat an {_passive.Player} die diplomatische Aktion {MessageType} ausgeführt.";
            }
        }
    }
}
