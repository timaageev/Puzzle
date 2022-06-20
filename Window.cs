using System;
using System.Windows.Forms;
using System.Drawing;

namespace Puzzle {
    public class Window: Form {
        private const int FieldSize = 8;
        private const int CellSize = 32;
        
        private MenuStrip _menuBar;
        private ToolStripMenuItem _reset, _exit;

        private bool bFirstTime = true;

        private Button[,] _cells = new Button[FieldSize, FieldSize];
        public Window() {
            this.Text = "Головоломка";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            
            _menuBar = new MenuStrip();
            _reset = new ToolStripMenuItem("Сброс", null, (sender, args) => {Reset();});
            _exit = new ToolStripMenuItem("Выход", null, ExitOnClick);
            _menuBar.Items.Add(_reset);
            _menuBar.Items.Add(_exit);
            
            
            Reset();
            this.Controls.Add(_menuBar);
        }

        private void ExitOnClick(object sender, EventArgs e) {
            Application.Exit();
        }

        private void OnCellClick(object sender, EventArgs e) {
            var btn = (Button)sender;

            int x = btn.Location.X / CellSize, y = (btn.Location.Y - _menuBar.Size.Height) / CellSize;

            // Horizontal
            for (int i = 0; i < FieldSize; i++) {
                _cells[i, y].BackColor = (_cells[i, y].BackColor == Color.White) ? Color.Black : Color.White;
            }
            // Vertical
            for (int i = 0; i < FieldSize; i++) {
                _cells[x, i].BackColor = (_cells[i, y].BackColor == Color.White) ? Color.Black : Color.White;
            }
        }
        
        private void Reset() {
            var rnd = new Random();
            if (bFirstTime) {
                bFirstTime = false;
                for (int i = 0; i < FieldSize; i++) {
                    for (int j = 0; j < FieldSize; j++) {
                        _cells[i, j] = new Button();
                        _cells[i, j].Size = new Size(CellSize, CellSize);
                        _cells[i, j].Location = new Point(i * CellSize, j * CellSize + _menuBar.Size.Height);
                        _cells[i, j].Click += OnCellClick;
                        this.Controls.Add(_cells[i, j]);
                    }
                }
            }
            
            for (int i = 0; i < FieldSize; i++) {
                for (int j = 0; j < FieldSize; j++) {
                    _cells[i, j].BackColor = (rnd.Next(0, 100) < 50) ? Color.Black : Color.White;
                }
            }
         }
    }
}