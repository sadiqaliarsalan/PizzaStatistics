using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebPizzaCommon.Managers
{
    public class TcpConnectionManager : ITcpConnectionManager, IDisposable
    {
        private TcpClient _client;
        private TcpListener _server;
        private NetworkStream _stream;
        private bool _isServer;

        public TcpConnectionManager(string serverAddress, int serverPort, bool isServer = false)
        {
            _isServer = isServer;
            if (_isServer)
            {
                _server = new TcpListener(IPAddress.Any, serverPort);
                _server.Start();
                Console.WriteLine("Server started on port " + serverPort);
            }
            else
            {
                _client = new TcpClient();
                _client.Connect(serverAddress, serverPort);
                _stream = _client.GetStream();
            }
        }

        public void SendData(string data)
        {
            if (_stream == null)
            {
                throw new InvalidOperationException("Stream is not initialized");
            }

            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            _stream.Write(dataBytes, 0, dataBytes.Length);
        }

        public TcpClient AcceptClient()
        {
            TcpClient client = _server.AcceptTcpClient();
            Console.WriteLine("Client connected.");
            return client;
        }

        public string ReadData(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }

        public void Dispose()
        {
            _stream?.Dispose();
            _client?.Close();
            _server?.Stop();
        }
    }
}
