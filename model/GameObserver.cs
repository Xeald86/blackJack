using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    interface GameObserver
    {
        void CardDelt(model.Card c, bool visibility);
    }
}
