using System;
using UnityEngine;
using System.Collections.Generic;
using LitJson;

namespace Net
{
    
	/// <summary>
	/// 消息分发类
	/// </summary>
	public class SocketEventHandle:MonoBehaviour
	{
		private static SocketEventHandle _instance;

		public  delegate void ServerCallBackEvent (ClientResponse response);
		public  delegate void ServerDisconnectCallBackEvent ();
		public ServerCallBackEvent LoginCallBack;//登录回调
		public ServerCallBackEvent HelloCallBack;//创建房间回调

        public ServerDisconnectCallBackEvent disConnetNotice; //断线



		private List<ClientResponse> callBackResponseList;

		private bool isDisconnet = false;
        //private DateTime now;

		public SocketEventHandle ()
		{
			callBackResponseList = new List<ClientResponse> ();
		}

		void Start(){
			//SocketEventHandle.getInstance;
            //now = DateTime.UtcNow;
		}

        //public static GameObject temp = new GameObject();
        public static SocketEventHandle getInstance{
			//if (_instance == null) {
			//	GameObject temp = new GameObject ();
			//	_instance = temp.AddComponent<SocketEventHandle> ();
			//}
			//return _instance;
            get
            {
                return _instance;
            }
		}
        void Awake()
        {
            _instance = this;
        }

        void Update () {
			
		}

		void FixedUpdate(){
			while(callBackResponseList.Count >0){
				ClientResponse response = callBackResponseList [0];
				callBackResponseList.RemoveAt (0);
				dispatchHandle (response);

			}

            //判断是否断线
            /******************
			DateTime n = DateTime.UtcNow;
            TimeSpan ts = n - now;
            if (ts.TotalSeconds > 10)
            {
                //ts = n - GlobalDataScript.getInstance().xinTiaoTime;
                //if (ts.TotalSeconds > 20)
                CustomSocket.getInstance().sendHeadData2();
            }
            now = n;
			**************/

            if (isDisconnet) {
				isDisconnet = false;
				disConnetNotice ();
			}
		}


        void OnApplicationFocus(bool isFocus)
        {
            Debug.Log("--------OnApplicationPause---" + isFocus);

            if (isFocus)
            {
				Debug.Log("返回到游戏");  //  返回游戏的时候触发     执行顺序 2
				//CustomSocket.getInstance().sendHeadData();
            }
            else
            {
				Debug.Log("离开游戏");  //  离开游戏时触发
            }
        }

        private void dispatchHandle(ClientResponse response){
            JsonData jsonData2 = JsonMapper.ToObject(System.Text.Encoding.UTF8.GetString(response.bytes));
            Debug.Log(jsonData2["Hello"]);
            switch (jsonData2["Hello"]["Name"].ToString())
            {
			case "kas":
                    if (HelloCallBack != null)
                    {
                        HelloCallBack(response);
                    }
                    break;
			case "Login":
				if (LoginCallBack != null) {
                        
                        LoginCallBack(response);
				}
				break;

			}


        }

		public void addResponse(ClientResponse response){
			callBackResponseList.Add (response);
		}


		public void noticeDisConect(){
			isDisconnet = true;
		}
	}
}

