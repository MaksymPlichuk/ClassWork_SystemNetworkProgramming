using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _09_NPServer
{
    internal class Program
    {
        class Server
        {
            UdpClient server;
            IPEndPoint clientEndPoint = null;
            const string JOIN_CMD = "$<JOIN>";
            const string DISC_CMD = "$<DISC>";
            List<IPEndPoint> members;
            public Server()
            {
                members = new List<IPEndPoint>();
                server = new(4040);
                Console.WriteLine("Server started on port: 4040");
            }

            private void AddClient(IPEndPoint member)
            {
                if (!members.Contains(member))
                {
                    members.Add(member);
                    Console.WriteLine($"Member [{member}] Added!");
                }
                else { Console.WriteLine($"Member [{member}] is already in chat!"); }
                   
            }
            private void RemoveClient(IPEndPoint member)
            {
                if (!members.Contains(member)) { Console.WriteLine($"Member [{member}] is not in the chat!"); }
                else { members.Remove(member); Console.WriteLine($"Member [{member}] deleted from the chat!"); }

            }
            public void Start() {
                while (true)
                {
                    byte[] data = server.Receive(ref clientEndPoint);
                    string msg = Encoding.UTF8.GetString(data);
                    switch (msg)
                    {
                        case JOIN_CMD:
                            AddClient(clientEndPoint);
                            break;

                        case DISC_CMD:
                            RemoveClient(clientEndPoint);
                            break;

                        default:
                            if (members.Contains(clientEndPoint)) { Console.WriteLine($"member: [{clientEndPoint}]  Message: [{msg}]"); }
                            else { Console.WriteLine($"client: [{clientEndPoint}]  Message: [{msg}]"); }

                            SendAll(data);
                            break;
                    }
                }
            }
            private void SendAll(byte[] data)
            {
                foreach (var member in members)
                {
                    server.SendAsync(data, data.Length, member);
                }
            }
        }
        static void Main(string[] args)
        {
            Server s = new Server();
            s.Start();
        }
    }
}
