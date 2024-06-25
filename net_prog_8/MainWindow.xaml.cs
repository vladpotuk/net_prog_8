using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace net_prog_7.Client
{
    public partial class MainWindow : Window
    {
        private const string ServerIP = "10.0.0.139";
        private const int ServerPort = 8888;
        private UdpClient udpClient;

        public MainWindow()
        {
            InitializeComponent();
            udpClient = new UdpClient();
        }

        private async void btnSendRequest_Click(object sender, RoutedEventArgs e)
        {
            string request = txtRequest.Text;
            byte[] requestData = Encoding.ASCII.GetBytes(request);

            try
            {
                await udpClient.SendAsync(requestData, requestData.Length, ServerIP, ServerPort);
                txtLog.AppendText($"Sent request to server: {request}\n");

                
                UdpReceiveResult result = await udpClient.ReceiveAsync();
                string response = Encoding.ASCII.GetString(result.Buffer);
                txtLog.AppendText($"Received response from server: {response}\n");
            }
            catch (Exception ex)
            {
                txtLog.AppendText($"Error: {ex.Message}\n");
            }
        }
    }
}
