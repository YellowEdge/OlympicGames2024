using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OgWeb.Models;
using OgWeb.Repositories;
using System.Text;
using System.Text.Json.Nodes;

namespace OgWeb.Pages.MyCart;

[Authorize(Policy = "TwoFactorEnabled")]
[Authorize(Roles = "admin,client")]
//[IgnoreAntiforgeryToken]
public class MyCheckoutModel : PageModel
{
	private readonly ICartRepository _cartRepo;

	public string PaypalClientId { get; set; } = "";
    private string PaypalSecret { get; set; } = "";
    public string PaypalUrl { get; set; } = "";
	public string FullName { get; set; }
    public string Total { get; set; }
    public MyCheckoutModel(IConfiguration configuration, ICartRepository cartRepo)
    {
        PaypalClientId = configuration["PaypalSettings:ClientId"]!;
        PaypalSecret = configuration["PaypalSettings:Secret"]!;
        PaypalUrl = configuration["PaypalSettings:Url"]!;
		_cartRepo = cartRepo;
	}
    public void OnGet(string name, string total)
    { 
        FullName = TempData["Name"]?.ToString() ?? "";
        Total = TempData["Total"]?.ToString() ?? "";
        ViewData["heading"] = "Welcome to PayPal !";
        TempData.Keep();
    }

    public JsonResult OnPostCreateOrder()
    {
        Total = TempData["Total"]?.ToString() ?? "";

        if (Total == "")
        {
            Total = "0";
            //return new JsonResult("");
        }

        // create the request body
        JsonObject createOrderRequest = new JsonObject();
        createOrderRequest.Add("intent", "CAPTURE");
        JsonObject amount = new JsonObject();
        amount.Add("currency_code","USD");
        amount.Add("value", Total);

        JsonObject purchaseUnit1 = new JsonObject();
        purchaseUnit1.Add("amount", amount);

        JsonArray purchaseUnits = new JsonArray();
        purchaseUnits.Add(purchaseUnit1);

        createOrderRequest.Add("purchase_units", purchaseUnits);

		// get access token
		string accessToken = GetPaypalAccessToken();

        // send request
        string url = PaypalUrl + "/v2/checkout/orders";
        string orderId = "";

        using(var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(createOrderRequest.ToString(),null,"application/json");
            var responseTask = client.SendAsync(requestMessage);
            responseTask.Wait();

			var result = responseTask.Result;

			if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

				var strResponse = readTask.Result;
                var jsonResponse = JsonNode.Parse(strResponse);
                if(jsonResponse != null)
                {
                    orderId = jsonResponse["id"]?.ToString() ?? "";
                    // save the order to the database
                }
            }            
        }

        var response = new
        {
            Id = orderId
		};
        return new JsonResult(response);
    
    }

	//added async task<jsonresult> à la place de JsonResult ici:
	public async Task<JsonResult> OnPostCompleteOrder([FromBody] JsonObject data)
    {
        if (data == null || data["orderID"] == null) return new JsonResult("");

        var orderID = data["orderID"]!.ToString();

		// get access token
		string accessToken = GetPaypalAccessToken();
		string url = PaypalUrl + "/v2/checkout/orders/" + orderID + "/capture";

		using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
			var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
			requestMessage.Content = new StringContent("", null, "application/json");

			var responseTask = client.SendAsync(requestMessage);
			responseTask.Wait();

            var result = responseTask.Result;
			if (result.IsSuccessStatusCode)
            {
				var readTask = result.Content.ReadAsStringAsync();
				readTask.Wait();

				var strResponse = readTask.Result;
				var jsonResponse = JsonNode.Parse(strResponse);
				if (jsonResponse != null)
				{
					string paypalOrderStatus = jsonResponse["status"]?.ToString() ?? "";
                    if(paypalOrderStatus == "COMPLETED")
                    {
                        
						// update payment status in database => "accepted"
						bool ispaid = true; string status = "Delivered";
                        // clear cookie

                        CheckoutModel model = new CheckoutModel();
                        model.Address = TempData["Address"].ToString();
						model.Name = TempData["Name"].ToString();
						model.Email = TempData["Email"].ToString();
						model.MobileNumber = TempData["MobileNumber"].ToString();
						model.PaymentMethod = TempData["PaymentMethod"].ToString();

						var isCheckedOut = await _cartRepo.DoCheckout(model, ispaid, status);
						// clear TempData
						TempData.Clear();

						return new JsonResult("success");
                    }
				}
			}
		}
		return new JsonResult("");
	}

    public JsonResult OnPostCancelOrder([FromBody] JsonObject data)
    {
		if (data == null || data["orderID"] == null) return new JsonResult("");

		var orderID = data["orderID"]!.ToString();

        //Update payment status in the database => "canceled"

		return new JsonResult("");
	}

    private string GetPaypalAccessToken()
    {
        string accesstoken = "";

        string url = PaypalUrl + "/v1/oauth2/token";

        using (HttpClient client = new HttpClient())
        {
            string credentials64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(PaypalClientId + ":" + PaypalSecret));
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials64);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent("grant_type=client_credentials", null, "application/x-www-form-urlencoded");

            var responseTask = client.SendAsync(requestMessage);
            responseTask.Wait();

            var result = responseTask.Result;
            if(result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                var strResponse = readTask.Result;
                var jsonResponse = JsonNode.Parse(strResponse);
                if(jsonResponse != null)
                {
                    accesstoken = jsonResponse["access_token"]?.ToString() ?? "";
                }
            }
        }
        return accesstoken;
    }
}
