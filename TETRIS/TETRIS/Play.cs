using System;
using System.Drawing;
using System.IO;
using System.Timers;
using System.Windows.Forms;

namespace TETRIS
{
    public partial class Play : Form
    {
        WMPLib.WindowsMediaPlayer gameMusic = Home.GetMediaPlayer();

        private int h, m, s;
        private int speed = Options.GetSpeed();
        private Bitmap canvasBitmap;
        private Graphics canvasGraphics;
        private int size = 20;
        private Game game;
        private Bitmap workingBitmap;
        private Graphics workingGraphics;
        private Bitmap nextBitmap;
        private Graphics nextGraphics;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private System.Timers.Timer t;
        private string[] colors = { "#ffffff","#ffff00", "#00ffff", "#800080", "#ff7f00", "#0000ff", "#ff0000", "#00ff00" };

        private void loadCanvas()
        {
            pictureBox1.Width = size * game.width;
            pictureBox1.Height = game.height * size;
            canvasBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            canvasGraphics = Graphics.FromImage(canvasBitmap);
            Color _color = System.Drawing.ColorTranslator.FromHtml("#ffffff");
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(_color);
            for (int i = 0; i < game.width; i++)
            {
                for (int j = 0; j < game.height; j++)
                {
                    canvasGraphics.FillRectangle(myBrush, i * size, j * size, size - 1, size - 1);
                }
            }
            pictureBox1.Image = canvasBitmap;
        }


        private void Play_Load_1(object sender, EventArgs e)
        {
            t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += OnTimeEvent;
            t.Start();
            gameMusic.controls.stop();
            string path = Directory.GetCurrentDirectory();
            Console.WriteLine(path);
            gameMusic.URL = @"D:\Developement\tetrics\CS-Tetris\TETRIS\TETRIS\game_theme.wav";
            gameMusic.settings.setMode("loop", true);
            gameMusic.controls.play();
        }
        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                s += 1;
                if (s == 60)
                {
                    s = 0;
                    m += 1;
                }
                if (m == 60)
                {
                    m = 0;
                    h += 1;
                }
                txtResult.Text = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
            }));
        }
        private void Play_FormClosing(Object sender, FormClosingEventArgs e)
        {
            t.Stop();
            Application.Exit();
        }
        private void drawShape()
        {
            workingBitmap = new Bitmap(canvasBitmap);
            workingGraphics = Graphics.FromImage(workingBitmap);
            Color _color = System.Drawing.ColorTranslator.FromHtml(game.currentShape.Color);
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(_color);
            for (int i = 0; i < game.currentShape.Width; i++)
            {
                for (int j = 0; j < game.currentShape.Height; j++)
                {
                    if (game.currentShape.Piece[j, i] != 0)
                    {
                        workingGraphics.FillRectangle(myBrush, (game.currentX + i) * size, (game.currentY + j) * size, size - 1, size - 1);
                    }
                }
            }

            pictureBox1.Image = workingBitmap;
        }
        public Play()
        {
            InitializeComponent();
            game = new Game();
            loadCanvas();
            updateNextShape();

            timer.Tick += Timer_Tick;
            timer.Interval = 500-speed*4;
            timer.Start();

        }

        private void Play_KeyDown(object sender, KeyEventArgs e)
        {
            int posX = 0;
            int posY = 0;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    posX--;
                    break;
                case Keys.Right:
                    posX++;
                    break;
                case Keys.Down:
                    posY++;
                    break;
                case Keys.Up:
                    game.rotate();
                    break;
                default:
                    return;
            }
            bool canMove = game.canMove(posX, posY);
            if (!canMove && e.KeyCode == Keys.Up)
            {
                game.rollback();
            }
            drawShape();
        }
        private void updateGrid()
        {
            for(int i = 0; i < game.width; i++)
            {
                for (int j = 0; j < game.height; j++)
                {
                    canvasGraphics = Graphics.FromImage(canvasBitmap);
                    Color _color = System.Drawing.ColorTranslator.FromHtml(colors[game.grid[i,j]]);
                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(_color);
                    canvasGraphics.FillRectangle(myBrush, i * size, j * size, size - 1, size - 1);
                }
            }
            pictureBox1.Image = canvasBitmap;
        }

        private void updateNextShape()
        {
            nextBitmap = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            nextGraphics = Graphics.FromImage(nextBitmap);
            for (int i = 0; i < game.nextShape.Width; i++)
            {
                for (int j = 0; j < game.nextShape.Height; j++)
                {
                    if (game.nextShape.Piece[j, i] !=0)
                    {
                        Color _color = System.Drawing.ColorTranslator.FromHtml(game.nextShape.Color);
                        System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(_color);
                        nextGraphics.FillRectangle(myBrush, i * size, j * size, size - 1, size - 1);
                    }
                }
            }
            pictureBox2.Image = nextBitmap;

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            bool isMoveSuccess = game.canMove(0, 1);
            drawShape();
            if (!isMoveSuccess)
            {
                game.checkIfGameOver();
                if (game.ended)
                {
                    Application.Exit();
                    return;
                }
                game.updateArray();
                canvasBitmap = new Bitmap(workingBitmap);
                bool update = game.clearRows();
                if (update)
                {
                    updateGrid();
                }
                label3.Text = "Score: " + game.score.ToString();
                label4.Text = "Level: " + game.score / 1000;
                timer.Interval = 500 - speed * 4 - 20*(game.score / 1000);
                game.currentShape = game.nextShape;
                game.nextShape = game.getNewShape();
                updateNextShape();
            }
        }

        public int GetSpeed()
        {
            return speed;
        }

        public void SetSpeed(int value)
        {
            speed = value;
        }
    }
}
