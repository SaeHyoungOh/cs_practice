using System;
using System.Collections.Generic;

namespace console_calc.Class
{
	/*
	 * Class to run the console calculator
	 */

	public class ConsoleCalculator
	{
		private ConsoleInterface ui = new ConsoleInterface();
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
				calc.Add(new Calculation());    // keep track of past calculations

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
}