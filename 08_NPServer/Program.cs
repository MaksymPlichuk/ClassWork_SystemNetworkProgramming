    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    namespace _08_NPServer
    {
        internal class Program
        {
            //static string address = "127.0.0.1";
            //static int port = 8080;
            static void Main(string[] args)
            {

                UdpClient server = new(8080);
                Console.WriteLine("Server started on port 8080");

                Dictionary<string, string> responses = new()
                {
                    { "hi", "Hello" },
                    { "how are you", "Im great" },
                    { "bye", "Goodbye" }
                };

                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);

                while (true)
                {
                    byte[] data = server.Receive(ref remoteEP);
                    string message = Encoding.UTF8.GetString(data).Trim().ToLower();
                    Console.WriteLine($"Client [{remoteEP}]: {message}");

                    string reply = responses.ContainsKey(message) ? responses[message] : "I don't understand";
                    byte[] replyData = Encoding.UTF8.GetBytes(reply);
                    server.Send(replyData, replyData.Length, remoteEP);
                    Console.WriteLine($"Sent: {reply}");
                }
            }
        }
    }   
