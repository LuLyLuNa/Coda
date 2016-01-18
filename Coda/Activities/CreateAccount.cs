using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Coda
{
	public class CreateAccount : ContentPage
	{
		Entry username, password, confirmPassword, denominazione, via, email, telefono;

		public CreateAccount ()
		{
			var create = new Button { Text = "Create Account" };
			create.Clicked += async (sender, e) => {

				if (String.IsNullOrEmpty(username.Text) || String.IsNullOrEmpty(password.Text)|| String.IsNullOrEmpty(email.Text) || String.IsNullOrEmpty(telefono.Text) || String.IsNullOrEmpty(via.Text) || String.IsNullOrEmpty(denominazione.Text))
				{
					await DisplayAlert("Errore", "Compila tutti i campi correttamente", "Re-try");
				}
				else if (password.Text != confirmPassword.Text) {
					await DisplayAlert("Errore", "Le password inserite non coincidono", "Re-try");
				}
				else if (Regex.Match(email.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success) {
					var sv = new DatoWebService();
					String result = await sv.Create(denominazione.Text, via.Text, telefono.Text, email.Text, username.Text, password.Text);
					HandleResult(result);
				} else {
					await DisplayAlert("Errore", "Indirizzo Email Non Valido", "Re-try");
				}

			};

			var cancel = new Button { Text = "Cancel" };
			cancel.Clicked += (sender, e) => {
				App.Current.MainPage = new NavigationPage(new LoginPage());
			};
			username = new Entry {
				Keyboard = Keyboard.Text,
				Placeholder = "Enter Username",
			};

			denominazione = new Entry {
				Placeholder = "Inserisci il nome del locale"
			};

			via = new Entry {
				Placeholder = "Indirizzo"
			};

			password = new Entry {
				Keyboard = Keyboard.Text,
				Placeholder = "Enter password",
				IsPassword = true,
			};

			confirmPassword = new Entry {
				Keyboard = Keyboard.Text,
				Placeholder = "Confirm password",
				IsPassword = true,
			};

			email = new Entry {
				Keyboard = Keyboard.Email,
				Placeholder = "Enter your e-mail",
			};

			telefono = new Entry {
				Keyboard = Keyboard.Telephone,
				Placeholder = "Enter Number",
			};

			Content = new StackLayout {
				Padding = new Thickness (10, 40, 10, 10),
				Children = {
					new Label { 
						Text = "Compila i campi richiesti per la registrazione",
						FontSize = 20,
						TextColor = Color.Blue,
						//XAlign = TextAlignment.Center
					},
					new Label { Text = "Username" },
					username,
					new Label { Text = "Denominazione ristorante/pizzeria" },
					denominazione,
					new Label { Text = "Via" },
					via,
					new Label { Text = "Password" },
					password,
					new Label { Text = "Confirm Password" },
					confirmPassword,
					new Label { Text = "Email" },
					email,
					new Label { Text = "Telefono" },
					telefono,
					create, cancel
				}
			};
		}

		void HandleResult(String result){
			System.Diagnostics.Debug.WriteLine("SIGNUP RESULT: " + result);

			if (result.Equals("FALSE"))
			{
				DisplayAlert("Error", "Server NOT REACHEABLE", "Re-try later");
			}
			if (result.Equals ("RISTEXIST")) 
			{
				DisplayAlert("Error", "Ristorante già presente", "Re-try later");
			}
			if (result.Equals ("EMAILEXIST")) 
			{
				DisplayAlert ("Error", "Email già presente!", "Re-try later");
			}
			if (result.Equals("TRUE")){
				DisplayAlert("Account", "Account creato con successo!", "Ok");
				Navigation.PushModalAsync(new LoginPage());
			}
		}
	}
}

