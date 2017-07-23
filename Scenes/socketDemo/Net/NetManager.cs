using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

namespace Net
{
    public class NetManager : MonoBehaviour
    {
        //private static NetManager _instance = new NetManager();
        private static NetManager _instance;
        public static NetManager Instance
        {
            get
            {
                return _instance;
            }
        }
        void Awake()
        {
            _instance = this;
        }

        //private Dictionary<Type, TocHandler> _handlerDic;
        private SocketClient _socketClient;
        SocketClient socketClient
        {
            get
            {
                if (_socketClient == null)
                {
                    _socketClient = new SocketClient();
                }
                return _socketClient;
            }
        }

        void Start()
        {
            Init();
        }

        public void Init()
        {
            //_handlerDic = new Dictionary<Type, TocHandler>();
            socketClient.OnRegister();
        }

        /// <summary>
        /// 发送链接请求
        /// </summary>
        public void SendConnect()
        {
            socketClient.SendConnect();
        }

        /// <summary>
        /// 关闭网络
        /// </summary>
        public void OnRemove()
        {
            socketClient.OnRemove();
        }

        /// <summary>
        /// 发送SOCKET消息
        /// </summary>
        public void SendMessage(ByteBuffer buffer)
        {
            socketClient.SendMessage(buffer);
        }

        /// <summary>
        /// 发送SOCKET消息
        /// </summary>
        public void SendMessages(string sendStr)
        {
            //byte[] buf = System.Text.Encoding.UTF8.GetBytes(sendStr);
            //byte[] bufLen = WriteShort((short)buf.Length);
            //byte[] buff = new byte[buf.Length+bufLen.Length];
            //bufLen.CopyTo(buff, 0);
            //buf.CopyTo(buff, bufLen.Length);
            //socketClient.SessionSend(buff);


            ByteBuffer buff = new ByteBuffer();
            buff.WriteString(sendStr);
            socketClient.SendMessage(buff);


        }


        /// <summary>
        /// 连接 
        /// </summary>
        public void OnConnect()
        {
            Debug.Log("======连接========");
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void OnDisConnect()
        {
            Debug.Log("======断开连接========");
        }

        
        /// <summary>
        /// 交给Command，这里不想关心发给谁。
        /// </summary>
        void Update()
        {
          
        }
    }

}
