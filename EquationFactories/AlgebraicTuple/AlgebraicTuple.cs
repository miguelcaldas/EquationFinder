/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EquationFinderCore;
using System.Globalization;

namespace EquationFactories
{
	public partial class AlgebraicTuple : IEquation
	{
		public decimal Result
		{
			get
			{
				if (_result == null) { _result = Solve(); }
				return (decimal)_result;
			}
		}
		private decimal? _result = null;

		private IEquationFinderArgs EquationArgs { get; set; }
		private List<Tuple<decimal, TupleOperation>> Equation { get; set; }
		private List<int> TermPool { get { return EquationArgs.TermPool; } }
		private string OperatorPool { get { return EquationArgs.OperatorPool; } }
		private decimal TargetValue { get { return EquationArgs.TargetValue; } }
		private int NumberOfOperations { get { return EquationArgs.NumberOfOperations; } }

		public AlgebraicTuple()
		{
		}

		public AlgebraicTuple(EquationFinderArgs equationArgs)
		{
			GenerateNewAndEvaluate(equationArgs);
		}

		public void GenerateNewAndEvaluate(IEquationFinderArgs args)
		{			
			_result = null;
			EquationArgs = args;
			Equation = GenerateRandomEquation();
			Solve();
		}

		public bool IsSolution
		{
			get { return (Solve() == TargetValue); }
		}

		private List<Tuple<decimal, TupleOperation>> GenerateRandomEquation()
		{
			List<Tuple<decimal, TupleOperation>> result = new List<Tuple<decimal, TupleOperation>>();

			int counter = 1;
			decimal term = 0;
			int termCount = TermPool.Count;
			int opCount = OperatorPool.Length;
			TupleOperation operation = new TupleOperation();
			OperationType lastOperand = OperationType.None;
			while (counter <= NumberOfOperations)
			{
				do
				{
					term = Convert.ToDecimal(TermPool[EquationArgs.Rand.Next(0, termCount)]);
				}
				while (lastOperand == OperationType.Divide && term == 0);

				if (counter == NumberOfOperations)
				{
					operation = new TupleOperation(OperationType.Equal);
				}
				else
				{
					operation = new TupleOperation(OperatorPool.ElementAt(EquationArgs.Rand.Next(0, opCount)).ToString());
				}

				result.Add(new Tuple<decimal, TupleOperation>(term, operation));
				lastOperand = operation.Operand;
				counter++;
			}

			return result;
		}

		private decimal Solve()
		{
			if (_result == null)
			{
				TupleOperation lastOperation = new TupleOperation(OperationType.None);
				decimal runningTotal = 0;
				foreach (Tuple<decimal, TupleOperation> t in Equation)
				{
					if (lastOperation.Operand == OperationType.None)
					{
						runningTotal = (decimal)t.Item1;
					}
					else
					{
						runningTotal = lastOperation.Calculate((decimal)runningTotal, (decimal)t.Item1);
					}
					lastOperation = t.Item2;
				}
				_result = runningTotal;
			}
		
			return (decimal)_result;			
		}

		public override string ToString()
		{
			StringBuilder resultText = new StringBuilder();
			foreach (Tuple<decimal, TupleOperation> exp in Equation)
			{
				resultText.AppendFormat(CultureInfo.CurrentCulture, "{0:0.##} {1} ", exp.Item1, exp.Item2);
			}
			resultText.AppendFormat(CultureInfo.CurrentCulture, "{0:0.##}", Result);
			return resultText.ToString();
		}
	}
}
