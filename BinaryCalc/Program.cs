using System;
using System.Linq;
using System.Text;

namespace BinaryCalc
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("First number: ");
			var firstNumber = Console.ReadLine();

			Console.WriteLine("Second number: ");
			var secondNumber = Console.ReadLine();

			var r = BinaryPlus(firstNumber, secondNumber);

			Console.WriteLine("Result: ");
			Console.WriteLine(r);

			Console.WriteLine("Result dec: " + ConvertToDec(r));
			Console.WriteLine("Result hex: " + ConverTo(r, SystemType.hex));
			Console.WriteLine("Result oct: " + ConverTo(r, SystemType.oct));


		}

		private static string BinaryPlus(string n1, string n2)
		{

			string biggestLine, lowestLine;

			if (n1.Length > n2.Length)
			{
				biggestLine = n1;
				lowestLine = n2;
			}
			else
			{
				lowestLine = n1;
				biggestLine = n2;
			}

			var result = new StringBuilder();

			var isInc = false;

			for (var i = 0; i < lowestLine.Length; i++)
			{

				var l = int.Parse(lowestLine[lowestLine.Length - i - 1].ToString());
				var b = int.Parse(biggestLine[biggestLine.Length - i - 1].ToString());

				var r = l + b;

				if (isInc)
					r++;

				if (r == 2)
				{
					isInc = true;
					r = 0;
				}
				else if (r == 3)
				{
					isInc = true;
					r = 1;
				}
				else
				{
					isInc = false;
				}

				result.Insert(0, r.ToString());

				if (isInc && lowestLine.Length -1 == i)
				{
					if (lowestLine.Length < biggestLine.Length)
					{
						lowestLine = "1" + lowestLine;
						isInc = false;
					}
					else
					{
						result.Insert(0, "1");
					}
					
				}

			}

			for (var i = 0; i < biggestLine.Length - lowestLine.Length; i++) {
				result.Insert(0, biggestLine[i].ToString());
			}

			return result.ToString();

		}

		private static int ConvertToDec(string val)
		{
			var r = 0;

			for (var i = 0; i < val.Length; i++) {
				r+= int.Parse(val[i].ToString()) * (int)Math.Pow(2, val.Length - i - 1);
			}

			return r;
		}

		public enum SystemType { 
			oct, 
			hex
		}

		private static string ConverTo(string number, SystemType system)
		{

			var allowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

			var length = system == SystemType.hex ? 4 : 3;

			var r = "";

			while (number.Any()) 
			{
				var start = number.Length - length;

				var part = number.Substring(start < 0 ? 0 : start);

				if (part.Length < length)
					part = string.Join("", new int[length - part.Length]) + part;

				r = allowedChars[ConvertToDec(part)] + r;
				number = number.Substring(0, start < 0 ? 0 : start);
			}

			return r;

		}
	}
}
