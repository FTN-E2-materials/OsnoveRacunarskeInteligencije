using System;

namespace LambdaIzrazi
{
    class Program
    {
        //delegat polje, može mu se dodeliti bilo kakva funkcija sa int parametrom.
        delegate int del(int i);
        //delegat sa dva parametra.
        delegate int del2(int i, int j);

        static void Main(string[] args)
        {

            del myDelegate = x => x * x;
            int rez = myDelegate(5); //rez = 25
            del2 myDelegate2 = (x1, x2) => x1 * x2;
            int rez2 = myDelegate2(5, 5); //rez = 25

            Console.Write("Rez1: "+ rez + " Rez2: "+ rez2);

        }
    }
}
