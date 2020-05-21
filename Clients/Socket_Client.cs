using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Clients
{
    /// <summary>
    /// 客户端
    /// </summary>
    class Socket_Client
    {
        public Socket_Client(string ip, int port)
        {
            this.iPAddress = IPAddress.Parse(ip);
            this.port = port;
        }

        /// <summary>
        /// 套接字
        /// </summary>
        private Socket client_socket;
        /// <summary>
        /// 客户端要连接的ip地址
        /// </summary>
        private IPAddress iPAddress;
        /// <summary>
        /// 客户端要连接的端口好
        /// </summary>
        private int port;
        /// <summary>
        /// 创建客户端连接的套接字
        /// </summary>
        /// <returns></returns>
        public Socket Create_Client_Socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        /// <summary>
        /// 连接服务器
        /// </summary>
        public void Connect_Server()
        {
            client_socket = Create_Client_Socket();
            //tcp连接服务器的时候只需要连接一次，因为tcp是长链接
            client_socket.Connect(new IPEndPoint(iPAddress, port));
        }

        /// <summary>
        /// 接收来自服务器的消息
        /// </summary>
        public void Recv_Msg_By_Client()
        {
            while (true)
            {
                byte[] ser_msg = new byte[1024];
                int count = client_socket.Receive(ser_msg);
                string str_msg = Encoding.UTF8.GetString(ser_msg, 0, count);
                if (count > 0)
                {
                    Console.WriteLine("接收到来自{0}的消息为：{1}", client_socket.RemoteEndPoint, str_msg);
                }
            }
        }  
        /// <summary>
        /// 向服务器发送请求
        /// </summary>
        public void Request_Client()
        {
            while (true)
            {
                Console.WriteLine("请输入你要发送到服务器的消息：");
                string send_msg = Console.ReadLine();
                byte[] by_msg = Encoding.UTF8.GetBytes(send_msg);
                client_socket.Send(by_msg);
            }
        }
    }
}
