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
        List<Player.BasePlayer> _players;

        Province[,] _provinces;
        public Province[,] Provinces { get => _provinces; }

        List<Country> _countries;
        public HashSet<Army> Armies { get; protected set; }

        int _fieldSize;
        public int FieldWidth { get => _fieldSize; }

        public int FieldHeight { get => _fieldSize; }

        public Game(List<Player.BasePlayer> players, int fieldSize)
        {
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
            foreach (Player.BasePlayer p in _players)
                p.MakeMove(new GameApi(this, p));

            foreach(Army a in Armies)
            {
                moveArmy(a);
            }
        }

        public void CreateArmy(Country country, Province province, int size)
        {
            Army army = new Army(size, country);
            army.InProvince = province;
            Armies.Add(army);
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

        public bool IsArmyAllowedInProvince(Army army, Province province)
        {
            return army.BlackFlagged || IsCountryAllowedInCountry(army.OwnerCountry, province.Owner);
        }

        public bool IsCountryAllowedInCountry(Country armyOwnerCountry, Country provinceOwnerCountry)
        {
            return armyOwnerCountry.MarchAccess.Contains(provinceOwnerCountry) || armyOwnerCountry.War.Contains(provinceOwnerCountry);
        }

        private void moveArmy(Army army)
        {
            army.ProgressMove();

            if (army.MovingProgress > Army.MOVING_PROGRESS_NEEDED)
            {
                Province from = army.InProvince;

                TryGetMoveTarget(from, army.MovingDirection, out Province to);

                if (IsArmyAllowedInProvince(army,to))
                {
                    army.InProvince = to;
                    from.ArmiesInProvince.Remove(army);
                    to.ArmiesInProvince.Add(army);
                    if (army.MoveQueue.Count == 0)
                        army.MovingDirection = Direction.None;
                    else
                        army.MovingDirection = army.MoveQueue.Dequeue();
                }
            }
        }

        public BasePlayer GetOwnerOfProvince(int x, int y)
        {
            if (x < 0 || x >= _fieldSize || y < 0 || y >= _fieldSize)
                throw new ArgumentOutOfRangeException("Punkt nicht im Feld enthalten");
            else
                return _provinces[x, y].Owner.Owner;
        }
    }
}
