using UnityEngine;
using UnityEngine.SceneManagement;

public class DeviceInfo : MonoBehaviour
{
	// GUI variables
	public Rect HUDdisplay;             // rectangular area to draw frame counter in
	public GUIStyle HUDstyle;           // set the text style of the frame counter
	string HUDtext;

//	WebCamTexture CamTexture;
	WebCamDevice[] WebCamDevices;
//	int deviceIndex;

	// Awake is called when the object is loaded or created
	void Awake()
	{

	}

	// Start is called before the first frame update
	void Start()
	{
		// GUI settings
		HUDstyle.alignment = TextAnchor.UpperLeft;   // sets text flow left to right from top
		HUDstyle.fontSize = 24;                         // font size to 24
		HUDstyle.normal.textColor = Color.white;        // text color white White
		HUDstyle.wordWrap = true;


		// sets HUD display to top left corner of screen
		HUDdisplay = new Rect(0, 0, 960, 600);

		Input.gyro.enabled = true;

		// build an array of webcam devices
		WebCamDevices = WebCamTexture.devices;

		HUDtext = "";
		HUDtext += "Screen: " + Screen.width + "," + Screen.height + "\n";
		HUDtext += "Resolution: " + Screen.currentResolution + "\n";
		HUDtext += "Internet: " + Application.internetReachability + "\n";
		HUDtext += "Editor: " + Application.isEditor + "\n";
		HUDtext += "Mobile: " + Application.isMobilePlatform + "\n";
		HUDtext += "Console: " + Application.isConsolePlatform + "\n";
		HUDtext += "Platform: " + Application.platform + "\n";
		HUDtext += "Gyro: " + SystemInfo.supportsGyroscope + "\n";
		HUDtext += "Gyro: " + Input.gyro.attitude.z + "\n";
		HUDtext += "DeviceType: " + SystemInfo.deviceType + "\n";
		HUDtext += "Model: " + SystemInfo.deviceModel + "\n";
		HUDtext += "Name: " + SystemInfo.deviceName + "\n";
		HUDtext += "OS: " + SystemInfo.operatingSystem + "\n";
		HUDtext += "OSfam: " + SystemInfo.operatingSystemFamily + "\n";
		HUDtext += "WebCams: " + WebCamDevices.Length + "\n";
		for (int i = 0; i < WebCamDevices.Length; i++)
		{
			HUDtext += "Cam" + i + ": " + WebCamDevices[i].name + (WebCamDevices[i].isFrontFacing ? " front" : " back") + "\n";
		}
		//deviceIndex = 0;
		//SetWebCamTexture(deviceIndex);
	}

	// Update is called once per frame
	void Update()
	{

	}

	// use the webcam index to make a texture
	//void SetWebCamTexture(int index)
	//{
	//	// if there is a CamTexture playing stop it
	//	if (CamTexture != null)
	//	{
	//		if (CamTexture.isPlaying)
	//		{
	//			CamTexture.Stop();
	//		}
	//		Destroy(CamTexture);
	//	}
	//	// choose a webcam based on index value
	//	CamTexture = new WebCamTexture(WebCamDevices[index].name,512,512,30);
	//	// if it worked set to this object's texture to the webcam and start playing
	//	if (CamTexture != null)
	//	{
	//		GetComponent<Renderer>().material.mainTexture = CamTexture;

	//		if (!CamTexture.isPlaying)
	//		{
	//			CamTexture.Play();
	//		}
	//	}
	//}

	// Display HUD values
	void OnGUI()
	{
		//Calculate change aspects - WebGL default is 960x600
		float resX = (float)(Screen.width) / 960f;
		float resY = (float)(Screen.height) / 600f;
		//Set matrix
		GUI.matrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(resX, resY, 1));

		GUI.Box(HUDdisplay, "");                   // displays default GUI box without header
		GUI.Label(HUDdisplay, HUDtext, HUDstyle);  // shows text in box with defined style

		//// if there is more than 1 camera, display button to iterate 
		//if (WebCamDevices.Length > 1)
		//{
		//	// test if CAM button pressed
		//	if (GUI.Button(new Rect(960 - 100 - 75, 600-10-50, 75, 50), "CAM " + deviceIndex))
		//	{
		//		Debug.Log(""); // trick to clear missing messages
		//		deviceIndex++;  // increment to iterate cams
		//						// wrap when exceeding the WebCamDevices array
		//		if (deviceIndex >= WebCamDevices.Length)
		//		{
		//			deviceIndex = 0;
		//		}
		//		SetWebCamTexture(deviceIndex); // set the new webcam device
		//	}
		//}

		if(GUI.Button(new Rect(960/2 - 25, 600 - 10 - 50, 50, 50), "BACK"))
		{
			SceneManager.LoadScene(0);      // load the MENU scene at index 0
		}
	}
}
