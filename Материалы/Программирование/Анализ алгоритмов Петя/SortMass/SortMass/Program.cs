using System;

namespace SortMass
{
    public class Sort
    {
        private readonly Func<int, int, bool> _equals;
        public Sort( Func<int,int,bool> equals = null )
        {
            if (equals == null)
                _equals = ( x,y ) => x > y;
            else
                _equals = equals;
        }
        public int[] InsertionSort( int[] A )
        {
            A = (int[])A.Clone();
            for (int j = 1; j < A.Length; j++)
            {
                int key = A[j];
                int i = j - 1;
                while (i >= 0 && _equals(A[i],key))
                {
                    A[i + 1] = A[i];
                    i--;
                }
                A[i + 1] = key;
            }
            return A;
        }
        public int[] QuickSort( int[] A )
        {
            A = (int[])A.Clone();
            QuickSortRec(A,0,A.Length - 1);
            return A;
        }
        private int[] QuickSortRec( int[] A,int p,int r )
        {
            if (p < r)
            {
                int q = Partition(A,p,r);
                QuickSortRec(A,p,q - 1);
                QuickSortRec(A,q + 1,r);
            }
            return A;
        }
        private int Partition( int[] A,int p,int r )
        {
            int x = A[r];
            int i = p;
            for (int j = p; j < r; j++)
            {
                if (!_equals(A[j],x))
                {
                    int aa = A[i];
                    A[i] = A[j];
                    A[j] = aa;
                    i++;
                }
            }
            int a = A[i];
            A[i] = A[r];
            A[r] = a;
            return i;
        }
        public int[] MergeSort( int[] A )
        {
            A = (int[])A.Clone();
            MergeSortRec(A,0,A.Length - 1);
            return A;
        }
        private int[] MergeSortRec( int[] A,int p,int r )
        {
            if (p < r)
            {
                int q = (int)Math.Floor((decimal)(p + r) / 2);
                MergeSortRec(A,p,q);
                MergeSortRec(A,q + 1,r);
                Merge(A,p,q,r);
            }
            return A;
        }
        private void Merge( int[] A,int p,int q,int r )
        {
            int n1 = q - p + 1;
            int n2 = r - q;
            int[] L = new int[n1 + 1];
            int[] R = new int[n2 + 1];
            for (int i = 0; i < n1; i++)
            {
                L[i] = A[p + i];
            }
            for (int j = 0; j < n2; j++)
            {
                R[j] = A[q + 1 + j];
            }
            L[n1] = int.MaxValue;
            R[n2] = int.MaxValue;
            int ii = 0, jj = 0;
            for (int k = p; k <= r; k++)
            {
                if (L[ii] <= R[jj])
                {
                    A[k] = L[ii];
                    ii++;
                }
                else
                {
                    A[k] = R[jj];
                    jj++;
                }
            }
        }
        public int[] PyzSort( int[] A )
        {
            A = (int[])A.Clone();
            bool k;
            do
            {
                k = false;
                for (int i = 0; i < A.Length - 1; i++)
                {
                    if (_equals(A[i],A[i + 1]))
                    {
                        k = true;
                        int a = A[i + 1];
                        A[i + 1] = A[i];
                        A[i] = a;
                    }
                }

            } while (k);
            return A;
        }

        public bool lol( int[] A )
        {
            A = (int[])A.Clone();
            return lolRec(A,0,A.Length - 1);
        }

        private bool lolRec( int[] A,int p,int r )
        {
            var v = false;
            if (p < r)
            {
                int q = (int)Math.Floor((p + r) / 2.0);
                v = v || lolRec(A,p,q);
                v = v || lolRec(A,q + 1,r);
                v = v || A[p] == p || A[r] == r;
            }
            return v;
        }

    }

    class Program
    {
        static void Main( string[] args )
        {
            int[] mass = new[] { 1,4,6,2,7,8,10,12 };
            for (int i = 0; i < mass.Length; i++)
            {
                Console.Write(mass[i] + " ,");
            }
            Console.WriteLine();

            //убывание сортировка
            //var sort = new Sort(( x,y ) => x < y);

            //возрастание сортировка
            var sort = new Sort();

            Console.WriteLine(sort.lol(new[] { 0,2,3,4,5 }));

            var result = sort.InsertionSort(mass);
            var result1 = sort.MergeSort(mass); // не работает обратная сортировка
            var result2 = sort.PyzSort(mass);
            var result3 = sort.QuickSort(mass);

            for (int i = 0; i < result.Length; i++)
            {
                Console.Write(result[i] + " ,");
            }
            Console.WriteLine();

            for (int i = 0; i < result1.Length; i++)
            {
                Console.Write(result1[i] + " ,");
            }
            Console.WriteLine();

            for (int i = 0; i < result2.Length; i++)
            {
                Console.Write(result2[i] + " ,");
            }
            Console.WriteLine();

            for (int i = 0; i < result3.Length; i++)
            {
                Console.Write(result3[i] + " ,");
            }
            Console.WriteLine();
        }
    }
}
