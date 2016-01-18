using System;
using Xamarin.Forms;

namespace Coda
{
	public class NavigationDrawer : MasterDetailPage
	{
		public NavigationDrawer ()
		{
			string[] pagine = {"", "Profilo", "Prenotazioni" };

			ListView listView = new ListView
			{
				ItemsSource = pagine,
			};
			this.Master = new ContentPage
			{
				Title = "Options",
				Content = listView,
				Icon = "menu.jpg"
			};

			listView.ItemTapped += (sender, e) =>
			{
				ContentPage gotoPage = null;
				switch (e.Item.ToString())
				{
				case "":
					break;
				case "Profilo":
					gotoPage = new Profilo();
					break;
				case "Prenotazioni":
					gotoPage = new Prenotazioni();
					break;
				default:
					break;
				}

				Detail = new NavigationPage(gotoPage);
				((ListView)sender).SelectedItem = null; 
				this.IsPresented = false;
			};

			Detail = new NavigationPage(new Profilo());
		}
	}
}

