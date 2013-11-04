using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    class DealersWinningStrategy : IWinningStrategy
    {
        public bool Winner(Dealer a_dealer, Player a_player)
        {
            int maxScore = 21;

            if (a_player.CalcScore() > maxScore)
            {
                return true;
            }
            else if (a_dealer.CalcScore() > maxScore)
            {
                return false;
            }
            else if (a_dealer.CalcScore() == a_player.CalcScore())
            {
                return true;
            }

            return a_dealer.CalcScore() >= a_player.CalcScore();
        }
    }
}
