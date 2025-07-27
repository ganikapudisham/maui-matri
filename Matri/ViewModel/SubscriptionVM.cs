using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;

namespace Matri.ViewModel;

public partial class SubscriptionVM : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
{
    public SubscriptionVM()
    {
    }

    [RelayCommand]
    public async Task Pay(string planType)
    {
        string amount;
        string subscriptionPlan;

        var split = planType.Split('_');
        amount = split[1];
        subscriptionPlan = split[0];

        // Fetch order_id from backend
        using var http = new HttpClient();

        var request = new
        {
            plan = subscriptionPlan,
            amount = Convert.ToInt32(amount), // in paise
            currency = "INR",
            receipt = $"CJ-{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(1000, 9999)}"
        };

        var json = JsonConvert.SerializeObject(request);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await http.PostAsync("https://api.christianjodi.com/payments/create-order", content);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            // Log the response body
            Console.WriteLine("Razorpay Response: " + responseBody);

            var order = JsonConvert.DeserializeObject<OrderResponse>(responseBody);

            if (!string.IsNullOrWhiteSpace(order?.id))
            {
                var checkoutUrl = $"https://api.christianjodi.com/checkout.html?order_id={order.id}&amount={order.amount}&key={order.key}&currency={order.currency}&name={order.name}&description={order.description}&plan={order.plan}";
                await Launcher.Default.OpenAsync(checkoutUrl);
            }
            else
            {
                Console.WriteLine("Error: Order ID is missing in response.");
            }
        }
        else
        {
            // Log the error
            Console.WriteLine("Razorpay Error: " + responseBody);
        }
    }

    public class OrderResponse
    {
        public string id { get; set; }             // Razorpay order ID
        public int amount { get; set; }            // in paise
        public string currency { get; set; }
        public string key { get; set; }            // Razorpay public key
        public string name { get; set; }           // Merchant/app name
        public string description { get; set; }
        public string plan { get; set; }
    }
}
