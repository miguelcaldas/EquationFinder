﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationFinder_Console
{
	private sealed class Program
	{
		private static void Main(string[] args)
		{
			MainRoutine mRoutine = new MainRoutine(args.ToList());
			mRoutine.Find();			
		}
	}
}
