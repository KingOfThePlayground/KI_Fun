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

        int _fieldSize;
        public int FieldSize { get => _fieldSize; }

        public Game(List<IPlayer> players, int fieldSize)
        {
            _players = players;
            _provinces = new Province[fieldSize, fieldSize];

            Province p;
            Random rand = new Random();

            for (int y = 0; y < fieldSize; y++)
                for (int x = 0; x < fieldSize; x++)
                {
                    p = new Province(x, y);
                    _provinces[x, y] = p;
                }
        }

        public void Tick()
        {
            foreach (IPlayer p in _players)
                p.MakeMove(new GameAPI(this));
        }

        public IPlayer GetOwnerOfProvince(int x, int y)
        {
            if (x < 0 || x >= _fieldSize || y < 0 || y >= _fieldSize)
                throw new ArgumentOutOfRangeException("Punkt nicht im Feld enthalten");
            else
                return _provinces[x, y].Owner.Player;
        }
    }
}
