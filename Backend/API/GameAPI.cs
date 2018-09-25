using KI_Fun.Backend.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.API
{
    class GameApi
    {
        private Game _game;
        private Player.BasePlayer _player;

        public GameApi(Game game, Player.BasePlayer player)
        {
            _game = game;
            _player = player;
        }

        public CountryApi CountryApi { get => (CountryApi)_player.Country.Api; }

        public bool TryDeclareWar(Country country)
        {
            if (country.MarchAccess.Contains(country) || country.War.Contains(country))
                return false;
            else
            {
                country.War.Add(_player.Country);
                _player.Country.War.Add(country);
                country.Owner.MessageQueue.Enqueue(new WarDeclarationMessage(_player.Country, country));
                return true;
            }
        }

        public bool TrySendArmy(ArmyApi armyApi, Direction direction)
        {
            if (IsArmyMovePossible(armyApi, direction))
            {
                Army army = (Army)armyApi.Inner;
                if (army.OwnerCountry.Owner != _player)
                    throw new AccessViolationException("Zugriff auf fremde Armee");
                army.MoveQueue.Enqueue(direction);
                return true;
            }
            else
                return false;
        }

        public void EnqueMarchOrder(ArmyApi armyApi, Direction direction)
        {
            Army army = (Army)armyApi.Inner;
            if (army.OwnerCountry.Owner != _player)
                throw new AccessViolationException("Zugriff auf fremde Armee");
            army.MoveQueue.Enqueue(direction);
        }

        public bool TryGetMoveTarget(ArmyApi armyApi, Direction direction, out Province target)
        {
            return _game.TryGetMoveTarget(((Army)armyApi.Inner).InProvince, direction, out target);
        }

        public bool IsArmyMovePossible(ArmyApi armyApi, Direction direction)
        {
            Army army = armyApi.Inner as Army;
            if (_game.TryGetMoveTarget(army.InProvince, direction, out Province target))
                return _game.IsArmyAllowedInProvince(army, target);
            else
                return false;
        }
    }
}
