using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Clients
{
    class Program
    {
        private static byte[] result = new byte[1024];
        static void Main(string[] args)
        {    
            #region
            ////设定服务器IP地址 
            //IPAddress ip = IPAddress.Parse("127.0.0.1");
            //Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //try
            //{
            //    clientSocket.Connect(new IPEndPoint(ip, 8885)); //配置服务器IP与端口 
            //    Console.WriteLine("连接服务器成功");
            //}
            //catch
            //{
            //    Console.WriteLine("连接服务器失败，请按回车键退出！");
            //    return;
            //}
            ////通过clientSocket接收数据 
            //int receiveLength = clientSocket.Receive(result);
            //Console.WriteLine("接收服务器消息：{0}", Encoding.ASCII.GetString(result, 0, receiveLength));
            ////通过 clientSocket 发送数据 

            //while (true)
            //{
            //    Console.WriteLine("向服务器发送消息：");
            //    string sendMessage = Console.ReadLine();
            //    clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));
            //}
            //Console.ReadLine();
            #endregion
            Console.WriteLine("请输入你要连接的服务器的ip地址：");
            string ip = Console.ReadLine();
            Console.WriteLine("请输入你要连接的服务器的端口号：");
            int port = int.Parse(Console.ReadLine());

            //创建套接字
            Socket_Client s = new Socket_Client(ip, port);
            //连接服务器
            s.Connect_Server();
            //接收服务器的消息
            Thread recv = new Thread(s.Recv_Msg_By_Client);
            //给服务器发送消息
            Thread send = new Thread(s.Request_Client);
            recv.Start();
            send.Start();

            recv.Join();
            send.Join();
            Console.ReadKey();
        }
    }
}
