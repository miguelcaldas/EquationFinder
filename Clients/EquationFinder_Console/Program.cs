﻿/*
 *
 * Developed by Adam Rakaska
 *  http://www.csharpprogramming.tips
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationFinder_Console
{
	sealed class Program
	{
		private static void Main(string[] args)
		{
			MainRoutine mRoutine = new MainRoutine(args.ToList());
			mRoutine.Find();			
		}
	}
}
