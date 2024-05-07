using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;

//Adit Mishra
//May 7,2024
//Square Chaser

namespace Square_Chaser
{
    public partial class Form1 : Form
    {
        Rectangle player1 = new Rectangle(100, 100, 20, 20);//drawing players, ball and speed boost
        Rectangle player2 = new Rectangle(500, 300, 20, 20);
        Rectangle ball = new Rectangle(300, 200, 15, 15);
        Rectangle coin = new Rectangle(150, 300, 12, 12);

        int player1Score = 0;//variables
        int player2Score = 0;

        int playerSpeed = 4;
        int player2Speed = 4;

        Random randGen = new Random();//random generator

        bool wPressed = false;//movement
        bool aPressed = false;
        bool sPressed = false;
        bool dPressed = false;
        bool upPressed = false;
        bool downPressed = false;
        bool leftPressed = false;
        bool rightPressed = false;

        SolidBrush orangeBrush = new SolidBrush(Color.Orange);//pens and brushes for players, ball, boost and border
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush purpleBrush = new SolidBrush(Color.Purple);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        Pen bluePen = new Pen(Color.Blue, 8);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)//enabling player movement
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)//enabling player movement
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

            Player_Movement();
            Ball_Player_Intersection();
            Boost_Player_Intersection();
            Game_End();

            Refresh();
        }

        public void Player_Movement()
        {
            //move player 1
            if (wPressed == true && player1.Y > 55)
            {
                player1.Y = player1.Y - playerSpeed;
            }

            if (sPressed == true && player1.Y < 347 - player1.Height)
            {
                player1.Y = player1.Y + playerSpeed;
            }

            if (aPressed == true && player1.X > 55)
            {
                player1.X = player1.X - playerSpeed;
            }

            if (dPressed == true && player1.X < 547 - player1.Width)
            {
                player1.X = player1.X + playerSpeed;
            }

            //move player 2
            if (upPressed == true && player2.Y > 55)
            {
                player2.Y = player2.Y - player2Speed;
            }

            if (downPressed == true && player2.Y < 347 - player2.Height)
            {
                player2.Y = player2.Y + player2Speed;
            }

            if (leftPressed == true && player2.X > 55)
            {
                player2.X = player2.X - player2Speed;
            }

            if (rightPressed == true && player2.X < 547 - player2.Width)
            {
                player2.X = player2.X + player2Speed;
            }
        }

        public void Ball_Player_Intersection()
        {
            //check if player1 hits ball
            if (player1.IntersectsWith(ball))
            {
                ball.X = randGen.Next(55, 547);
                ball.Y = randGen.Next(55, 347);
                player1Score++;
                p1scoreLabel.Text = $"{player1Score}";
                SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.addPoint);
                soundPlayer.Play();
            }
            //check if player2 hits ball
            if (player2.IntersectsWith(ball))
            {
                ball.X = randGen.Next(55, 547);
                ball.Y = randGen.Next(55, 347);
                player2Score++;
                p2scoreLabel.Text = $"{player2Score}";
                SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.addPoint);
                soundPlayer.Play();
            }
        }

        public void Boost_Player_Intersection()
        {
            //check if player1 hits boost
            if (player1.IntersectsWith(coin))
            {
                coin.X = randGen.Next(55, 547);
                coin.Y = randGen.Next(55, 347);
                playerSpeed++;
                SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.powerUp);
                soundPlayer.Play();
            }
            //check if player2 hits boost
            if (player2.IntersectsWith(coin))
            {
                coin.X = randGen.Next(55, 547);
                coin.Y = randGen.Next(55, 347);
                player2Speed++;
                SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.powerUp);
                soundPlayer.Play();
            }
        }

        public void Game_End()
        {
            //if player 1 wins
            if (player1Score == 5)
            {
                winLabel.Text = "Player 1 Wins!";
                SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.gameWin);
                soundPlayer.Play();
            }

            //if player 2 wins
            else if (player2Score == 5)
            {
                winLabel.Text = "Player 2 Wins";
                SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.gameWin);
                soundPlayer.Play();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //filling players, ball, boost and drawing borders
            e.Graphics.FillRectangle(whiteBrush, ball);
            e.Graphics.FillRectangle(purpleBrush, coin);
            e.Graphics.FillRectangle(orangeBrush, player1);
            e.Graphics.FillRectangle(greenBrush, player2);
            e.Graphics.DrawLine(bluePen, 50, 350, 550, 350);
            e.Graphics.DrawLine(bluePen, 50, 50, 550, 50);
            e.Graphics.DrawLine(bluePen, 50, 47, 50, 353);
            e.Graphics.DrawLine(bluePen, 550, 47, 550, 353);
        }
    }
}
