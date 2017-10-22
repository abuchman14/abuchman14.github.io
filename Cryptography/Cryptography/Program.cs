using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Cryptography
{

    class Program
    {
        static int count;
        static void Main(string[] args)
        {
            Console.WriteLine("Available Methods:");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("Generators Mod P: Type \"generator\" to run this method");
            Console.WriteLine();
            Console.WriteLine("Powers of g mod p: Type \"powers\" to run this method");
            Console.WriteLine();
            Console.WriteLine("Number of elements in Z mod n: Type \"numelements\" to run this method");
            Console.WriteLine();
            Console.WriteLine("Solovay Strassen Test: Type \"ss\" to run this method");
            Console.WriteLine();
            Console.WriteLine("Quadratic Sieve Factoring: Type \"qs\" to run this method");
            Console.WriteLine();
            Console.WriteLine("Help: Type \"help\" for a description of each of the methods");
            Console.WriteLine();
            Console.WriteLine("Type \"q\" or \"quit\" to quit");

            string line = Console.ReadLine();
            string[] input = line.Split();

            while (input[0].ToLower() != "q" && input[0].ToLower() != "quit")
            {
                if (input[0].ToLower() == "generators")
                {
                    Console.WriteLine("[Generators]  \nEnter a prime number (p), for the modulus:");
                    int p = Convert.ToInt32(input[1]);
                    printGeneratorsModP(p);
                }

                else if (input[0].ToLower() == "powers")
                {
                    Console.WriteLine("[Powers of g mod p] \nEnter a prime number (p), for the modulus:");
                    int q = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter a generator (g) mod p:");
                    int g = Convert.ToInt32(Console.ReadLine());
                    printPowersOfGModP(q, g);
                }

                else if (input[0].ToLower() == "numelements")
                {
                    Console.WriteLine("[Number of Elements in Z mod n] \nEnter a number (n):");
                    int n = Convert.ToInt32(Console.ReadLine());
                    NumberOfElementsInZmodN(n);
                }
                else if (input[0].ToLower() == "ss")
                {
                    Console.WriteLine("[Solovay Strassen Test] \n Enter a number (n)");
                    int c = Convert.ToInt32(Console.ReadLine());
                    solovayStrassenTest(c);
                }

                else if (input[0].ToLower() == "qs")
                {
                    Console.Write("[Quadratic Sieve Factoring] \n Enter a number (n) to be factored:");
                    long n1 = Convert.ToInt64(Console.ReadLine());
                    quadraticSieve(n1);
                }

                else if (input[0] == "help")
                {
                    Console.WriteLine("Available Methods:");
                    Console.WriteLine("--------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("[Generators Mod P] Returns all numbers which, when raised to the powers 1 through p, \n generate all the numbers from 1 to p");
                    Console.WriteLine("Type \"generator\" to run this method");
                    Console.WriteLine();
                    Console.WriteLine("[Powers of g mod p] Prompts user for a number g and a modulus p. Calculates g to the \n all the powers 1 through p (mod p) and lists them");
                    Console.WriteLine("Type \"powers\" to run this method");
                    Console.WriteLine();
                    Console.WriteLine("[Number of elements in Z mod n] Calculates the number of elements in the set of \n integers mod n -- for any prime number p, this is p-1. More interesting for composite numbers, where the number of elements is fewer.");
                    Console.WriteLine("Type \"numelements\" to run this method");
                    Console.WriteLine();
                    Console.WriteLine("[Solovay Strassen Test] Runs the Solovay Strassen test on a given number n");
                    Console.WriteLine("Type \"ss\" to run this method");
                    Console.WriteLine();
                    Console.WriteLine("[Quadratic Sieve Factoring] Factors (sqrt(n) + i)^2 for increasing values of i. \n Doesn't factor n for you, but helps with the process of finding factors using the quadratic sieve method");
                    Console.WriteLine("Type \"qs\" to run this method");
                    Console.WriteLine();
                    Console.WriteLine("Type \"q\" or \"quit\" to quit");
                }

                else
                {
                    Console.WriteLine("Sorry, command not recognized. Please try again");
                }

                line = Console.ReadLine();
                input = line.Split();
            }
        }

        private static void printGeneratorsModP(int p)
        {
            Console.WriteLine("Generators mod p: ");
            for (int g = 1; g < p - 1; g++)
            {
                List<Tuple<string, long>> arr = new List<Tuple<string, long>>();
                for (int i = 1; i < p; i++)
                {
                    Tuple<string, long> val = new Tuple<string, long>("g^" + i + " = ", modpow(g, i, p));
                    arr.Add(val);
                }
                arr.Sort((x, y) => x.Item2.CompareTo(y.Item2));

                if (checkArray(arr))
                {
                    Console.Write(g + ",  ");
                    count++;
                }

            }

            if (p == 2)
            {
                Console.WriteLine("1");
                count = 1;
            }
            Console.WriteLine("Count: " + count);
            Console.WriteLine();

        }

        //Given a generator g and a mod p, computes all powers of g mod p and sorts them.
        //Prints g^x1 = 1, g^2 = 2, and so on, with values sorted in order
        private static void printPowersOfGModP(int p, int g)
        {
            List<Tuple<string, long>> arr = new List<Tuple<string, long>>();
            for (int i = 1; i < p; i++)
            {
                Tuple<string, long> val = new Tuple<string, long>("g^" + i + " = ", modpow(g, i, p));
                arr.Add(val);
            }
            arr.Sort((x, y) => x.Item2.CompareTo(y.Item2));

            for (int i = 0; i < p - 1; i++)
                Console.WriteLine(arr[i].Item1 + arr[i].Item2);

            if (!checkArray(arr))
            {
                Console.WriteLine(g + " is not a generator mod " + p);
            }

        }

        private static void NumberOfElementsInZmodN(int n)
        {

            Console.WriteLine("Number of elements in Z mod n: " + ElementsInZmodN(n).Count);
            Console.WriteLine();

        }

        private static void solovayStrassenTest(int n)
        {
            if (n % 2 == 0 || n < 0)
            {
                Console.WriteLine("n must be an odd integer > 0");
                return;
            }


            int witnesses = 0;
            int liars = 0;
            foreach (int element in ElementsInZmodN(n))
            {
                Console.WriteLine(element);
                int jacobiVal = jacobi(element, n);
                int eulerCriterion = (int)modpow(element, (n - 1) / 2, n);
                if (eulerCriterion == n - 1)
                    eulerCriterion = -1;
                Console.WriteLine("  Jacobi (" + element + "/" + n + ") = " + jacobiVal + " ");
                Console.WriteLine("  " + element + "^((n-1)/2) = " + eulerCriterion);

                if (jacobiVal != eulerCriterion)
                    witnesses++;
                else
                    liars++;
            }
            if (liars == n - 1)
                Console.WriteLine(n + " is prime");
            else
            {
                Console.WriteLine("Number of Witnesses: " + witnesses);
                Console.WriteLine("Number of Liars: " + liars);
            }
            Console.WriteLine();
        }

        /* Adapted from http://2000clicks.com/mathhelp/NumberTh27JacobiSymbolAlgorithm.aspx */
        public static int jacobi(int a, int b)
        {
            if (b <= 0 || b % 2 == 0)
            {
                Console.WriteLine("b must be an odd integer > 0");
                return 0;
            }

            int j = 1;
            if (a < 0)
            {
                a = -a;
                if ((b % 4) == 3)
                    j = -j;
            }
            do
            {
                while ((a % 2) == 0)
                {
                    /* Process factors of 2: Jacobi(2,b)=-1 if b=3,5 (mod 8) */
                    a = a / 2;
                    if (b % 8 == 3 || b % 8 == 5)
                        j = -j;
                }
                /* Quadratic reciprocity: Jacobi(a,b)=-Jacobi(b,a) if a=3,b=3 (mod 4) */
                int temp = b;
                b = a;
                a = temp;
                if (a % 4 == 3 && b % 4 == 3)
                    j = -j;
                a = a % b;
            } while (a != 0);
            if (b == 1)
            {
                return j;
            }
            else return 0;
        }

        public static void quadraticSieve(long n)
        {
            int r = (int)Math.Sqrt(n);
            Console.WriteLine("Press enter to print the next value. Press q to quit");
            int i = 1;
            while (i < 100)
            {
                string input = Console.ReadLine();
                if (input == "q")
                    return;
                Console.Write("(sqrt(n) + " + i + ")^2 = ");
                List<long> factors = Factors(modpow(r + i, 2, n));
                factors.Sort();
                if (factors.Count != 0)
                {
                    Console.Write(factors[0]);
                    for (int j = 1; j < factors.Count; j++)
                    {
                        Console.Write("*" + factors[j]);
                    }
                }
                i++;

            }
        }

        //Helper method for determining if a number is a generator
        private static bool checkArray(List<Tuple<String, long>> arr)
        {
            for (int i = 0; i < arr.Count - 1; i++)
            {
                if (arr[i].Item2 != i + 1)
                    return false;
            }
            return true;
        }

        //Modular Exponentiation
        static long modpow(long b, long exponent, long modulus)
        {

            long result = 1;

            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                {
                    result = (result * b) % modulus;
                }
                exponent >>= 1;
                b = (b * b) % modulus;
            }

            return result;
        }

        private static long gcd(long a, long b)
        {
            while (b > 0)
            {
                long aModB = a % b;
                a = b;
                b = aModB;
            }
            return a;
        }

        //Returns the a list of the numbers in (Z mod N)*  
        private static List<int> ElementsInZmodN(int n)
        {
            List<int> elements = new List<int>();
            for (int i = 0; i < n; i++)
            {
                if (gcd(i, n) == 1)
                    elements.Add(i);
            }
            return elements;
        }

        //Basic function for factoring a number. Would not work well for very large numbers
        /* From http://stackoverflow.com/questions/5872962/prime-factors-in-c-sharp/5873129 */
        private static List<long> Factors(long n)
        {
            List<long> retval = new List<long>();
            for (int b = 2; n > 1; b++)
            {
                while (n % b == 0)
                {
                    n /= b;
                    retval.Add(b);
                }
            }
            if (retval.Count == 0)
                retval.Add(n);
            return retval;

        }
    }
}


