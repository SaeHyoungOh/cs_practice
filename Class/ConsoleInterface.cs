using System;

namespace console_calc.Class
{
	/*
	 * Class to hold display interface
	 */

	public class ConsoleInterface
	{
		private User user = new User();

		// initialize the interface and get the user's name
		public void Init()
		{
			Console.WriteLine("Calculator for console - by Sae Hyoung Oh");
			Console.Write("Enter your name: ");
			string name = Console.ReadLine();
			user.SetUserName(name);

			Console.WriteLine("\nHello, " + name + ".\n");
			Console.WriteLine("Allowed operands: +, -, *, /, =");
			Console.WriteLine("Enter \'c\' to clear, \'x\' to exit.");
		}

		// ask for a number
		public string AskNum(int pos)
		{
			Console.Write("Enter number " + (pos + 1) + ": ");

			return Console.ReadLine();
		}

		// ask for an operator
		public string AskOper(int pos)
		{
			Console.Write("Enter operator " + (pos + 1) + ": ");

			return Console.ReadLine();
		}

		// when input is invalid number
		public void InvalidNum(int pos)
		{
			Console.WriteLine("invalid number");
		}

		// when input is invalid operator
		public void InvalidOper(int pos)
		{
			Console.WriteLine("invalid operator");
		}

		// print interim result
		public void PrintInterim(double num)
		{
			Console.WriteLine("Interim Result: " + num);
		}

		// print final result
		public void PrintFinal(double num)
		{
			Console.WriteLine("Final Result: " + num + '\n');
		}

		// say goodbye and exit
		public void Goodbye()
		{
			Console.WriteLine("\nGood bye, " + user.GetUserName() + ".");
			Environment.Exit(0);
		}
	}
}