using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace lab4
{
    public partial class Form1 : Form
    {
        List<Button> buttons;

        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "20201032";
            buttons = new List<Button>() { btn_1,btn_2,btn_3,btn_4,btn_5,btn_6,btn_7,btn_8,btn_9 };
        }
        public void SetDisable()
        {
            for (int i = 0; i < 9; i++)
            {
                buttons[i].Enabled = false;
            }
        }

      
        public static string[]  array = new string[9];
        
        public static int endgame(string[] arr)
        {
            if ((arr[0] == "x" && arr[1] == "x" && arr[2] == "x") || (arr[3] == "x" && arr[4] == "x" && arr[5] == "x") ||
                (arr[6] == "x" && arr[7] == "x" && arr[8] == "x") || (arr[0] == "x" && arr[3] == "x" && arr[6] == "x") ||
                (arr[1] == "x" && arr[4] == "x" && arr[7] == "x") || (arr[2] == "x" && arr[5] == "x" && arr[8] == "x") ||
                (arr[0] == "x" && arr[4] == "x" && arr[8] == "x") || (arr[2] == "x" && arr[4] == "x" && arr[6] == "x"))
            {
                return 10;
            }
            else if ((arr[0] == "o" && arr[1] == "o" && arr[2] == "o") || (arr[3] == "o" && arr[4] == "o" && arr[5] == "o") ||
            (arr[6] == "o" && arr[7] == "o" && arr[8] == "o") || (arr[0] == "o" && arr[3] == "o" && arr[6] == "o") ||
            (arr[1] == "o" && arr[4] == "o" && arr[7] == "o") || (arr[2] == "o" && arr[5] == "o" && arr[8] == "o") ||
            (arr[0] == "o" && arr[4] == "o" && arr[8] == "o") || (arr[2] == "o" && arr[4] == "o" && arr[6] == "o"))
            {
                return -10;
            }
            return 0;
        }
        public static int[,] board = { { -1, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } };
        public static List<int> scores = new List<int>();
        static int MAX = 1000;
        static int MIN = -1000;
        static int bestMoveI = 0;
        static int bestMoveJ = 0;
        static List<int[]> GetPossibleMoves(int[,] board)
        {
            List<int[]> result = new List<int[]>();
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == -1)
                    {
                        result.Add(new int[] { i, j });
                    }
                }
            }
            return result;
        }
        public static int[] AIMove(int i, int j)
        {
            board[i, j] = 0;
            List<int[]> possibleMoves = GetPossibleMoves(board);
            int bestScore = int.MinValue;
            foreach (int[] move in possibleMoves)
            {
                board[move[0], move[1]] = 1;
                int score = Minimax(0, false, board);
                board[move[0], move[1]] = -1;
                if (score > bestScore)
                {
                    bestScore = score;
                    bestMoveI = move[0];
                    bestMoveJ = move[1];
                }
            }
            board[bestMoveI, bestMoveJ] = 1;
            int[] result = new int[2];
            result[0] = bestMoveI;
            result[1] = bestMoveJ;
            return result;
        }
        static int Minimax(int depth, bool maximizingPlayer, int[,] board)
        {
            List<int[]> possibleMoves = GetPossibleMoves(board);
            int winner = CheckWinner(board);
            if (winner == 10)
                return 10 - depth;
            if (winner == -10)
                return -10 + depth;
            if (possibleMoves.Count <= 0 & winner == 0)
                return 0 + depth;
            if (maximizingPlayer)
            {
                int best = MIN;
                foreach (int[] move in possibleMoves)
                {
                    board[move[0], move[1]] = 1;
                    int score = Minimax(depth + 1, false, board);
                    best = Math.Max(best, score);
                    board[move[0], move[1]] = -1;
                }
                return best;
            }
            else
            {
                int best = MAX;
                foreach (int[] move in possibleMoves)
                {
                    board[move[0], move[1]] = 0;
                    int score = Minimax(depth + 1, true, board);
                    best = Math.Min(best, score);
                    board[move[0], move[1]] = -1;

                }
                return best;
            }
        }
        private static int CheckWinner(int[,] board)
        {
            int[] results =
            {
                board[0,0],board[0,1],board[0,2],board[1,0],board[1,1],board[1,2],board[2,0],board[2,1],board[2,2],
                board[0,0],board[1,0],board[2,0],board[0,1],board[1,1],board[2,1],board[0,2],board[1,2],board[2,2],
                board[0,0],board[1,1],board[2,2],board[0,2],board[1,1],board[2,0]
            };
            for (int i = 0; i < results.Length; i += 3)
            {
                if (results[i] == 0 && results[i + 1] == 0 && results[i + 2] == 0)
                    return -10;
                else if (results[i] == 1 && results[i + 1] == 1 && results[i + 2] == 1)
                    return 10;
            }
            return 0;
        }
        public static int mFinishGame()
        {
            int score = CheckWinner(board);
            if (GetPossibleMoves(board).Count <= 0 & score == 0)
            {
                return score;
            }
            else if (score == 10)
                return score;
            else if (score == -10)
                return score;
            return -1;
        }
        public static void ReInitializeBoard()
        {
            board = new int[,] { { -1, -1, -1 }, { -1, -1, -1 }, { -1, -1, -1 } };
        }
    

    private void btn_1_Click(object sender, EventArgs e)
        {
            if (btn_1.Text == "")
            {
                btn_1.Text = "X";
                btn_1.BackColor = Color.DarkSeaGreen;
                array[0] = "x";
                btn_1.Enabled = false;
            }
            endgame(array);
            ClickAI(0,0);
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            if (btn_2.Text == "")
            {
                btn_2.Text = "X";
                btn_2.BackColor = Color.DarkSeaGreen;
                array[1] = "x";
                btn_2.Enabled = false;
            }
            endgame(array);
            ClickAI(0, 1);

        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            if (btn_3.Text == "")
            {
                array[2] = "x";
                btn_3.Text = "X";
                btn_3.BackColor = Color.DarkSeaGreen;
                btn_3.Enabled = false;
            }
            endgame(array);
            ClickAI(0, 2);

        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            if (btn_4.Text == "")
            {
                array[3] = "x";
                btn_4.Text = "X";
                btn_4.BackColor = Color.DarkSeaGreen;
                btn_4.Enabled = false;
            }
            endgame(array);
            ClickAI(1, 0);

        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            if (btn_5.Text == "")
            {
                array[4] = "x";
                btn_5.Text = "X";
                btn_5.BackColor = Color.DarkSeaGreen;
                btn_5.Enabled = false;
            }
            endgame(array);
            ClickAI(1, 1);
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            if (btn_6.Text == "")
            {
                array[5] = "x";
                btn_6.Text = "X";
                btn_6.BackColor = Color.DarkSeaGreen;
                btn_6.Enabled = false;
            }
            endgame(array);
            ClickAI(1, 2);
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            if (btn_7.Text == "")
            {
                array[6] = "x";
                btn_7.Text = "X";
                btn_7.BackColor = Color.DarkSeaGreen;
                btn_7.Enabled = false;
            }
            endgame(array);
            ClickAI(2, 0);
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            if (btn_8.Text == "")
            {
                array[7] = "x";
                btn_8.Text = "X";
                btn_8.BackColor = Color.DarkSeaGreen;
                btn_8.Enabled = false;
            }
            endgame(array);
            ClickAI(2, 1);
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            if (btn_9.Text == "")
            {
                array[8] = "x";
                btn_9.Text = "X";
                btn_9.BackColor = Color.DarkSeaGreen;
                btn_9.Enabled = false;
            }
            endgame(array);
            ClickAI(2, 2);
        }
        private void ClickAI(int x, int y)
        {
            int winner = mFinishGame();
            if (winner == -1)
            {
                int[] aimove = AIMove(x, y);
                int index = aimove[0] * 3 + aimove[1];
                for (int i = 0; i < 9; i++)
                {
                    if (buttons[i].Name == $"btn_{index+1}")
                    {
                        buttons[i].Text = "O";
                        buttons[i].BackColor = Color.LightSkyBlue;
                        buttons[i].Enabled = false;
                        array[index] = "o";
                        endgame(array);
                    }
                }
                int winnerAfterAI = mFinishGame();
                if (winnerAfterAI != -1)
                {
                    FinishGame(winnerAfterAI);
                }
            }
            else
            {
                FinishGame(winner);
            }

        }
        private void FinishGame(int score)
        {
            string whowin = "x";
            textBox1.BackColor = Color.White;
            if (score == 0) {
                MessageBox.Show(this, "DRAW", "Tic Tac Toe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                whowin = "draw";
            }
            else if (score == 10) {
                MessageBox.Show(this, "O WON", "Tic Tac Toe", MessageBoxButtons.OK, MessageBoxIcon.Information);
                whowin = "o";
            }
            else
                MessageBox.Show(this, "X WON", "Tic Tac Toe", MessageBoxButtons.OK, MessageBoxIcon.Information);
           
            SetDisable();
            }
    
    private void btn_reset_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
