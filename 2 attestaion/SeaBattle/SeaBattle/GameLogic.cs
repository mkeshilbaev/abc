﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SeaBattle
{
    public delegate void TurnDelegate();

    class GameLogic
    {
        public PlayerPanel p1, p2;
        public Color Color1 = Color.Blue;
        public Color Color2 = Color.White;

        public GameLogic(TurnDelegate GameDlg)
        {
            p1 = new PlayerPanel(PanelPosition.Left, PlayerType.Human, MakeBotTurn, GameDlg, Color1);
            p2 = new PlayerPanel(PanelPosition.Right, PlayerType.Bot, MakeBotTurn, GameDlg, Color2);
        }

        void MakeBotTurn()
        {
            Random rnd = new Random();
            int i = rnd.Next(0, 10);
            int j = rnd.Next(0, 10);

            while (p1.brain.Process2(string.Format("{0}_{1}", i, j)))
            {
                Thread.Sleep(2000);
                i = rnd.Next(0, 10);
                j = rnd.Next(0, 10);
            }
        }
    }
}
