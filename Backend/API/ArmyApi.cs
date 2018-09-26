using KI_Fun.Backend.Player;
using System;

namespace KI_Fun.Backend.API
{
    class ArmyApi : Api
    {
        Army _army { get => (Army)_inner; }
        public ArmyApi(Army inner) : base(inner)
        {
        }

        public bool TryGetMovingDirection(BasePlayer player, out Direction result)
        {
            if (_army.Owner.Player == player)
            {
                result = _army.MovingDirection;
                return true;
            }
            else
            {
                result = Direction.None;
                return false;
            }
        }

        public bool TrySendArmy(BasePlayer player, Direction direction)
        {
            if (_army.Owner.Player != player)
                throw new AccessViolationException("Zugriff auf fremde Armee");

            if (IsArmyMovePossible(player, direction))
            {
                _army.MovingDirection = direction;
                _army.MovingProgress = 0d;
                return true;
            }
            else
                return false;
        }

        public void EnqueMarchOrder(BasePlayer player, Direction direction)
        {
            if (_army.Owner.Player != player)
                throw new AccessViolationException("Zugriff auf fremde Armee");
            _army.MoveQueue.Enqueue(direction);
        }

        public bool TryGetMoveTarget(BasePlayer player, Direction direction, out ProvinceApi targetApi)
        {
            if (_army.Owner.Player != player)
                throw new AccessViolationException("Zugriff auf fremde Armee");
            if (_army.TryGetMoveTarget(direction, out Province target))
            {
                targetApi = target.Api;
                return true;
            }
            else
            {
                targetApi = null;
                return false;
            }
        }

        public bool IsArmyMovePossible(BasePlayer player, Direction direction)
        {
            if (!(_army.Owner.Player != player || _army.IsNeighbouring(player)))
                throw new AccessViolationException("Unerlaubte Anfrage");

            if (_army.TryGetMoveTarget(direction, out Province target))
                return _army.IsArmyAllowedInProvince(target);
            else
                return false;
        }

        public Direction MovingDirection { get => _army.MovingDirection; }
        public double MovingProgress { get => _army.MovingProgress; }
        public int Size { get => _army.Size; }
        public int X { get => _army.InProvince.X; }
        public int Y { get => _army.InProvince.Y; }
    }
}
