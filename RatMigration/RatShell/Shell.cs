﻿using System.Collections.Generic;
using System;

namespace RatMigration.RatShell
{
	public static class Shell
	{
		#region Methods

		public static ReturnCode TryInvoke(string Name, string[] Args, out string Return)
		{
			if (Commands.Count == 0)
			{
				Return = "Command not found!";
				return ReturnCode.CommandNotFound;
			}

			for (int I = 0; I < Commands.Count; I++)
			{
				if (Commands[I].Name == Name)
				{
					Return = Commands[I].Invoke(Args);
					return ReturnCode.Success;
				}
			}
			Return = string.Empty;
			return ReturnCode.CommandNotFound;
		}
		public static string Invoke(string Name, string[] Args)
		{
			TryInvoke(Name, Args, out string S);
			return S;
		}
		public static string Invoke(string[] Args)
		{
			string Name = Args[0];
			string[] NewArgs = new string[Args.Length - 1];
			for (int I = 0; I < NewArgs.Length; I++)
			{
				NewArgs[I] = Args[I + 1];
			}

			TryInvoke(Name, NewArgs, out string Return);
			return Return;
		}
		public static void Initialize()
		{
			// Create all command objects.
			_ = new Commands.Clear();

			// Log for debugging
			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write("[ OK ] ");
			Console.ResetColor();
			Console.WriteLine("Initialized RatShell successfully.");
		}

		#endregion

		#region Fields

		internal static List<Command> Commands { get; set; } = new();

		#endregion
	}
}