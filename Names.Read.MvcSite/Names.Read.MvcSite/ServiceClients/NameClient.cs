using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Names.Read.SoapService.Contracts;

namespace Names.Read.MvcSite.ServiceClients
{
	public class NameClient
	{
		public NameClient()
		{
		}

		public async Task<bool> TestServiceConnection()
		{
			return await GetRequest<bool>("test/service-connection");
		}

		public async Task<string> TestDataStoreConnection()
		{
			return await GetRequest<string>("test/database-connection");
		}

		public async Task<ICollection<NameResponse>> GetDetailedNames(string origin, string gender)
		{
			GenderOption genderOption = GenderOption.Any;
			switch(gender)
			{
				case "OnlyBoys": genderOption = GenderOption.OnlyBoys; break;
				case "OnlyGirls": genderOption = GenderOption.OnlyGirls; break;
			}
			return await GetDetailedNames(origin, genderOption);
		}

		public async Task<ICollection<NameResponse>> GetDetailedNames(string origin, GenderOption gender)
		{
			return await GetRequest<ICollection<NameResponse>>("names", "origin="+origin+"&gender="+gender);
		}

		public async Task<ICollection<CategoryResponse>> GetCategories()
		{
			return await GetRequest<ICollection<CategoryResponse>>("categories");
		}

		private async Task<TModel> GetRequest<TModel>(string partialUrl, string queryParams = null)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:5000/api/");
				if (queryParams != null)
					partialUrl += "?" + queryParams;
				var result = await client.GetAsync(partialUrl);
				if (result.IsSuccessStatusCode)
				{
					return await ReadAsAsync<TModel>(result.Content);
				}
				else //web api sent error response 
				{
					//log response status here..
					return default;
				}
			}
		}

		private async Task<TModel> PostRequest<TRequest, TModel>(string partialUrl, TRequest request)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:5000/api/");
				var result = await client.PostAsJsonAsync<TRequest>(partialUrl, request);
				if (result.IsSuccessStatusCode)
				{
					return await ReadAsAsync<TModel>(result.Content);
				}
				else //web api sent error response 
				{
					//log response status here..
					return default;
				}
			}
		}

		public static async Task<T> ReadAsAsync<T>(HttpContent content)
		{
			string s = await content.ReadAsStringAsync();
			JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
			return await System.Text.Json.JsonSerializer.DeserializeAsync<T>(await content.ReadAsStreamAsync(), options);
		}
	}

	public class JsonContent : StringContent
	{
		public JsonContent(object obj) :
			base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
		{ }
	}
}