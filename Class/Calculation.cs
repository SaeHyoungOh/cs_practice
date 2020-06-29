using System.Collections.Generic;

namespace console_calc.Class
{
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
}