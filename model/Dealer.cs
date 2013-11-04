using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    class Dealer : Player
    {
        private Deck m_deck = null;
        private const int g_maxScore = 21;

        private rules.IWinningStrategy m_winningRule;
        private rules.INewGameStrategy m_newGameRule;
        private rules.IHitStrategy m_hitRule;

        private List<GameObserver> m_listeners = new List<GameObserver>();


        public Dealer(rules.RulesFactory a_rulesFactory)
        {
            m_newGameRule = a_rulesFactory.GetNewGameRule();
            m_hitRule = a_rulesFactory.GetHitRule();
            m_winningRule = a_rulesFactory.GetWinningRule();
        }

        public bool NewGame(Player a_player)
        {
            if (m_deck == null || IsGameOver())
            {
                m_deck = new Deck();
                ClearHand();
                a_player.ClearHand();
                return m_newGameRule.NewGame(this, a_player);   
            }
            return false;
        }

        public void PlaceCard(Player a_player, bool visibility)
        {
            Card c;
            c = m_deck.GetCard();
            //c.Show(visibility);
            a_player.DealCard(c);

            foreach (GameObserver listener in m_listeners)
            {
                listener.CardDelt(c, visibility);
            }
        }

        public void RegisterListener(GameObserver a_listener)
        {
            m_listeners.Add(a_listener);
        }

        public bool Hit(Player a_player)
        {
            if (m_deck != null && a_player.CalcScore() < g_maxScore && !IsGameOver())
            {
                PlaceCard(a_player, true);

                return true;
            }
            return false;
        }

        public bool Stand(Player a_player)
        {
            if(m_deck != null) {
                ShowHand();
            }

            while (m_hitRule.DoHit(this))
            {
                PlaceCard(this, true);
            }
            return true;
        }

        public bool IsDealerWinner(Player a_player)
        {
            return m_winningRule.Winner(this, a_player);
        }

        public bool IsGameOver()
        {
            if (m_deck != null && /*CalcScore() >= g_hitLimit*/ m_hitRule.DoHit(this) != true)
            {
                return true;
            }
            return false;
        }
    }
}
