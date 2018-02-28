using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _8_Puzzle_Solver
{
    class Process
    {
         List<Puzzle> Result = new List<Puzzle>();
        bool isDup(List<int>l,int a)
         {
             for (int i = 0; i < l.Count; i++)
                 if (a == l[i])
                     return true;
             return false;
         }
        public void GenerateMatrix(ref int [,]Matrix,int n)
        {
            List<int> h=new List<int>();
            Random r = new Random();
            for(int i=0 ;i<n;i++)
                for(int j=0;j<n;j++)
                {
                    if (h.Count <= 0)
                    {
                        Matrix[i, j] = r.Next(0, 9);
                        h.Add(Matrix[i, j]);
                    }
                    else
                    {                       
                        do
                        {
                            Matrix[i, j] = r.Next(0, 9);
                            bool k = isDup(h, Matrix[i, j]);
                        }
                        while (isDup(h, Matrix[i, j]) == true);
                        h.Add(Matrix[i, j]);
                    }
                }
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    if (Matrix[i, j] == 0)
                        Matrix[i, j] = -1;
        }
        public static void Swap(ref int a, ref int b)  //swap to Integer (using to Swap in matrix of the Puzzle)
        {
            int temp;
            temp = a;
            a = b;
            b = temp;
        }
        int Prediction(int[,]Matrix)
        {
            int temp = 0;
            int first=Matrix[0,0];
            int[] linearArray = new int[8];
            int count = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++) 
                    if (Matrix[i, j] != -1)
                    {
                        linearArray[count] = Matrix[i, j];
                        count++;
                    }
            for (int i = 0; i < count; i++)
                for (int j = i + 1; j < count; j++)
                    if (linearArray[j] < linearArray[i])
                        temp++;

           if (temp % 2 == 0)
                return 1;
           else
                return 0;
        }
        public Puzzle LastPuzzle(int [,]Matrix)  //the Best Puzzle (Result for the Game)
        {
            Puzzle p = new Puzzle(3);
            int[,] M = new int[3, 3] { { 1, 2, 3 }, { 8, -1, 4 }, { 7, 6, 5 } };
            int[,] N = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, -1 } };
            if (Prediction(Matrix) == 0)
            {
                p.setMatrix(M);
                p.seth(0);
            }
            else
            {
                p.setMatrix(N);
                p.seth(0);
            }
            return p;
        }
        public Position findMatrix(int val, int[,] Matrix, int n) //return the position of the Matrix (int Puzzle) using Position class
        {
            Position p = new Position();
            bool isbreak = false;
            for (int i = 0; i < n; i++)
            {
                if (isbreak == true)
                    break;
                else
                    for (int j = 0; j < n; j++)
                    {
                        if (Matrix[i, j] == val)
                        {
                            isbreak = true;
                            p.setColumn(j);
                            p.setRow(i);
                            break;
                        }
                    }
            }
            return p;
        }
        public int findPuzzle(Puzzle p, List<Puzzle> lP, int n) //return the position of the Puzzle
        {
            int k = 0;
            for (int i = 0; i < lP.Count; i++)
                if (duplicateMatrix(p, lP[i], n) == true)
                {
                    k = i;
                    break;
                }
            return k;
        }
        public List<Puzzle> SwapPuzzle(Puzzle p, int n, Puzzle init)  //create the State for the Puzzle (4 State: up, down, left, right)
        {
            List<Puzzle> lP = new List<Puzzle>();
            int[,] Matrix = new int[n, n];
            Matrix = p.getMatrix();
            Position pM = new Position();
            pM = findMatrix(-1, Matrix, n);
            if (pM.getColumn() > 0)    //left
            {
                Matrix = p.getMatrix();
                Puzzle pz = new Puzzle(n);
                Swap(ref Matrix[pM.getRow(), pM.getColumn()], ref Matrix[pM.getRow(), pM.getColumn() - 1]);
                int h = Measure(Matrix, init, n);
                pz.setMatrix(Matrix);
                pz.seth(h);
                pz.setDad(p.getMatrix());
                lP.Add(pz);
            }
            if (pM.getColumn() < n - 1)    //right
            {
                Matrix = p.getMatrix();
                Puzzle pz = new Puzzle(n);
                Swap(ref Matrix[pM.getRow(), pM.getColumn()], ref Matrix[pM.getRow(), pM.getColumn() + 1]);
                int h = Measure(Matrix, init, n);
                pz.setMatrix(Matrix);
                pz.seth(h);
                pz.setDad(p.getMatrix());
                lP.Add(pz);
            }
            if (pM.getRow() > 0)    //up
            {
                Matrix = p.getMatrix();
                Puzzle pz = new Puzzle(n);
                Swap(ref Matrix[pM.getRow(), pM.getColumn()], ref Matrix[pM.getRow() - 1, pM.getColumn()]);
                int h = Measure(Matrix, init, n);
                pz.setMatrix(Matrix);
                pz.seth(h);
                pz.setDad(p.getMatrix());
                lP.Add(pz);
            }
            if (pM.getRow() < n - 1)   //down
            {
                Matrix = p.getMatrix();
                Puzzle pz = new Puzzle(n);
                Swap(ref Matrix[pM.getRow(), pM.getColumn()], ref Matrix[pM.getRow() + 1, pM.getColumn()]);
                int h = Measure(Matrix, init, n);
                pz.setMatrix(Matrix);
                pz.seth(h);
                pz.setDad(p.getMatrix());
                lP.Add(pz);
            }
            return lP;
        }
        public bool duplicateMatrix(Puzzle p, Puzzle k, int n)   //return the value to decide if the Matrix in Puzzle is duplicated with others (true if Duplicated)
        {
            bool l = true;
            int[,] M1 = new int[n, n];
            int[,] M2 = new int[n, n];
            M1 = p.getMatrix();
            M2 = k.getMatrix();
            for (int i = 0; i < n; i++)
            {
                if (l == false)
                    break;
                else
                    for (int j = 0; j < n; j++)
                        if (M1[i, j] != M2[i, j])
                        {
                            l = false;
                            break;
                        }
            }
            return l;
        }
        public bool duplicateMatrix(int[,] M1, int[,] M2, int n)   //return the value to decide if the Matrix in Puzzle is duplicated with others (true if Duplicated)
        {
            bool l = true;
            for (int i = 0; i < n; i++)
            {
                if (l == false)
                    break;
                else
                    for (int j = 0; j < n; j++)
                        if (M1[i, j] != M2[i, j])
                        {
                            l = false;
                            break;
                        }
            }
            return l;
        }
        public bool duplicatePuzzle(Puzzle p, List<Puzzle> lP, int n)  //return the value to decide which has had in Puzzle Array (true if has existed)
        {
            bool k = false;
            for (int i = 0; i < lP.Count; i++)
                if (duplicateMatrix(p, lP[i], n) == true)
                {
                    k = true;
                    break;
                }
            return k;
        }
        public Puzzle choosePuzzle(List<Puzzle> lP, int n)  //choose the best state in a set of State
        {
            Puzzle pn = new Puzzle(n);
            int min = 100000;
            int key = 0;
            for (int i = 0; i < lP.Count; i++)
            {
                if (lP[i].geth() < min)
                {
                    min = lP[i].geth();
                    key = i;
                }
            }
            pn.setMatrix(lP[key].getMatrix());
            pn.seth(lP[key].geth());
            pn.setDad(lP[key].getDad());
            return pn;
        }
        public int Measure(int[,] Matrix, Puzzle init, int n) //measure the cost of each state (using Mahattan distance)
        {
            int h = 0;
            int[,] MatrixInit = new int[n, n];
            MatrixInit = init.getMatrix();
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    Position po = new Position();
                    po = findMatrix(Matrix[i, j], MatrixInit, n);
                    h = h + ((Math.Abs(i - po.getRow()) + Math.Abs(j - po.getColumn())));
                }
            return h;
        }
        public List<Puzzle> useAStar(Puzzle p, Puzzle init, int n)  //using A-star Algorithm to find the best state
        {
            List<Puzzle> OPEN = new List<Puzzle>();
            List<Puzzle> CLOSE = new List<Puzzle>();
            Puzzle pz = new Puzzle(n);
            pz.setMatrix(p.getMatrix());  //pz is the initial state of Puzzle input by user
            pz.seth(p.geth());
            OPEN.Add(p); //add initial state to the OPEN
            bool b = false;
            while (b == false)
            {
                OPEN.RemoveAt(findPuzzle(pz, OPEN, n));
                CLOSE.Add(pz);
                if (duplicateMatrix(pz, init, n) == true)  //if the state is the Result then Exit
                {
                    b = true;
                }
                List<Puzzle> lP = SwapPuzzle(pz, n, init);  //the Generated State from the below state 
                for (int i = 0; i < lP.Count; i++)
                {
                    if (duplicatePuzzle(lP[i], OPEN, n) == true) //has had in OPEN, then update its value if its h value is greater than present value
                        if (lP[i].geth() < OPEN[findPuzzle(lP[i], OPEN, n)].geth())
                        {
                            OPEN[findPuzzle(lP[i], OPEN, n)].seth(lP[i].geth());
                            OPEN[findPuzzle(lP[i], OPEN, n)].setDad(lP[i].getDad());
                        }
                    if (duplicatePuzzle(lP[i], CLOSE, n) == true) //has had in OPEN, then update its value if its h value is greater than present value
                        if (lP[i].geth() < CLOSE[findPuzzle(lP[i], CLOSE, n)].geth())
                        {
                            CLOSE[findPuzzle(lP[i], CLOSE, n)].seth(lP[i].geth());
                            CLOSE[findPuzzle(lP[i], CLOSE, n)].setDad(lP[i].getDad());
                        }
                    if (duplicatePuzzle(lP[i], CLOSE, n) == false && duplicatePuzzle(lP[i], OPEN, n) == false) //add to OPEn if it don't have in both OPEN and CLOSE
                    {
                        OPEN.Add(lP[i]);
                    }
                }
                pz = choosePuzzle(OPEN, n);
            }
            return CLOSE;
        }
        List<Puzzle> BackTrack(Puzzle p, List<Puzzle> lP, int n, Puzzle init)  //return the Parent of currently State (to build the Solution)
        {
            List<Puzzle> BPz = new List<Puzzle>();
            int k = 0;
            BPz.Add(init);
            while (k <= lP.Count)
            {
                for (int i = 1; i < lP.Count; i++)
                    if (duplicateMatrix(p.getDad(), lP[i].getMatrix(), n) == true)
                    {
                        BPz.Add(lP[i]);
                        p.setMatrix(lP[i].getMatrix());
                        p.seth(lP[i].geth());
                        p.setDad(lP[i].getDad());
                    }
                k++;
            }
            return BPz;
        }
        public void initialGame(ref int [,]Matrix,int n)
        {
            n = 3;
            Matrix = new int[n, n];
            GenerateMatrix(ref Matrix, n);
        }
        public void Processing(int [,]Matrix)  //Proccess the Program
        {
            //1. Input
            int n = 3;
            //2. Initial the Best State (Result)           
            Puzzle p = new Puzzle(n);
            Puzzle init = new Puzzle(n);
            init = LastPuzzle(Matrix);
            p.setMatrix(Matrix);
            p.seth(Measure(Matrix, init, n));
            List<Puzzle> lP = new List<Puzzle>();
            //3. Execute it using A-star Algorithm
            lP = useAStar(p, init, n);
            //4. Show the Result          
            Result = BackTrack(lP[lP.Count - 1], lP, n, init);
            Result.Reverse(); //reverse the List
            //ShowResult(Result, n);
        }
        public List<Puzzle>returnResult()
        {
            return this.Result;
        }
        public int TotalSteps()
        {
            return Result.Count;
        }
    }
}
