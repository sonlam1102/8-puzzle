using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8_Puzzle_Solver
{
    class Position
    {
        int row;
        int column;
        public Position() { row = -1; column = -1; }
        public int getRow()
        {
            return row;
        }
        public int getColumn()
        {
            return column;
        }
        public void setRow(int r)
        {
            this.row = r;
        }
        public void setColumn(int col)
        {
            this.column = col;
        }
    }
}
