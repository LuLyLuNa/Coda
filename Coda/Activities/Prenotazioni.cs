using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using XLabs.Forms.Controls;

namespace Coda
{
	public class Prenotazioni : ContentPage
	{
		public string username;
		public string ncoperti;
		public string pizzeria;
		public string ristorante;

		public Prenotazioni ()
		{
			
			this.Load ();
		}

		private async void Load()
		{
			DateTime now = new DateTime ();
			DatoWebService service = new DatoWebService();

			List<Prenotazioni> myArr = new List<Prenotazioni> ();
			myArr = await service.elencoPrenotazioni (App.id, string.Format ("{0:dd-MM-yyyy}", now));

			System.Diagnostics.Debug.WriteLine(myArr[0].username);
		

			StackLayout stack = new StackLayout {
				HorizontalOptions = LayoutOptions.Center,
				Spacing = 20,
				Children = {
				}
			};
			Content = new ScrollView { Content = stack };
		}



		void HandleResult(String result){
			//System.Diagnostics.Debug.WriteLine("RESULT: " + result);
			if (result.Equals("TRUE"))
			{
				DisplayAlert("AVVISO", "Prenotazione eseguita con successo!", "OK");
				//DisplayAlert("LOGIN", "SUCCESS", "OK");
			}
			if (result.Equals("FALSE"))
			{
				DisplayAlert("ERROR", "SERVER NOT REACHEABLE", "OK");
			}
		}
	}
}

