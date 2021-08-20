using Newtonsoft.Json;
using SimpleHttp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var cancel = new CancellationTokenSource();

            Route.Add("/{Query}", async (req, res, props) =>
            {
                res.AsText("You can safely close the page.");
                cancel.Cancel();

                var payload = new Dictionary<string, string>
                {
                    { "ClientID", "react" },
                    { "ClientSecret", "my secret" },
                    { "AuthCode", req.QueryString.Get("auth_code")}
                };

                var jsonContent = JsonConvert.SerializeObject(payload);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();
                var response = await client.PostAsync("http://127.0.0.1:5001/get_access_token", httpContent);

                Dictionary<string, string> tokens = JsonConvert.DeserializeObject<Dictionary<string, string>>(await response.Content.ReadAsStringAsync());

                MessageBox.Show(tokens["access_token"]);
            });

            HttpServer.ListenAsync(
                1337,
                cancel.Token,
                Route.OnHttpRequestAsync
                );

            System.Diagnostics.Process.Start("http://127.0.0.1:5001/login?callback=http://localhost:1337");
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }
    }
}
