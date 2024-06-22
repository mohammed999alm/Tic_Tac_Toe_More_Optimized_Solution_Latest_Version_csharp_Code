using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tic_Tac_Toe_More_Optimized_Solution.Properties;

namespace Tic_Tac_Toe_More_Optimized_Solution
{




    public partial class Form1 : Form
    {


        enPlayer playTurn;
        stGameStatus gameStatus;
        enum enPlayer 
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        }

        struct stGameStatus 
        {
           public enPlayer winner;
           public bool gameOver;
           public short playCount;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Color white = Color.FromArgb(255, 255, 255, 255);

            Pen pen1 = new Pen(white, 15);


            pen1.StartCap = System.Drawing.Drawing2D.LineCap.Round;

            pen1.EndCap = System.Drawing.Drawing2D.LineCap.Round;



            //Horizontal Lines
            e.Graphics.DrawLine(pen1, 400, 300, 1050, 300);

            e.Graphics.DrawLine(pen1, 400, 460, 1050, 460);


            //Vertical Lines

            e.Graphics.DrawLine(pen1, 610, 140, 610, 620);

            e.Graphics.DrawLine(pen1, 840, 140, 840, 620);

        }


        void endGame() 
        {
            lblPlayTurn.Text = "Game Over";

            switch (gameStatus.winner) 
            {
                case enPlayer.Player1:
                    lblWinner.Text = "Player1";
                    break;

                    case enPlayer.Player2:
                    lblWinner.Text = "Player2";
                    break;

                default:
                    lblWinner.Text = "Draw";
                    break;
            }

            MessageBox.Show("Game Over", "Game Over", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private bool checkValues(Button btn1, Button btn2, Button btn3) 
        {
            if (btn1.Tag.ToString() != "?")
            {
                if (btn1.Tag == btn2.Tag && btn1.Tag == btn3.Tag)
                {
                    btn1.BackColor = Color.GreenYellow;
                    btn2.BackColor = Color.GreenYellow;
                    btn3.BackColor = Color.GreenYellow;

                    if (btn1.Tag.ToString() == "X")
                    {


                        gameStatus.winner = enPlayer.Player1;

                        gameStatus.gameOver = true;

                        endGame();

                        return true;
                    }

                    else
                    {

                        gameStatus.winner = enPlayer.Player2;

                        gameStatus.gameOver = true;

                        endGame();

                        return true;
                    }
                }

            }

            gameStatus.gameOver = false;

            return false;
        }
        void checkWinner() 
        {
            if (checkValues(button1, button2, button3))
                return;

            if (checkValues(button4, button5, button6))
                return;


            if (checkValues(button7, button8, button9))
                return;

            if (checkValues(button1, button4, button7))
                return;

            if (checkValues(button2, button5, button8))
                return;

            if (checkValues(button3, button6, button9))
                return;

            if (checkValues(button1, button5, button9))
                return;

            if (checkValues(button3, button5, button7))
                return;
        }

        void changeImage(Button btn) 
        {

            if (gameStatus.gameOver) 
            {
                return;
            }


            if (btn.Tag.ToString() == "?")
            {
                switch (playTurn)
                {
                    case enPlayer.Player1:
                        btn.Image = Resources.X;
                        lblPlayTurn.Text = "Player 2";
                        playTurn = enPlayer.Player2;
                        gameStatus.playCount++;
                        btn.Tag = "X";
                        checkWinner();
                        break;

                    case enPlayer.Player2:
                        btn.Image = Resources.O;
                        lblPlayTurn.Text = "Player 1";
                        playTurn = enPlayer.Player1;
                        gameStatus.playCount++;
                        btn.Tag = "O";
                        checkWinner();
                        break;
                }
            }

            else 
            {
                MessageBox.Show("Wrong Choice", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (gameStatus.playCount == 9) 
            {
                lblPlayTurn.Text = "Game Over";

                gameStatus.winner = enPlayer.Draw;

                gameStatus.gameOver = true;

                endGame(); 
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            changeImage((Button)sender);
        }
        private void resetButtons(Button btn) 
        {
            btn.BackColor = Color.Transparent;
            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            //reset enums
            playTurn = enPlayer.Player1;
            gameStatus.winner = enPlayer.GameInProgress;

            //reset labels
            lblPlayTurn.Text = "Player1";
            lblWinner.Text = "Game In Progress";

            //reset game status
            gameStatus.playCount = 0;
            gameStatus.gameOver = false;

            //reset buttons

            resetButtons(button1);
            resetButtons(button2);
            resetButtons(button3);
            resetButtons(button4);
            resetButtons(button5);
            resetButtons(button6);
            resetButtons(button7);
            resetButtons(button8);
            resetButtons(button9);
        }
    }
}
