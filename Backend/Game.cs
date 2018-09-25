using KI_Fun.Backend.API;
using KI_Fun.Backend.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend
{
    class Game : Wrapped
    {
        List<IPlayer> _players;

        Province[,] _provinces;
        public Province[,] Provinces { get => _provinces; }

        List<Country> _countries;
        public HashSet<Army> Armies { get; protected set; }

        int _fieldSize;
        public int FieldWidth { get => _fieldSize; }

        public int FieldHeight { get => _fieldSize; }

        public Game(List<IPlayer> players, int fieldSize)
        {
            Api = new GameApi(this);
            _players = players;
            _fieldSize = fieldSize;

            setupCountries();
            setupProvinces();
        }

        private void setupCountries()
        {
            _countries = new List<Country>();

            for (int i = 0; i < _players.Count; i++)
            {
                _countries.Add(new Country(_players[i]));
                _players[i].Country = _countries[i];
            }
        }

        private void setupProvinces()
        {
            Armies = new HashSet<Army>();
            _provinces = new Province[FieldWidth, FieldHeight];

            bool[] countryHasArmy = new bool[_players.Count];
            Province p;
            Random rand = new Random();

            for (int y = 0; y < FieldHeight; y++)
                for (int x = 0; x < FieldWidth; x++)
                {
                    p = new Province(x, y);
                    _provinces[x, y] = p;
                    int ownerNum = rand.Next(0, _players.Count);
                    p.Owner = _countries[ownerNum];
                    _countries[ownerNum].Provinces.Add(p);
                    if (!countryHasArmy[ownerNum])
                    {
                        CreateArmy(_countries[ownerNum], p, 100);
                        countryHasArmy[ownerNum] = true;
                    }
                }
        }

        public void Tick()
        {
            foreach (IPlayer p in _players)
                p.MakeMove((GameApi)(Api));

            foreach(Army a in Armies)
            {
                moveArmy(a);
            }
        }

        public void CreateArmy(Country country, Province province, int size)
        {
            Army army = new Army(size, country);
            army.InProvince = province;
            province.ArmiesInProvince.Add(army);
            country.Armies.Add(army);
        }

        private (int x, int y) moveTargetCoordinates(Province province, Direction direction)
        {
            switch (direction)
            {
                case Direction.East:
                    return (province.X - 1, province.Y);

                case Direction.West:
                    return (province.X + 1, province.Y);

                case Direction.North:
                    return (province.X, province.Y-1);

                case Direction.South:
                    return (province.X, province.Y+1);

                default:
                    return (province.X, province.Y);
            }
        }

        public bool TryGetMoveTarget(Province province, Direction direction, out Province target)
        {
            (int x, int y) = moveTargetCoordinates(province, direction);
            if (x >= 0 && x < FieldWidth && y>= 0 && y<FieldHeight)
            {
                target = _provinces[x, y];
                return true;
            }
            else
            {
                target = null;
                return false;
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
