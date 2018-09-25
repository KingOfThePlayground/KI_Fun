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
        List<Brush> _brushes = new List<Brush>() {Brushes.Blue, Brushes.Red, Brushes.Green, Brushes.Yellow, Brushes.Cyan, Brushes.Violet, Brushes.Orange, Brushes.Brown, Brushes.White };
        Backend.Game _game;
        Dictionary<IPlayer, Brush> _countryBrushes;
        int _xOriginMainPictureBox = 0;
        int _yOriginMainPictureBox = 0;
        bool _moveUp = false, _moveDown = false, _moveLeft = false, _moveRight = false;
        int _provinceSize = 60;
        int _provincesPerRow = 16;
        int _fieldSize;

        public FormMain()
        {
            _fieldSize = _provincesPerRow * _provinceSize;
            _countryBrushes = new Dictionary<IPlayer, Brush>();
            List<Backend.Player.IPlayer> players = new List<Backend.Player.IPlayer>() { new DummyPlayer(), new DummyPlayer(), new DummyPlayer(), new DummyPlayer(), new DummyPlayer(), new DummyPlayer(), new DummyPlayer(), new DummyPlayer(), new DummyPlayer() };
            for (int i = 0; i < players.Count; i++)
            {
                _countryBrushes.Add(players[i], _brushes[i]);
            }
            _game = new Game(players,_provincesPerRow);
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int stepSize = timer1.Interval / 10;
            if (_moveDown && -_yOriginMainPictureBox + pictureBoxMain.Bottom < _fieldSize)
            {
                _yOriginMainPictureBox -= stepSize;
            }
            if (_moveUp && _yOriginMainPictureBox < 0)
            {
                _yOriginMainPictureBox += stepSize;
            }
            if (_moveRight && -_xOriginMainPictureBox + pictureBoxMain.Right < _fieldSize)
            {
                _xOriginMainPictureBox -= stepSize;
            }
            if (_moveLeft && _xOriginMainPictureBox < 0)
            {
                _xOriginMainPictureBox += stepSize;
            }
            pictureBoxMain.Refresh();
            pictureBoxOverview.Refresh();
        }

        private void pictureBoxMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(_xOriginMainPictureBox,_yOriginMainPictureBox);
            int xStep = _provinceSize, yStep = _provinceSize;
            for (int y = 0; y < _game.FieldHeight; y++)
            {
                for (int x = 0; x < _game.FieldWidth; x++)
                {
                    e.Graphics.FillRectangle(_countryBrushes[_game.Provinces[x, y].Owner.Owner], new RectangleF(x * xStep, y * yStep, xStep, yStep));
                    e.Graphics.DrawRectangle(Pens.Black, new Rectangle(x * xStep, y * yStep, xStep, yStep));
                }
            }
        }

        private void pictureBoxMain_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X - _xOriginMainPictureBox;
            int y = e.Y - _yOriginMainPictureBox;
            int xProvince = x / _provinceSize;
            int yProvince = y / _provinceSize;
        }

        private void pictureBoxOverview_Paint(object sender, PaintEventArgs e)
        {
            float xStep = pictureBoxOverview.ClientSize.Width / _game.FieldWidth;
            float yStep = pictureBoxOverview.ClientSize.Height / _game.FieldHeight;
 
            for (int y = 0; y < _game.FieldHeight; y++)
            {
                for (int x = 0; x < _game.FieldWidth; x++)
                {                   
                    e.Graphics.FillRectangle(_countryBrushes[_game.Provinces[x, y].Owner.Owner],new RectangleF(x * xStep, y * yStep, xStep, yStep));
                }
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    _moveUp = true;
                    e.Handled = true;
                    break;
                case Keys.Down:
                case Keys.S:
                    _moveDown = true;
                    e.Handled = true;
                    break;
                case Keys.Left:
                case Keys.A:
                    _moveLeft = true;
                    e.Handled = true;
                    break;
                case Keys.Right:
                case Keys.D:
                    _moveRight = true;
                    e.Handled = true;
                    break;
            }
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    _moveUp = false;
                    e.Handled = true;
                    break;
                case Keys.Down:
                case Keys.S:
                    _moveDown = false;
                    e.Handled = true;
                    break;
                case Keys.Left:
                case Keys.A:
                    _moveLeft = false;
                    e.Handled = true;
                    break;
                case Keys.Right:
                case Keys.D:
                    _moveRight = false;
                    e.Handled = true;
                    break;
            }
        }
    }
}
