using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;




public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

   

    private void subscribeAddress()
    {
        string apiKey = "f7953504f9f308267a803e90edfeb23c-us15"; //your API KEY created by you.
        string dataCenter = "us15";
        string listID = "e712cdd7ad"; //The ListID of the list you want to use.

        SubscribeClassCreatedByMe subscribeRequest = new SubscribeClassCreatedByMe
        {
            email_address = "maliksaif777@yahoo.com",
            status = "subscribed"
        };
        subscribeRequest.merge_fields = new MergeFieldClassCreatedByMe();
        subscribeRequest.merge_fields.FNAME = "test7";
        subscribeRequest.merge_fields.LNAME = "test16";

        using (HttpClient client = new HttpClient())
        {
            var uri = "https://" + dataCenter + ".api.mailchimp.com/";
            var endpoint = "3.0/lists/" + listID + "/members";

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", apiKey);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(uri);

            //NOTE: To avoid the method being 'async' we access to '.Result'
            HttpResponseMessage response =  client.PostAsJsonAsync(endpoint, subscribeRequest).Result;//PostAsJsonAsync method serializes an object to 
            //JSON and then sends the JSON payload in a POST request
            //StatusCode == 200
            // -> Status == "subscribed" -> Is on the list now
            // -> Status == "unsubscribed" -> this address used to be on the list, but is not anymore
            // -> Status == "pending" -> This address requested to be added with double-opt-in but hasn't confirmed yet
            // -> Status == "cleaned" -> This address bounced and has been removed from the list

            //StatusCode == 404
            if ((response.IsSuccessStatusCode))
            {
                //Your code here
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        subscribeAddress();
    }

    private void test()
    {

    }
}