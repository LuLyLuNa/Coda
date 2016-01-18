using System;

using Xamarin.Forms;

namespace Coda
{
	public class App : Application
	{
		public static String user {
			get;
			set;
		}

		public static String dati {
			get;
			set;
		}

		public static String id {
			get;
			set;
		}

		public static String denominazione {
			get;
			set;
		}

		public static String idcg {
			get;
			set;
		}

		public static String cg {
			get;
			set;
		}

		public App ()
		{
			// The root page of your application
			MainPage = new NavigationPage(new LoginPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

