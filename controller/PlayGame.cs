﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.controller
{
    class PlayGame : model.GameObserver
    {
        private model.Game m_game;
        private view.IView m_view;

        public PlayGame(model.Game a_game, view.IView a_view)
        {
            m_game = a_game;
            m_view = a_view;
        }
        public bool Play()
        {
            DisplayAllScreen();

            if (m_game.IsGameOver())
            {
                m_view.DisplayGameOver(m_game.IsDealerWinner());
            }

            m_view.GetInput();

            if (m_view.WantsNewGame())
                m_game.NewGame();
            else if (m_view.WantsToHit())
                m_game.Hit();
            else if (m_view.WantsToStand())
                m_game.Stand();

            return (!m_view.WantsToQuit());
        }

        public void DisplayAllScreen()
        {
            m_view.DisplayWelcomeMessage();

            m_view.DisplayDealerHand(m_game.GetDealerHand(), m_game.GetDealerScore());
            m_view.DisplayPlayerHand(m_game.GetPlayerHand(), m_game.GetPlayerScore());
        }

        public void CardDelt(model.Card c, bool visibility)
        {
            //System.Threading.Thread.Sleep(500);
            DisplayAllScreen();
            c.Show(visibility);
            //System.Threading.Thread.Sleep(1000);
            DisplayAllScreen();
        }

    }
}
