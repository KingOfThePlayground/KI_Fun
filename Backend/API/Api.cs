using KI_Fun.Backend.Player;

namespace KI_Fun.Backend.API
{
    abstract class Api
    {
        protected Wrapped _inner;

        public Api(Wrapped inner)
        {
            _inner = inner;
        }

        public bool IsOwnerPlayer(BasePlayer player)
        {
            return _inner.Owner.Player == player;
        }
    }
}
