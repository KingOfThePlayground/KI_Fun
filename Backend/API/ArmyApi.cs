using KI_Fun.Backend.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.API
{
    class ArmyApi : Api
    {
        Army _army;
        public ArmyApi(Army inner) : base(inner)
        {
            _army = (Army)inner;
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

        public Direction MovingDirection { get => _army.MovingDirection; }
        public double MovingProgress { get => _army.MovingProgress; }
        public int Size { get => _army.Size; }
        public int X { get => _army.InProvince.X; }
        public int Y { get => _army.InProvince.Y; }
    }
}
