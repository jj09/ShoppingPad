using ShoppingPad.Common.Helpers;
using SQLite;
using System;
using System.IO;
using UIKit;

namespace ShoppingPad.iOS
{
	public class Application
	{
		public static SQLiteConnection SqliteConnection;

		// This is the main entry point of the application.
		static void Main (string[] args)
		{
			// init db
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder instead
			var path = Path.Combine(libraryPath, ServiceRegistrar.DbFileName);
			SqliteConnection = new SQLiteConnection(path);

			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
