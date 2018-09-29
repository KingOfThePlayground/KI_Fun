using KI_Fun.Backend.Messages;
using KI_Fun.Backend.Player;
using System;
using System.Collections.Generic;

namespace KI_Fun.Backend.API
{
    class CountryApi : Api
    {
        Country _country { get => (Country)_inner; }

        public IEnumerable<ArmyApi> Armies { get => new ApiCollection<ArmyApi>(_country.Armies); }
        public CountryApi(Country inner) : base(inner)
        {
        }

        public int FieldWidth { get => _country.AllProvinces.GetLength(0); }
        public int FieldHeight { get => _country.AllProvinces.GetLength(1); }

        public ProvinceApi GetProvinceAt(int x, int y)
        {
            if (x >= 0 && x < FieldWidth && y >= 0 && y < FieldHeight)
                return _country.AllProvinces[x, y].Api;
            else
                throw new ArgumentOutOfRangeException("Die Koordinaten sind außerhalb der Karte.");
        }

        public bool TryAcceptOffer(BasePlayer player, DiplomaticMessage offer)
        {
            if (_country.Owner.Player != player)
                return false;
            if (!_country.RetrieveOffer(offer))
                return false;
            switch (offer.MessageType)
            {
                case MessageType.PeaceOffer:
                    {
                        new DiplomaticMessage(MessageType.PeaceOfferAccepted, offer);
                        Country active = offer.Passive._country;
                        Country passive = offer.Active._country;
                        active.War.Remove(passive);
                        passive.War.Remove(active);
                        break;
                    }
                case MessageType.MarchAccessRequest:
                    {
                        new DiplomaticMessage(MessageType.MarchAccessAccepted, offer);
                        Country allows = offer.Passive._country;
                        Country marches = offer.Active._country;
                        marches.MarchAccess.Add(allows);
                        break;
                    }
            }
            return true;
        }

        public bool TryOfferPeace(BasePlayer player, CountryApi countryApi)
        {
            if (_country.Owner.Player != player)
                return false;
            Country country = countryApi._country;
            if (!this._country.War.Contains(country))
                return false;
            else
            {
                new DiplomaticMessage(MessageType.PeaceOffer, this._country, country);
                return true;
            }
        }

        public bool TryRequestMarchAccess(BasePlayer player, CountryApi countryApi)
        {
            if (_country.Owner.Player != player)
                return false;
            Country country = countryApi._country;
            if (this._country.War.Contains(country) || this._country.MarchAccess.Contains(country))
                return false;
            else
            {
                new DiplomaticMessage(MessageType.MarchAccessRequest, this._country, country);
                return true;
            }
        }

        public bool TryRevokeOwnMarchAccess(BasePlayer player, CountryApi countryApi)
        {
            if (_country.Owner.Player != player)
                return false;
            Country country = countryApi._country;
            if (this._country.MarchAccess.Contains(country))
                return false;
            else
            {
                new DiplomaticMessage(MessageType.OwnMarchAccessRevoked, this._country, country);
                return true;
            }
        }

        public bool TryRevokeOtherMarchAccess(BasePlayer player, CountryApi countryApi)
        {
            if (_country.Owner.Player != player)
                return false;
            Country country = countryApi._country;
            if (country.MarchAccess.Contains(this._country))
                return false;
            else
            {
                new DiplomaticMessage(MessageType.OtherMarchAccessRevoked, this._country, country);
                return true;
            }
        }

        public bool TryDeclareWar(BasePlayer player, CountryApi countryApi)
        {
            if (_country.Owner.Player != player)
                return false;
            Country country = countryApi._country;
            if (this._country.MarchAccess.Contains(country) || _country.War.Contains(country))
                return false;
            else
            {
                country.War.Add(this._country);
                this._country.War.Add(country);
                country.Player.MessageQueue.Enqueue(new DiplomaticMessage(MessageType.WarDeclaration, this._country, country));
                return true;
            }
        }
    }
}
