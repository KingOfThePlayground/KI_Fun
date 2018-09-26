using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KI_Fun.Backend.API;
using KI_Fun.Backend.Player;

namespace KI_Fun.Backend
{
    class Army : Wrapped<ArmyApi>
    {
        public const double MOVING_PROGRESS_NEEDED = 1d;
        public const double ARMY_SPEED = 0.02d;

        public Army(Game game, int size, Country country) : base(game)
        {
            Size = size;
            Owner = country;
            MoveQueue = new Queue<Direction>();
            Api = new ArmyApi(this);
        }

        public bool BlackFlagged { get; set; }
        public int Size { get; set; }
        public Direction MovingDirection { get; set; }
        public double MovingProgress { get; set; }
        public Queue<Direction> MoveQueue { get; private set; }

        public Province InProvince { get; set; }
        public int X { get => InProvince.X; }
        public int Y { get => InProvince.Y; }

        public override bool IsNeighbouring(BasePlayer player)
        {
            return IsNeighbouring(player, Owner.AllProvinces, X, Y);
        }

        public bool TryGetMoveTarget(Direction direction, out Province target)
        {
            return _game.TryGetMoveTarget(InProvince, direction, out target);
        }

        public bool IsArmyAllowedInProvince(Province target)
        {
            return BlackFlagged || Owner.IsAllowedInCountry(target.Owner);
        }

        public void ProgressMove()
        {
            if (MoveQueue.Count != 0 && MovingDirection == Direction.None)
                MovingDirection = MoveQueue.Dequeue();
            if (MovingDirection == Direction.None)
                return;
            MovingProgress += ARMY_SPEED;
            BlackFlagged = false;
        }
    }
}
