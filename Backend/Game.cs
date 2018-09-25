using KI_Fun.Backend.API;
using KI_Fun.Backend.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend
{
    class Game
    {
        List<IPlayer> _players;

        Province[,] _provinces;
        public Province[,] Provinces {get => _provinces;}

        List<Country> _countries;
        HashSet<Army> _armies;

        int _fieldSize;
        public int FieldWidth { get => _fieldSize; }

        public int FieldHeight { get => _fieldSize; }

        public Game(List<IPlayer> players, int fieldSize)
        {
            _players = players;
            _countries = new List<Country>();

            for (int i = 0; i < players.Count; i++) {
                _countries.Add(new Country(_players[i]));
                _players[i].Country = _countries[i];
            }

            _provinces = new Province[fieldSize, fieldSize];

            Province p;
            Random rand = new Random();

            for (int y = 0; y < fieldSize; y++)
                for (int x = 0; x < fieldSize; x++)
                {
                    p = new Province(x, y);
                    _provinces[x, y] = p;
                    int ownerNum = rand.Next(0, _players.Count);
                    p.Owner = _countries[ownerNum];
                    _countries[ownerNum].Provinces.Add(p);
                }
        }

        public void Tick()
        {
            foreach (IPlayer p in _players)
                p.MakeMove(new GameAPI(this));

            foreach(Army a in _armies)
            {
                moveArmy(a);
            }
        }

        private void moveArmy(Army army)
        {
            army.ProgressMove();

            if (army.MovingProgress > Army.MOVING_PROGRESS_NEEDED)
            {
                Province from = army.InProvince;
                Province to=null;
                
                switch (army.MovingDirection)
                {
                    case Direction.East:
                        to = _provinces[from.X - 1, from.Y];
                        break;

                    case Direction.West:
                        to = _provinces[from.X - 1, from.Y];
                        break;

                    case Direction.North:
                        to = _provinces[from.X - 1, from.Y];
                        break;

                    case Direction.South:
                        to = _provinces[from.X - 1, from.Y];
                        break;
                }

                if (to.ArmyAllowedInProvince(army))
                {
                    army.InProvince = to;
                    from.ArmiesInProvince.Remove(army);
                    to.ArmiesInProvince.Add(army);
                }
            }
        }

        public IPlayer GetOwnerOfProvince(int x, int y)
        {
            if (x < 0 || x >= _fieldSize || y < 0 || y >= _fieldSize)
                throw new ArgumentOutOfRangeException("Punkt nicht im Feld enthalten");
            else
                return _provinces[x, y].Owner.Owner;
        }
    }
}
