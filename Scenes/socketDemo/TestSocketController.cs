using UnityEngine;
using System.Collections;
using LitJson;
using Net;





// Demo class
// inherits of WebSocketUnityDelegate to receive WebSockets events
public class TestSocketController : MonoBehaviour {

	// boolean to manage display
	private bool sendingMessage = false;
	private bool receivedMessage = false;
    private bool isConnect = false;
	
	
	// Use this for initialization
	void Start () {
        addListener();

    }
	
	// Update is called once per frame
	void Update () {
	}

    public void addListener()
    {
        SocketEventHandle.getInstance.HelloCallBack += HelloGame;

    }

    private void removeListener()
    {
        SocketEventHandle.getInstance.HelloCallBack -= HelloGame;
    }

    /// <summary>
	/// 测试的游戏handle
	/// </summary>
	/// <param name="response">Response.</param>
	public void HelloGame(ClientResponse response)
    {
        Debug.Log("HelloGame...");
        JsonData jsonData2 = JsonMapper.ToObject(System.Text.Encoding.UTF8.GetString(response.bytes));
        Debug.Log(jsonData2["Hello"]["Name"]);
    }

        void OnGUI(){
		
		// Make a background box
		GUI.Box(new Rect(10,10,400,400), "Actions");
		
		// Check if the websocket has an opened connection
		if(!isConnect)
        {
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			if(GUI.Button(new Rect(30,40,320,80), "Connect")) {
				
			
				
                NetManager.Instance.SendConnect();
                isConnect = true;

            }
		}else{
			// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
			if(GUI.Button(new Rect(30,40,320,80), "Disconnect")) {
			
				// Third Step : we need to close the connection when we finish
			}
			
			if(GUI.Button(new Rect(30,130,320,80), "Send message data")) {
			

                // create or modify json object
                JsonData newFriend = new JsonData();
                newFriend["Hello"] = new JsonData();
                newFriend["Hello"]["Name"] = "terry";
                string strFriends = newFriend.ToJson();
                Debug.Log(strFriends);

                //webSocket.Send(strFriends);
                NetManager.Instance.SendMessages(strFriends);
				sendingMessage = true;
			}
			
			if(sendingMessage){
				GUI.Label(new Rect(30, 350, 400, 60), "Waiting answer from server...");
			}else if(receivedMessage){
				GUI.Label(new Rect(30, 350, 400, 60), "Server sent an echo answer !");
			}
			
		}
	}

}
