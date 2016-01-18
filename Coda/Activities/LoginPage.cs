using Xamarin.Forms;
using System;

namespace Coda
{
	public class LoginPage : ContentPage
	{
		Entry username, password;
		public LoginPage ()
		{
			System.Diagnostics.Debug.WriteLine("COSTRUTTORE LOGIN PAGE");
			var sv = new DatoWebService();
			var accedi = new Button { Text = "Accedi" };
			accedi.Clicked += async (sender, e) => {
				if (await sv.CheckNetwork()){
					if (String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Text))
					{
						await DisplayAlert("Validation Error", "Username and Password are required", "Retry");
					}
					else {
						System.Diagnostics.Debug.WriteLine("ACCESSO OK");
						String result = await sv.LoginTestAsync(username.Text,password.Text);
						App.user = username.Text;
						App.dati = await sv.getRistorante(username.Text);
						App.id = await sv.idRistorante(App.user);
						HandleResult(result);
						System.Diagnostics.Debug.WriteLine("id: "+ App.id);
						//await Navigation.PushAsync(new Profilo());
					}
				} else {
					await DisplayAlert("Errore", "Verifica la connessione ad internet", "Ok");
				}
			};

			var create = new Button { Text = "Registrati" };
			create.Clicked += (sender, e) => {
				Navigation.PushModalAsync(new CreateAccount());
			};

			username = new Entry {
				//Text=globalUser,
				Keyboard = Keyboard.Text,
				Placeholder = "Type Username",
				WidthRequest = 200,
				HorizontalOptions = LayoutOptions.Start
			};

			password = new Entry {
				Keyboard = Keyboard.Text,
				Placeholder = "Type password",
				IsPassword = true,
			};


			Content = new StackLayout {
				Padding = new Thickness (10, 20, 10, 10),
				Children = {
					new BoxView { HorizontalOptions = LayoutOptions.FillAndExpand, },
					new Label { Text = "Username" },
					username,
					new Label { Text = "Password" },
					password,
					accedi, 
					create

				},
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 10
			};
		}

		void HandleResult(String result){
			//System.Diagnostics.Debug.WriteLine("RESULT: " + result);

			if (result.Equals("TRUE"))
			{
				Navigation.PushModalAsync(new NavigationDrawer());
				//DisplayAlert("LOGIN", "SUCCESS", "OK");
			}
			if (result.Equals("FALSE"))
			{
				DisplayAlert("LOGIN", "Incorrect username or password!", "OK");
			}
			if (result.Equals("NOTREACH"))
			{
				DisplayAlert("LOGIN", "SERVER NOT REACHEABLE", "OK");
			}
		}
	}
}

