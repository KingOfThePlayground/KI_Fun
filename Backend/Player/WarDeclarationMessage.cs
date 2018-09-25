using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KI_Fun.Backend.Player
{
    class WarDeclarationMessage : Message
    {
        public Country Declarer { get; private set; }
        public Country DeclaredUpon { get; private set; }

        public WarDeclarationMessage(Country declarer, Country declaredUpon) : base(MessageType.WarDeclaration)
        {
            Declarer = declarer;
            DeclaredUpon = declaredUpon;
        }

        public override string ToString()
        {
            return $"{Declarer.Owner} hat {DeclaredUpon.Owner} den Krieg erklärt.";
        }
    }
}
