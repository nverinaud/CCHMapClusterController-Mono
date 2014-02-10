//
//  Author:
//       Nicolas VERINAUD <nicolas@chloro.eu>
//
//  Copyright (c) 2014 Chlorophyll Vision. All Rights Reserved.
//
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MapClusterSample
{
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			window = new UIWindow(UIScreen.MainScreen.Bounds);
			window.RootViewController = new MapViewController();
			window.MakeKeyAndVisible();
			
			return true;
		}
	}
}

