using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend
{
    class Army
    {
        public const double MOVING_PROGRESS_NEEDED = 1d;
        public const double ARMY_SPEED = 0.1d;
        public int _size { get; set; }
        public Direction MovingDirection { get; set; }
        public double MovingProgress { get; set; }
        public Queue<Direction> MoveQueue { get; private set; }
        
        public Country OwnerCountry { get; private set; }
        public Province InProvince { get; set; }

        public void ProgressMove()
        {
            if (MoveQueue.Count != 0 && MovingDirection == Direction.None)
                MovingDirection = MoveQueue.Dequeue();
            if (MovingDirection == Direction.None)
                return;
            MovingProgress += ARMY_SPEED;
        }
    }
}
