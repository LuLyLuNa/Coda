using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Coda
{
	public class Profilo : ContentPage
	{
		public Profilo ()

		{

			this.Personale ();	
		}

		public async void Personale(){

			DatoWebService sv = new DatoWebService ();
			Image avatar = new Image
			{
				Source = "Avatar.png",
				Aspect = Aspect.AspectFit,
				HorizontalOptions = LayoutOptions.Center,
				//VerticalOptions = LayoutOptions.Fill
			};

			Label info = new Label
			{
				Text = App.dati,
				FontSize = Device.GetNamedSize (NamedSize.Large, typeof(Label)),
				HorizontalOptions = LayoutOptions.Center,
				XAlign = TextAlignment.Center
			};

			DateTime now = DateTime.Now.ToLocalTime ();


			Label data = new Label {
				Text = string.Format ("{0:dd/MM/yyyy}", now),
				HorizontalOptions = LayoutOptions.Center,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
			};

			Button genera = new Button {
				Text = "GENERA CODICE GIORNALIERO",
				WidthRequest = 50,
				HeightRequest = 50
			};

			Button logout = new Button {
				Text = "LOGOUT",
				WidthRequest = 50,
				HeightRequest = 50
			};

			Label cod = new Label {
				XAlign = TextAlignment.Center
			};

			String[] splitstring;
			String r = await sv.dailyCheck(App.id, string.Format ("{0:dd-MM-yyyy}", now));

			if (r.Equals("NOTREACH")){
				await DisplayAlert ("Errore", "Errore nel server", "OK");
			} else if (r.Equals ("FALSE")) {
				cod.Text = "Genera il tuo codice giornaliero";
				genera.IsEnabled = true;
			} else {
				splitstring = r.Split('\n');
				App.idcg = splitstring [0];
				App.cg = splitstring [1];
				cod.Text = App.cg;
				genera.IsEnabled = false;
			}

			genera.Clicked += async (object sender, EventArgs e) => {
				String s = randomString ();
				await DisplayAlert ("CODICE GIORNALIERO", "Il codice giornaliero è: " + s, "OK");
				//String result = await sv.setCodiceGiornaliero(s, string.Format ("{0:yyyy-M-d}", now));
				r = await sv.setCodiceGiornaliero (s, string.Format ("{0:dd-MM-yyyy}", now), App.id);

				if (r.Equals("FALSE")){
					await DisplayAlert ("Errore", "Errore nel server", "OK");
				} else {
					App.id = r;
					App.cg = s;
					cod.Text = App.cg;
					genera.IsEnabled = false;
				}

			};



			logout.Clicked += (object sender, EventArgs e) => {
				App.Current.MainPage = new NavigationPage(new LoginPage());
				//Navigation.PushAsync(new LoginPage());
			};

			StackLayout stack = new StackLayout {
				Children = {
					avatar,
					new BoxView { HorizontalOptions = LayoutOptions.FillAndExpand },
					info,
					data,
					new BoxView { HorizontalOptions = LayoutOptions.FillAndExpand },
					cod,
					genera,
					new BoxView { HorizontalOptions = LayoutOptions.FillAndExpand },
					logout
				},
			};
			Content = new ScrollView { Content = stack};
		}

		public string randomString(){
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var stringChars = new char[5];
			var random = new Random();

			for (int i = 0; i < stringChars.Length; i++)
			{
				stringChars[i] = chars[random.Next(chars.Length)];
			}

			var finalString = new String(stringChars);
			return finalString;
		}
	}
}

