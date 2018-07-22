using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToeCSharp
{
    public partial class Form1 : Form
    {
        private enum PlayerTurn
        {
            None,
            Player1,
            Player2
        };

        private enum Winner { None, Player1, Player2, Draw};

        private PlayerTurn turn;
        private Winner winner;
        private void OnNewGame()
        {
            turn = PlayerTurn.Player1;
            //lblStatus.Text = "";
            //pictureBox0.Image = null;
            PictureBox[] allPictures =
            {
                pictureBox0, pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7,
                pictureBox8
            };
            // clear all pictures
            foreach (var p in allPictures)
            {
                p.Image = null;
            }

            winner = Winner.None;
            ShowTurn();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void OnClick(object sender, EventArgs e)
        {
            
            PictureBox p = sender as PictureBox;
           // MessageBox.Show(p.Name);
            if (p.Image != null)
                return;
            if(turn==PlayerTurn.None)
                return;
            ;
            if (turn==PlayerTurn.Player1)
            {
                p.Image = player1.Image;
            }
            else
            {
                p.Image = player2.Image;
            }
            // Check for a winner
            winner = GetWinner();

            // Change turns
            if(winner == Winner.None) { 
            turn = (PlayerTurn.Player1 == turn) ? PlayerTurn.Player2 : PlayerTurn.Player1;
            }
            else
            {
                turn = PlayerTurn.None;
            }
            ShowTurn();
        }
        /*
         * +---+---+---+
         * | 0 | 1 | 2 |
         * +---+---+---+
         * | 3 | 4 | 5 |
         * +---+---+---+
         * | 6 | 7 | 8 |
         * +---+---+---+
         */

        private Winner GetWinner()
        {
            PictureBox[] allWinningMoves =
            {
                //check row
                pictureBox0, pictureBox1, pictureBox2,
                pictureBox3, pictureBox4, pictureBox5,
                pictureBox6, pictureBox7, pictureBox8,
                //check colums
                pictureBox0, pictureBox3, pictureBox6,
                pictureBox1, pictureBox4, pictureBox7,
                pictureBox2, pictureBox5, pictureBox8,
                //check diagonal
                pictureBox0, pictureBox4, pictureBox8,
                pictureBox2, pictureBox4, pictureBox6,
                
            };
            for (int i = 0; i < allWinningMoves.Length; i += 3)
            {
                if (allWinningMoves[i].Image != null)
                {
                    if (allWinningMoves[i].Image == allWinningMoves[i + 1].Image &&
                        allWinningMoves[i].Image == allWinningMoves[i + 2].Image)
                    {
                        // we have a winner
                        if (allWinningMoves[i].Image == player1.Image)
                            return Winner.Player1;
                        else
                            return Winner.Player2;
                    }
                }
            }
            // check for empty cell
            PictureBox[] allPicturez =
            {
                pictureBox0, pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7,
                pictureBox8
            };
            // uuu
            foreach (var pz in allPicturez)
            
                if (pz.Image == null)
                    return Winner.None;
            
            //DRAW
            return Winner.Draw;


        }

        void ShowTurn()
        {
            string status = "";
            switch (winner)
            {
                case Winner.None:
                    
                    if (turn == PlayerTurn.Player1)
                        status = "Turn: player 1";
                    else
                      status = "Turn: player 2";
                    break;
                case Winner.Player1:
                    status = "Player 1 wins!";
                    break;
                case Winner.Player2:
                    status = "Player 2 wins!";
                    break;
                case Winner.Draw:
                    status = "It is a draw...";
                    break;
            }

            

            lblStatus.Text = status;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            OnNewGame();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to start a new game?", "New Game",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result==DialogResult.Yes)
            OnNewGame();
        }
    }
}
