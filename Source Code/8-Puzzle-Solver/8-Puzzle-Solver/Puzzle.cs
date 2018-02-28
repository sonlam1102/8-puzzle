using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8_Puzzle_Solver
{
    class Puzzle
    {
        int[,] Matrix;
        int h;
        int n;
        int[,] dad;
        public Puzzle(int n)
        {
            this.n = n;
            Matrix = new int[n, n];
            dad = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Matrix[i, j] = new int();
                    Matrix[i, j] = 0;
                }
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    dad[i, j] = new int();
                    dad[i, j] = 0;
                }
            h = 0;

        }
        public Puzzle() { }
        public int geth()
        {
            return h;
        }
        public void seth(int h)
        {
            this.h = h;
        }
        public int[,] getMatrix()
        {
            int[,] M = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    M[i, j] = new int();
                    M[i, j] = this.Matrix[i, j];
                }
            return M;
        }
        public void setMatrix(int[,] Matrix)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    this.Matrix[i, j] = Matrix[i, j];
        }
        public int[,] getDad()
        {
            int[,] M = new int[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    M[i, j] = new int();
                    M[i, j] = this.dad[i, j];
                }
            return M;
        }
        public void setDad(int[,] p)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    this.dad[i, j] = p[i, j];
        }
    }
}
