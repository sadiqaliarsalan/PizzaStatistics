using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WebPizzaCommon.Managers
{
    public interface ITcpConnectionManager
    {
        void SendData(string data);
        TcpClient AcceptClient(); 
        string ReadData(TcpClient client); 
        void Dispose();
    }
}
