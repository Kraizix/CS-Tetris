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
        private Home mainMenu;
        private int hours, minutes, seconds;
        private int speed = Options.GetSpeed();
        private Bitmap canvasBitmap;
        private Graphics canvasGraphics;
        private readonly int size = 20;
        private Game game;
        private Bitmap workingBitmap;
        private Graphics workingGraphics;
        private Bitmap nextBitmap;
        private Graphics nextGraphics;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private System.Timers.Timer time;
        private readonly string[] colors = { "#ffffff","#ffff00", "#00ffff", "#800080", "#ff7f00", "#0000ff", "#ff0000", "#00ff00" };
        private readonly bool clearMode = Options.getMode();
        private readonly int clearScore = Options.getClearScore();
        private readonly bool randomMode = Options.GetRandom();
        private System.Windows.Forms.Timer rtimer = new System.Windows.Forms.Timer();
        private int lastClear = 0;
        private bool pause = false;
        private bool isPlaying = true;

        private void LoadCanvas()
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
            time = new System.Timers.Timer();
            time.Interval = 1000;
            time.Elapsed += OnTimeEvent;
            time.Start();
            gameMusic.controls.stop();
            string path = Directory.GetCurrentDirectory();
            Console.WriteLine(path);
            gameMusic.URL = Home.AssetsPath + @"\game_theme.wav";
            gameMusic.settings.setMode("loop", true);
            gameMusic.controls.play();
        }

        private void Randomizer(object sender,EventArgs e)
        {
            string[] input = { "Rotate", "RRotate", "MoveRight", "MoveLeft", "Drop", "Dropdown" };
            Random random = new Random();
            string inp = input[random.Next(input.Length)];
            int posX = 0;
            int posY = 0;
            switch (inp)
            {
                case "Rotate":
                    game.Rotate();
                    break;
                case "RRotate":
                    game.ReverseRotate();
                    break;
                case "MoveRight":
                    posX++;
                    break;
                case "MoveLeft":
                    posX--;
                    break;
                case "Drop":
                    posY--;
                    break;
                case "Dropdown":
                    Drop();
                    break;
            }
            rtimer.Interval = random.Next(500, 5000);
            bool canMove = game.CanMove(posX, posY);
            if (!canMove && (inp == "RRotate" || inp == "Rotate"))
            {
                game.Rollback();
            }
            DrawShape();
        }
        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    seconds += 1;
                    if (seconds == 60)
                    {
                        seconds = 0;
                        minutes += 1;
                    }
                    if (minutes == 60)
                    {
                        minutes = 0;
                        hours += 1;
                    }
                    txtResult.Text = string.Format("{0}:{1}:{2}", hours.ToString().PadLeft(2, '0'), minutes.ToString().PadLeft(2, '0'), seconds.ToString().PadLeft(2, '0'));
                }));
            } catch
            {
                return;
            }
        }
        private void Play_FormClosing(Object sender, FormClosingEventArgs e)
        {
            try
            {
                time.Stop();
                timer.Stop();
                if (randomMode)
                {
                    rtimer.Stop();
                }
                Hide();
                mainMenu.Show();
            } catch
            {
                return;
            }
            
            

        }
        private void DrawShape()
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
        public Play(Home home)
        {
            mainMenu = home;
            InitializeComponent();
            game = new Game();
            LoadCanvas();
            UpdateNextShape();

            timer.Tick += Timer_Tick;
            timer.Interval = 500-speed*4;
            timer.Start();
            if (randomMode)
            {
                rtimer.Tick += Randomizer;
                rtimer.Interval = 300;
                rtimer.Start();
            }

        }

        private void Play_KeyDown(object sender, KeyEventArgs e)
        {
            if (pause)
            {
                if (e.KeyCode== Keys.Escape){
                    pause = false;
                    timer.Start();
                    time.Start();
                    if (randomMode)
                    {
                        rtimer.Start();
                    }
                    label5.Text = "Playing";
                }
                return;
            }
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
                    game.Rotate();
                    break;
                case Keys.D:
                    Drop();
                    break;
                case Keys.R:
                    game.ReverseRotate();
                    break;
                case Keys.M:
                    if (isPlaying)
                    {
                        gameMusic.controls.pause();
                        isPlaying = false;
                        label6.Text = "🔇";
                    } else
                    {
                        gameMusic.controls.play();
                        isPlaying = true;
                        label6.Text = "🔈";
                    }
                   
                    break;
                case Keys.Escape:
                    timer.Stop();
                    time.Stop();
                    if (randomMode)
                    {
                        rtimer.Stop();
                    }
                    pause = true;
                    label5.Text = "Paused";
                    break;
                default:
                    return;
            }
            bool canMove = game.CanMove(posX, posY);
            if (!canMove && (e.KeyCode == Keys.Up || e.KeyCode == Keys.R))
            {
                game.Rollback();
            }
            DrawShape();
        }
        private void UpdateGrid()
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

        private void UpdateNextShape()
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
            bool isMoveSuccess = game.CanMove(0, 1);
            DrawShape();
            if (!isMoveSuccess)
            {
                bool ended = game.UpdateArray();
                if (ended)
                {
                    timer.Stop();
                    time.Stop();
                    if (randomMode)
                    {
                        rtimer.Stop();
                    }
                    End end = new End(this);
                    end.ShowDialog();
                    return;
                }
                canvasBitmap = new Bitmap(workingBitmap);
                bool update = game.ClearRows();
                if (update)
                {
                    UpdateGrid();
                }
                label3.Text = "Score: " + game.score.ToString();
                label4.Text = "Level: " + game.score / 1000;
                timer.Interval = 500 - speed * 4 - 20*(game.score / 1000);
                if (clearMode && game.score/clearScore>lastClear)
                {
                    lastClear = game.score/clearScore;
                    ClearMode();
                }
                game.currentShape = game.nextShape;
                game.nextShape = game.GetNewShape();
                if (game.currentShape.Color == "#ffff00")
                {
                    game.currentX = 4;
                }
                UpdateNextShape();
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

        private void ClearMode()
        {
            for (int i = 0; i < game.width; i++)
            {
                for (int j = 0; j < game.height; j++)
                {
                    game.grid[i,j] = 0;
                }
            }
            UpdateGrid();
        }

        public int GetScore()
        {
            return game.score;
        }

        public void Reset()
        {
            game = new Game();
            LoadCanvas();
            UpdateNextShape();
            game.score = 0;
            timer.Interval = 500 - speed * 4;
            hours = 0;
            minutes = 0;
            seconds = 0;
            label3.Text = "Score: " + game.score.ToString();
            label4.Text = "Level: " + game.score / 1000;
            txtResult.Text = string.Format("{0}:{1}:{2}", hours.ToString().PadLeft(2, '0'), minutes.ToString().PadLeft(2, '0'), seconds.ToString().PadLeft(2, '0'));
            timer.Start();
            time.Start();
        }

        private void Drop()
        {
            while (true) {
                if (!game.CanMove(0, 1))
                {
                    break;
                }
            }
        }
    }
}
