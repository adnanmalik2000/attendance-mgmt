<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">

        function save() {
            var request = require('request'),
           url = require('url'),
           crypto = require('crypto');

            // variables
            var datacenter = "us15", // something like 'us11' (after '-' in api key)
                listId = "e712cdd7ad",
                email = "maliksaif77@yahoo.com",
                apiKey = "f7953504f9f308267a803e90edfeb23c-us15";

            // mailchimp options
            var options = {
                url: url.parse('https://' + datacenter + '.api.mailchimp.com/3.0/lists/' + listId + '/members/' + crypto.createHash('md5').update(email).digest('hex')),
                headers: {
                    'Authorization': 'authId ' + apiKey // any string works for auth id
                },
                json: true,
                body: {
                    email_address: email,
                    status_if_new: 'pending', // pending if new subscriber -> sends 'confirm your subscription' email
                    status: 'subscribed',
                    merge_fields: {
                        FNAME: "subscriberFirstName",
                        LNAME: "subscriberLastName"
                    },
                    interests: {
                        MailChimpListGroupId: true // if you're using groups within your list
                    }
                }
            };

            // perform update
            request.put(options, function (err, response, body) {
                if (err) {
                    // handle error
                } else {
                    console.log('subscriber added to mailchimp list');
                }
            });
        }

        function save1() 
        {

        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="button" onclick="save();" title="hghjg"/>
        <asp:Button ID="Button1" OnClick="Button1_Click" runat="server" Text="Button" />
    </div>
    </form>
</body>
</html>
