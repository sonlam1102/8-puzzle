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

namespace _8_Puzzle_Solver
{

    public partial class Form1 : Form
    {
        bool isBreak = false;
        List<Puzzle> lP = new List<Puzzle>();
        int[,] M = new int[3, 3];
        bool isPressed = false;
        Process p = new Process();

        public Form1()
        {
            InitializeComponent();
        }
        public String[,] ConvertMatrix(int[,]M)
        {
            String[,] s = new String[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (M[i, j] == -1)
                        s[i, j] = "*";                      
                    else
                        s[i, j] = Convert.ToString(M[i, j]);
            return s;
        }
        public void setMatrixtolabel(String[,]Matrix)
        {
            label1.Text = Matrix[0, 0];
            label2.Text = Matrix[1, 0];
            label3.Text = Matrix[2, 0];
            label4.Text = Matrix[0, 1];
            label5.Text = Matrix[1, 1];
            label6.Text = Matrix[2, 1];
            label7.Text = Matrix[0, 2];
            label8.Text = Matrix[1, 2];
            label9.Text = Matrix[2, 2];
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (isPressed == false)
            {
                MessageBox.Show("Chưa khởi tạo giá trị cho Puzzle! ","8-Puzzle-Solver");
            }
            else
            {
                p.Processing(M);
                lP = p.returnResult();
                lbTB.Text = "Tổng số bước: " + Convert.ToString(p.TotalSteps());
                String[,] Matrix = new String[3, 3];
                pB.Minimum = 0;
                pB.Maximum = lP.Count;
                pB.Value = 0;
                pB.Step = 1;
                for (int i = 0; i < lP.Count; i++)
                {
                    if (isBreak == true)
                        break;
                    Matrix = ConvertMatrix(lP[i].getMatrix());
                    setMatrixtolabel(Matrix);
                    pB.PerformStep();
                    Thread.Sleep(500);
                    Application.DoEvents();
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            isBreak = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int n=3;
            String[,] Ma = new String[3, 3];
            p.initialGame(ref M, n);
            Ma = ConvertMatrix(M);
            setMatrixtolabel(Ma);          
            isPressed = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            isPressed = false;
            String[,] Ma = new String[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Ma[i, j] = "-";
            setMatrixtolabel(Ma);
            pB.Value = 0;
            lbTB.Text = "";
        }   
    }
}
