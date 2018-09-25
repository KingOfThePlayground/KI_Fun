using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend
{
    class Army
    {
        const double MOVING_PROGRESS_NEEDED = 1d;
        public const double ARMY_SPEED = 0.1d;
        int _size;
        Province _current;
        Direction _movingDirection;
        double _movingProgress;

        Country _ownerCountry;
        Province _inProvince;

        public void Move(double speed)
        {

        }
    }
}
