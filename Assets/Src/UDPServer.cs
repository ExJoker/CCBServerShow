using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPServer
{
    private static UDPServer instance;

    public static UDPServer Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UDPServer();
            }
            return instance;
        }
    }

    public string recvStr;

    Socket ReceiveSocket;
    Socket SendSocket;
    EndPoint SnedPoint;
    EndPoint ReceivePoint;
    string sendStr;
    byte[] recvData = new byte[1024];
    byte[] sendData = new byte[1024];
    int recvLen;
    Thread connectThread;
    //初始化  
    public void InitSocket(string ip, int ReceivePort, int SendPort)
    {
        ReceivePoint =(EndPoint) new IPEndPoint(IPAddress.Any, ReceivePort);                        //本地IP
        ReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); //创建Socket
        ReceiveSocket.Bind(ReceivePoint);                                                           //发送EndPoint
        //开启一个线程连接  
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
        //定义客户端  
        SendSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        SnedPoint = new IPEndPoint(IPAddress.Parse(ip), SendPort);
    }

    public void SocketSend(byte[] msg)
    {
        //SendSocket.SendTo(msg, msg.Length, SocketFlags.None, SnedPoint);
    }
    //服务器接收  
    public void SocketReceive()
    {
        while (true)
        {
            recvData = new byte[1024];
            try
            {

                recvLen = ReceiveSocket.ReceiveFrom(recvData, ref ReceivePoint);
                if (recvLen > 0)
                {
                    string recvStr = Encoding.UTF8.GetString(recvData, 0, recvLen);
                    GameManager.Instance.handleReceiveMessage(recvStr);
                    NewHouseUI.Instance.NewhandleReceiveMessage(recvStr);
                }

            }
            catch (Exception e)
            {
            }
        }
    }

    //连接关闭  
    public void SocketQuit()
    {
        //关闭线程  
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最后关闭socket  
        if (ReceiveSocket != null)
            ReceiveSocket.Close();

    }


}
