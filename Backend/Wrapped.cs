using KI_Fun.Backend.API;
using KI_Fun.Backend.Player;

namespace KI_Fun.Backend
{
    abstract class Wrapped
    {
        protected Api _api;
        public virtual Api Api
        {
            get
            {
                return _api;
            }
            set
            {
                _api = value;
                GameApi.AddInner(this);
            }
        }

        public Country Owner { get; set; }

        public virtual bool IsNeighbouring(BasePlayer player)
        {
            return false;
        }

        public bool IsNeighbouring(BasePlayer player, int x, int y)
        {
            Country country = player.Country;
            int fieldWidth = country.AllProvinces.GetLength(0);
            int fieldHeight = country.AllProvinces.GetLength(1);
            Province p;

            for (int i = x - 1; i <= x + 1; i++)
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < fieldWidth && j >= 0 && j < fieldHeight)
                    {
                        p = country.AllProvinces[i, j];
                        if (p.Owner == country)
                            return true;
                        foreach (Army a in p.ArmiesInProvince)
                            if (a.Owner == country)
                                return true;
                    }
                }

            return false;
        }

        private bool coordinateNeighbourhoodCheck(int x_1, int y_1, int x_2, int y_2)
        {
            return (x_1 - x_2) * (x_1 - x_2) + (y_1 - y_2) * (y_1 - y_2) < 3;
        }
    }

    abstract class Wrapped<T_API> : Wrapped where T_API : Api
    {
        public new T_API Api
        {
            get
            {
                return (T_API)_api;
            }
            set
            {
                _api = value;
                GameApi.AddInner(this);
            }
        }
    }
}
