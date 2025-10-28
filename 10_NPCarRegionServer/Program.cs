using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace _10_NPCarRegionServer
{
    internal class Program
    {
        class Server
        {

            string ip = "127.0.0.1";
            int port = 4040;

            TcpListener server_listener;

            Dictionary<string,string>carRegions = new Dictionary<string,string>();
            public Server()
            {
                server_listener = new TcpListener(new IPEndPoint(IPAddress.Parse(ip), port));
                Console.WriteLine("Server started on port: 4040");
                carRegions = new()
                {
                    {"Автономна Республіка Крим", "АК, КК"},
                    {"Вінницька", "АВ, КВ"},
                    {"Волинська", "АС, КС"},
                    {"Дніпропетровська", "АЕ, КЕ"},
                    {"Донецька", "АН, КН"},
                    {"Житомирська", "АМ, КМ"},
                    {"Закарпатська", "АО, КО"},
                    {"Запорізька", "АР, КР"},
                    {"Івано-Франківська", "АТ, КТ"},
                    {"Київ", "АА, КА"},
                    {"Київська", "АІ, КІ"},
                    {"Кіровоградська", "ВА, НА"},
                    {"Луганська", "ВВ, НВ"},
                    {"Львівська", "ВС, НС"},
                    {"Миколаївська", "ВЕ, НЕ"},
                    {"Одеська", "ВН, НН"},
                    {"Полтавська", "ВІ, НІ"},
                    {"Рівненська", "ВК, НК"},
                    {"Сумська", "ВМ, НМ"},
                    {"Севастопіль", "СН, ІН"},
                    {"Тернопільська", "ВО, НО"},
                    {"Харківська", "АХ, КХ"},
                    {"Херсонська", "ВТ, НТ"},
                    {"Хмельницька", "ВХ, НХ"},
                    {"Черкаська", "СА, ІА"},
                    {"Чернівецька", "СЕ, ІЕ"},
                    {"Чернігівська", "СВ, ІВ"}
                };
            }

            public void Start()
            {
                string reply;
                server_listener.Start();
                Console.WriteLine("Waiting for Connection....");
                TcpClient client = server_listener.AcceptTcpClient();
                Console.WriteLine($"Connection established! with [{client.Client.LocalEndPoint}]");

                NetworkStream ns = client.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);

                while (true) { 

                    string msg = sr.ReadLine()!;
                    Console.WriteLine($"Member: [{client.Client.LocalEndPoint}]  Message: [{msg}]");


                    if (carRegions.ContainsKey(msg))
                    {
                        reply = carRegions[msg];
                    }
                    else
                    {
                        var region = carRegions.FirstOrDefault(x => x.Value.Contains(msg)).Key;

                        if (region != null) { reply = region; }
                        else { reply = "Wrong Code/Region"; }
                    }

                    sw.WriteLine( reply );
                    sw.Flush();
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
