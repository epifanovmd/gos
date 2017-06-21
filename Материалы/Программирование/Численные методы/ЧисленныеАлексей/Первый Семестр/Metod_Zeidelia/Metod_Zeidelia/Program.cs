using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metod_Zeidelia
{
    class Program
    {
        static void Main(string[] args)
        {
            double x0, y0, eps, x1;
            int shag = 0 ;
            Console.WriteLine("Программа находит численно корни системы по методу Зейделя:\n\tsin(x) + y = 2\n\t4cos(y) + x = 0");
            Console.Write("Введите x0: ");
            x0 = Double.Parse(Console.ReadLine().Replace('.',','));
            x1 = x0;
            Console.Write("Введите y0: ");
            y0 = Double.Parse(Console.ReadLine().Replace('.', ','));
            Console.Write("Введите eps: ");
            eps = Double.Parse(Console.ReadLine().Replace('.', ','));

            Console.WriteLine("Выражаем из начальной системы х и у:\n\ty = 2 - sin(x)\n\tx = - 4cos(y)");

            do
            {
                shag++;
                Fun(ref x0, ref y0);
                if (Math.Abs(Func1(x0, y0)) < eps && Math.Abs(Func2(x0, y0)) < eps)
                {
                    x1 = Func1(x0, y0);
                        break;
                 
                }

            }
            while (true);

            Console.WriteLine("Корни были найдены за {0} итераций\ny = {1}\nx = {2}", shag, x0, y0);
        }

        private static void Fun(ref double x0, ref double y0)
        {
            double y1 = y0;
            y0 = 2 - Math.Sin(x0);
            x0 = -4 * Math.Cos(y1);
        }

        private static double Func1(double x, double y)
        {
            return 4 * Math.Cos(y) + x;
        }
        private static double Func2(double x, double y)
        {
            return Math.Sin(x) + y - 2;
        }
    }
}
