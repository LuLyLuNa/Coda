using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Plugin.Connectivity;
using Newtonsoft.Json;

namespace Coda
{
	public class DatoWebService
	{
		public DatoWebService ()
		{
		}

		public async Task<bool> CheckNetwork() {
			bool reach;
			try
			{
				reach = CrossConnectivity.Current.IsConnected;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			return reach;
		}

		public async Task<String> LoginTestAsync (String username , String password) {

			var client = new System.Net.Http.HttpClient ();

			Uri Indirizzo = client.BaseAddress = new Uri("http://matrimonirc.altervista.org/loginRistorante.php");


			HttpContent content = new FormUrlEncodedContent(
				new List<KeyValuePair<string, string>> { 
					new KeyValuePair<string, string>("username", username),
					new KeyValuePair<string, string>("password", password),

				});
			content.Headers.ContentType.CharSet = "UTF-8";
			HttpResponseMessage response = await client.PostAsync(Indirizzo, content);
			string resultContent = response.Content.ReadAsStringAsync().Result;
			return resultContent;
		}

		public async Task<String> Create (String denominazione, String via, String telefono, String email , String username, String password) {

			var client = new System.Net.Http.HttpClient ();

			Uri Indirizzo = client.BaseAddress = new Uri("http://matrimonirc.altervista.org/ristoranteNew.php");

			HttpContent content = new FormUrlEncodedContent(
				new List<KeyValuePair<string, string>> { 
					new KeyValuePair<string, string>("denominazione", denominazione),
					new KeyValuePair<string, string>("via", via),
					new KeyValuePair<string, string>("telefono", telefono),
					new KeyValuePair<string, string>("email", email),
					new KeyValuePair<string, string>("username", username),
					new KeyValuePair<string, string>("password", password),
				});
			content.Headers.ContentType.CharSet = "UTF-8";
			HttpResponseMessage response = await client.PostAsync(Indirizzo, content);
			string resultContent = response.Content.ReadAsStringAsync().Result;
			if(resultContent.Equals("-1"))
			{
				System.Diagnostics.Debug.WriteLine("ERROR -1");
			}
			else
			{
				System.Diagnostics.Debug.WriteLine(resultContent);
			}

			return resultContent;

		}

		public async Task<String> getRistorante(String user){

			var client = new System.Net.Http.HttpClient();

			Uri Indirizzo = client.BaseAddress = new Uri("http://matrimonirc.altervista.org/dettagliRistorante.php");
			//HttpContent content = new StringContent (denominazione);

			HttpContent content = new FormUrlEncodedContent(
				new List<KeyValuePair<string, string>> { 
					new KeyValuePair<string, string>("username", user),
				});
			content.Headers.ContentType.CharSet = "UTF-8";

			HttpResponseMessage response = await client.PostAsync(Indirizzo, content);

			var resultContent = response.Content.ReadAsStringAsync().Result;

			//System.Diagnostics.Debug.WriteLine("RISULTATO: "+resultContent);

			return resultContent;
		}

		public async Task<String> setCodiceGiornaliero (String codice, String data, String idristorante) {
			System.Diagnostics.Debug.WriteLine("metodo: "+data);
			var client = new System.Net.Http.HttpClient ();

			Uri Indirizzo = client.BaseAddress = new Uri("http://matrimonirc.altervista.org/setCodiceGiornaliero.php");


			HttpContent content = new FormUrlEncodedContent(
				new List<KeyValuePair<string, string>> { 
					new KeyValuePair<string, string>("codice", codice),
					new KeyValuePair<string, string>("data", data),
					new KeyValuePair<string, string>("idristorante", idristorante),
				});
			content.Headers.ContentType.CharSet = "UTF-8";
			HttpResponseMessage response = await client.PostAsync(Indirizzo, content);
			string resultContent = response.Content.ReadAsStringAsync().Result;
			return resultContent;
		}


		public async Task<String> idRistorante(String username){
			var client = new System.Net.Http.HttpClient();

			Uri Indirizzo = client.BaseAddress = new Uri("http://matrimonirc.altervista.org/getRistoranteID.php");
			//HttpContent content = new StringContent (denominazione);

			HttpContent content = new FormUrlEncodedContent(
				new List<KeyValuePair<string, string>> { 
					new KeyValuePair<string, string>("username", username),
				});
			content.Headers.ContentType.CharSet = "UTF-8";

			HttpResponseMessage response = await client.PostAsync(Indirizzo, content);

			var resultContent = response.Content.ReadAsStringAsync().Result;

			return resultContent;
		}

		public async Task<String> dailyCheck(String idristorante, String data){
			var client = new System.Net.Http.HttpClient();

			Uri Indirizzo = client.BaseAddress = new Uri("http://matrimonirc.altervista.org/dailyCheck.php");
			//HttpContent content = new StringContent (denominazione);

			HttpContent content = new FormUrlEncodedContent(
				new List<KeyValuePair<string, string>> { 
					new KeyValuePair<string, string>("idristorante", idristorante),
					new KeyValuePair<string, string>("data", data),
				});
			content.Headers.ContentType.CharSet = "UTF-8";

			HttpResponseMessage response = await client.PostAsync(Indirizzo, content);

			var resultContent = response.Content.ReadAsStringAsync().Result;

			return resultContent;
		}
			

		public async Task<List<Prenotazioni>> elencoPrenotazioni(String idristorante, String data) {

			var client = new System.Net.Http.HttpClient();

			Uri Indirizzo = client.BaseAddress = new Uri("http://matrimonirc.altervista.org/elencoPrenotazioni.php");

			HttpContent content = new FormUrlEncodedContent(
				new List<KeyValuePair<string, string>> { 
					new KeyValuePair<string, string>("idristorante", idristorante),
					new KeyValuePair<string, string>("data", data),
				});
			content.Headers.ContentType.CharSet = "UTF-8";

			HttpResponseMessage response = await client.PostAsync(Indirizzo, content);

			var result = response.Content.ReadAsStringAsync().Result;

			List<Prenotazioni> ristoranti = JsonConvert.DeserializeObject<List<Prenotazioni>>(result);	
			return ristoranti;
		}
	}
}

