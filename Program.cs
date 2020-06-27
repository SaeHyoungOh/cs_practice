using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace console_calc
{
	/*
	 * Class to hold user information
	 */
	public class User
	{
		private string userName;

		// default constructor
		public User() { }
		// initializing constructor
		public User(string name)
		{
			userName = name;
		}
		// set userName
		public void SetUserName(string name)
		{
			userName = name;
		}
		// get userName
		public string GetUserName()
		{
			return userName;
		}
	}

	/*
	 * Class to hold numbers and calculate them
	 */
	public class Calculation
	{
		private List<double> numList = new List<double>();
		private List<char> operList = new List<char>();
		private double runningResult = 0;

		// add a number to the operand list
		public int AddNum(double input)
		{
			numList.Add(input);
			return numList.Count;
		}
		// get a number from the operand list
		public double GetNum(int pos)
		{
			return numList[pos];
		}
		// add an operator to the list
		public int AddOper(char input)
		{
			operList.Add(input);
			return operList.Count;
		}
		// get an oeprator from the list
		public char GetOper(int pos)
		{
			if (pos < operList.Count && pos >= 0)
			{
				return operList[pos];
			}
			else
			{
				return '\0';
			}
		}
		public int GetOperSize()
		{
			return numList.Count;
		}
		// set runningResult
		public void SetRunningResult(double num)
		{
			runningResult = num;
		}
		// get runningResult
		public double GetRunningResult()
		{
			return runningResult;
		}

		// clear all lists
		public void Clear()
		{
			numList.Clear();
			operList.Clear();
			runningResult = 0;
		}
		// add a number to runningResult
		public double Add(int pos)
		{
			runningResult += numList[pos];
			return runningResult;
		}
		// subtract runningResult by a number
		public double Subtract(int pos)
		{
			runningResult -= numList[pos];
			return runningResult;
		}
		// multiply runningResult by a number
		public double Multiply(int pos)
		{
			runningResult *= numList[pos];
			return runningResult;
		}
		// divide runningResult by a number
		public double Divide(int pos)
		{
			runningResult /= numList[pos];
			return runningResult;
		}
	}

	/*
	 * Class to hold display interface
	 */
	public class Interface
	{
		User user = new User();

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

	/*
	 * Class to run the console calculator
	 */
	public class ConsoleCalculator
	{
		private Interface ui = new Interface();
		private List<Calculation> calc = new List<Calculation>();
		private List<char> allowedOper = new List<char>() { '+', '-', '*', '/', '=' };
		private int histIndex = 0;
		private bool quitting;

		// get the number from the user
		private bool PromptNum(int pos)
		{
			bool validInput = true;
			do
			{
				string numStr = ui.AskNum(pos);
				// if user enters "x", exit
				if (numStr == "x" || numStr == "X")
				{
					quitting = true;
					return false;
				}
				// if user enters "c", clear the numbers and operators
				else if (numStr == "c" || numStr == "C")
				{
					calc[histIndex].Clear();
					return false;
				}
				// for all other inputs
				else
				{
					// if user enters a valid number
					if (Double.TryParse(numStr, out double result))
					{
						calc[histIndex].AddNum(result);
						validInput = true;
					}
					// if input is not a valid number
					else
					{
						ui.InvalidNum(pos);
						validInput = false;
					}
				}
			} while (!validInput);

			return true;
		}
		// get the operator from the user
		private bool PromptOper(int pos)
		{
			bool validInput = true;
			do
			{
				string operStr = ui.AskOper(pos);
				// if user enters "x", exit
				if (operStr == "x" || operStr == "X")
				{
					quitting = true;
					return false;
				}
				// if user enters "c", clear the numbers and operators
				else if (operStr == "c" || operStr == "C")
				{
					calc[histIndex].Clear();
					return false;
				}
				// for all other inputs
				else
				{
					char operChar;
					// if operator is longer than 1 character, ask again
					if (operStr.Length == 1)
					{
						operChar = operStr[0];
						// if user enters a valid operator
						if (allowedOper.Contains(operChar))
						{
							calc[histIndex].AddOper(operChar);
							validInput = true;
						}
						// if input is not a valid operator
						else
						{
							ui.InvalidOper(pos);
							validInput = false;
						}
					}
				}
			} while (!validInput);

			return true;
		}
		// calculate the equation so far
		private double Calculate(char operChar, int pos)
		{
			// do the calculation
			switch (operChar)
			{
				case '+':
					return calc[histIndex].Add(pos);
				case '-':
					return calc[histIndex].Subtract(pos);
				case '*':
					return calc[histIndex].Multiply(pos);
				case '/':
					return calc[histIndex].Divide(pos);
				default:
					return calc[histIndex].GetRunningResult();
			}

		}
		// one iteration makes one mini-calculation
		private bool Iteration(int pos)
		{
			// get number first
			if (PromptNum(pos))
			{
				// get operator second
				if (PromptOper(pos))
				{
					// do the interim calculation
					if (pos == 0)
					{
						ui.PrintInterim(calc[histIndex].Add(pos));

						// user enters '=' after first number
						if (calc[histIndex].GetOper(pos) == '=')
						{
							ui.PrintFinal(calc[histIndex].GetRunningResult());
							return true;
						}
					}
					// user enters some operator other than '='
					else if (calc[histIndex].GetOper(pos) != '=')
					{
						ui.PrintInterim(Calculate(calc[histIndex].GetOper(pos - 1), pos));
					}
					// user enters '='
					else
					{
						ui.PrintFinal(Calculate(calc[histIndex].GetOper(pos - 1), pos));
					}
					return true;
				}
			}
			return false;
		}
		// running the calculator program
		public void run()
		{
			ui.Init();
			// keep running until user exits
			do
			{
				calc.Add(new Calculation());	// keep track of past calculations

				int pos = 0;
				// keep getting input until user enters '='
				do
				{
					// invalid input will get input again
					if (Iteration(pos))
					{
						pos++;
					}
				} while (!quitting && calc[histIndex].GetOper(pos - 1) != '=');

				histIndex++;
			} while (!quitting);

			// print the history
			Console.WriteLine("\nHistory:");
			foreach (var item in calc)
			{
				// print each calculation, separated by entering '='
				for (int i = 0; i < item.GetOperSize(); i++)
				{
					Console.Write(item.GetNum(i));
					Console.Write(' ');
					Console.Write(item.GetOper(i));
					Console.Write(' ');
				}
				if (item.GetOperSize() > 0)
				{
					Console.WriteLine(item.GetRunningResult());
				}
			}
			Console.WriteLine();

			ui.Goodbye();
		}
	}

	/*
	 * Class for main method
	 */
	public class Program
	{ 
		static void Main(string[] args)
		{
			ConsoleCalculator program = new ConsoleCalculator();
			program.run();
		}
	}
}
