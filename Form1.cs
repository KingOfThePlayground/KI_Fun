using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KI_Fun.Backend;
using KI_Fun.Backend.Player;

namespace KI_Fun
{
    public partial class FormMain : Form
    {
        List<Brush> _brushes = new List<Brush>() { Brushes.Black, Brushes.White, Brushes.Blue, Brushes.Green, Brushes.Red, Brushes.Yellow, Brushes.Violet, Brushes.Orange};
        Backend.Game _game;
        Dictionary<IPlayer, Brush> _countryBrushes;

        public FormMain()
        {
            
            _countryBrushes = new Dictionary<IPlayer, Brush>();
            List<Backend.Player.IPlayer> players = new List<Backend.Player.IPlayer>() { new DummyPlayer(), new DummyPlayer()};
            for (int i = 0; i < players.Count; i++)
            {
                _countryBrushes.Add(players[i], _brushes[i]);
            }
            _game = new Game(players,8);
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBoxMain.Refresh();
            pictureBoxOverview.Refresh();
        }

        private void pictureBoxMain_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void pictureBoxOverview_Paint(object sender, PaintEventArgs e)
        {
            float xStep = pictureBoxOverview.Width / _game.FieldWidth;
            float yStep = pictureBoxOverview.Height / _game.FieldHeight;
            for (int y = 0; y < _game.FieldHeight; y++)
            {
                for (int x = 0; x < _game.FieldWidth; x++)
                {                                                                           
                    e.Graphics.FillRectangle(_countryBrushes[_game.Provinces[x, y].Owner.Owner],new RectangleF(x * xStep, y * yStep, xStep, yStep));
                }
            }
        }
    }
}
