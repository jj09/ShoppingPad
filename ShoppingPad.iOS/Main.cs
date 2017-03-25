using Mindscape.Raygun4Net;
using ShoppingPad.Common.Helpers;
using SQLite;
using System;
using System.IO;
using UIKit;

namespace ShoppingPad.iOS
{
	public class Application
	{

		// This is the main entry point of the application.
		static void Main (string[] args)
		{
            RaygunClient.Initialize("RAYGUN_API_KEY").AttachPulse();  // Raygun Pulse
            RaygunClient.Attach("RAYGUN_API_KEY");    // Raygun Crash Reporting

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main (args, null, "AppDelegate");
		}
	}
}
