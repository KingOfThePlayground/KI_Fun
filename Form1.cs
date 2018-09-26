using KI_Fun.Backend;
using KI_Fun.Backend.Player;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace KI_Fun
{
    public partial class FormMain : Form
    {
        List<BasePlayer> _players;
        List<Brush> _brushes = new List<Brush>() { Brushes.Blue, Brushes.Red, Brushes.Green, Brushes.Yellow, Brushes.Cyan, Brushes.Violet, Brushes.Orange, Brushes.Brown, Brushes.White };
        Backend.Game _game;
        Dictionary<BasePlayer, Brush> _countryBrushes;
        Dictionary<BasePlayer, int> _playerNumber;
        int _xOriginMainPictureBox = 0;
        int _yOriginMainPictureBox = 0;
        bool _moveUp = false, _moveDown = false, _moveLeft = false, _moveRight = false;
        int _provinceSize = 100;
        int _provincesPerRow = 8;
        int _fieldSize;
        const int TICK_PHASE_MS = 50;
        ConcurrentQueue<Backend.Messages.Message> _logQueue;

        object _threadLock = new object();

        public FormMain()
        {
            _logQueue = new ConcurrentQueue<Backend.Messages.Message>();
            _fieldSize = _provincesPerRow * _provinceSize;
            _countryBrushes = new Dictionary<BasePlayer, Brush>();
            _playerNumber = new Dictionary<BasePlayer, int>();
            _players = new List<Backend.Player.BasePlayer>() { new Hibbelig(), new Hibbelig(), new Hibbelig(), new Hibbelig() };
            for (int i = 0; i < _players.Count; i++)
            {
                _countryBrushes.Add(_players[i], _brushes[i]);
                _playerNumber.Add(_players[i], i);
            }
            _game = new Game(_players, _provincesPerRow);
            InitializeComponent();
            timer1.Start();
            tickWorker.RunWorkerAsync();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int stepSize = timer1.Interval / 10;
            if (_moveDown && -_yOriginMainPictureBox + pictureBoxMain.Height < _fieldSize)
            {
                _yOriginMainPictureBox -= stepSize;
            }
            if (_moveUp && _yOriginMainPictureBox < 0)
            {
                _yOriginMainPictureBox += stepSize;
            }
            if (_moveRight && -_xOriginMainPictureBox + pictureBoxMain.Width < _fieldSize)
            {
                _xOriginMainPictureBox -= stepSize;
            }
            if (_moveLeft && _xOriginMainPictureBox < 0)
            {
                _xOriginMainPictureBox += stepSize;
            }
            pictureBoxMain.Refresh();
            pictureBoxOverview.Refresh();

            while (_logQueue.Count != 0)
            {
                if (_logQueue.TryDequeue(out var msg))
                    textBoxLog.AppendText(msg.ToString());
            }
        }

        private void pictureBoxMain_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(_xOriginMainPictureBox, _yOriginMainPictureBox);
            int xStep = _provinceSize, yStep = _provinceSize;

            lock (_threadLock)
            {
                for (int y = 0; y < _game.FieldHeight; y++)
                {
                    for (int x = 0; x < _game.FieldWidth; x++)
                    {
                        e.Graphics.FillRectangle(_countryBrushes[_game.Provinces[x, y].Owner.Player], new RectangleF(x * xStep, y * yStep, xStep, yStep));
                        e.Graphics.DrawRectangle(Pens.Black, new Rectangle(x * xStep, y * yStep, xStep, yStep));
                    }
                }
                float anglePerPlayer = 360.0f / _countryBrushes.Count;
                foreach (Army army in _game.Armies)
                {
                    int x = army.InProvince.X;
                    int y = army.InProvince.Y;
                    RectangleF circle = new RectangleF((x + 0.2f) * _provinceSize, (y + 0.2f) * _provinceSize, 0.6f * _provinceSize, 0.6f * _provinceSize);
                    e.Graphics.FillEllipse(Brushes.Black, circle);
                    e.Graphics.FillPie(_countryBrushes[army.Owner.Player], circle.X + 2, circle.Y + 2, circle.Width - 4, circle.Height - 4, _playerNumber[army.Owner.Player] * anglePerPlayer - 90, anglePerPlayer);
                }
            }
        }

        private void pictureBoxMain_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X - _xOriginMainPictureBox;
            int y = e.Y - _yOriginMainPictureBox;
            int xProvince = x / _provinceSize;
            int yProvince = y / _provinceSize;
            string ownerString;

            lock (_threadLock)
            {
                ownerString = _game.Provinces[xProvince, yProvince].Owner.Player.ToString();
            }
            textBoxLog.AppendText($"Dies ist die Provinz mit den Koordinaten ({xProvince}, {yProvince}).\r\nSie gehört {ownerString}.\r\n");
        }

        private void tickWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            while (true)
            {
                watch.Restart();
                lock (_threadLock)
                {
                    _game.Tick();
                }
                if (watch.ElapsedMilliseconds < TICK_PHASE_MS)
                    Thread.Sleep(TICK_PHASE_MS - (int)watch.ElapsedMilliseconds);
            }
        }

        private void pictureBoxOverview_Paint(object sender, PaintEventArgs e)
        {
            float xStep = pictureBoxOverview.ClientSize.Width / _game.FieldWidth;
            float yStep = pictureBoxOverview.ClientSize.Height / _game.FieldHeight;

            lock (_threadLock)
            {
                for (int y = 0; y < _game.FieldHeight; y++)
                {
                    for (int x = 0; x < _game.FieldWidth; x++)
                    {
                        e.Graphics.FillRectangle(_countryBrushes[_game.Provinces[x, y].Owner.Player], new RectangleF(x * xStep, y * yStep, xStep, yStep));
                    }
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
