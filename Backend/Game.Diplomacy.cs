using KI_Fun.Backend.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend
{
    partial class Game
    {
		public HashSet<DiplomaticMessage> NewOffers { get; private set; }
        public HashSet<DiplomaticMessage> OldOffers { get; private set; }

        private void initDiplomacy()
        {
            NewOffers = new HashSet<DiplomaticMessage>();
            OldOffers = new HashSet<DiplomaticMessage>();
        }

        private void ageOffers()
        {
            OldOffers = NewOffers;
            NewOffers = new HashSet<DiplomaticMessage>(NewOffers.Count);
        }

		public bool IsOfferValid(DiplomaticMessage offer)
        {
            return NewOffers.Contains(offer) || OldOffers.Contains(offer);
        }

		public void AddOffer(DiplomaticMessage offer)
        {
            NewOffers.Add(offer);
        }
    }
}
