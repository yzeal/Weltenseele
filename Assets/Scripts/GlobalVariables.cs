using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using com.ootii.AI.Controllers;

//public struct PlayerData{
//	public Vector3 position;
//	public Quaternion rotation;
//}

public class GlobalVariables : MonoBehaviour {

//	public bool[] switches;
	public bool deleteProgressAtStart; //zum Testen
	public bool autoSave;

	public string currentScene;
	public string lastScene;
	
	public float minotaurusSpeed = 3.5f;

	public static GlobalVariables Instance { get; private set; }

	public Dictionary<string, PlayerData> playerDataPerScene;

	public Vector3 savePoint = Vector3.zero;

	private GameObject player;
	
	public bool inCrawlArea;
	public bool crawling;
	public float crawlBugFix;




	void Awake(){
		
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		
		Instance = this;
		
		DontDestroyOnLoad(gameObject);

		if(deleteProgressAtStart){
			PlayerPrefs.DeleteAll();
			//TODO neue levelSequence
		}//else{
//			load();
//		}

		player = GameObject.FindWithTag("Player");
//		Debug.Log(player.transform.position.x);

		playerDataPerScene = new Dictionary<string, PlayerData>();
//		currentScene = "stadtlevel2";
//		load();
	}

	void Start(){
		load();
	}
	

	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Weltenseele")){
			weltenseeleTeleport();			
		}

		if(Input.GetButton("mainMenu")){
			if(autoSave) save();
			Application.LoadLevel("start");
		}

		if(Input.GetButton("resetCamera")){
//			GameObject.FindWithTag("MainCameraRig").transform.position = player.transform.position - 1f * player.transform.forward;
			GameObject camRig = GameObject.FindWithTag("MainCameraRig");
			camRig.transform.position = player.transform.position - 2f * player.transform.forward + 1.2f * Vector3.up;
		}
		
		crawlBugFix += Time.deltaTime;
		if(crawlBugFix >= 2f){
			inCrawlArea = false;
			crawlBugFix = 0f;
		}

		//TESTI
		if(Input.GetKeyDown("j")){
			Player.Instance.IncreaseJumpHight();
			Player.Instance.jumpHeightIncreased = true;
		}

	}

	private void weltenseeleTeleport(){
		if(Player.Instance.GetComponent<MotionController>().IsGrounded){
			if(Application.loadedLevelName == "Weltenseele"){
				updatePlayerData();
				changeScene(GlobalVariables.Instance.lastScene);
			}else{
				updatePlayerData();
				changeScene("Weltenseele");
			}
		}
	}

	void OnLevelWasLoaded(){
		player = GameObject.FindWithTag("Player");
		if(player != null){
			player.GetComponent<Player>().enabled = true;
		}
		
		inCrawlArea = false;
		crawlBugFix = 0f;
	}

	public void load(){
		currentScene = PlayerPrefs.GetString("CurrentScene");
		lastScene = PlayerPrefs.GetString("LastScene");

		if(currentScene == "") currentScene = "stadtlevel2";
		if(lastScene == "") lastScene = "stadtlevel2";
		
		if(PlayerPrefs.GetInt("Crawling") != 0){	
			crawling = true;
		}else{
			crawling = false;
		}
		if(PlayerPrefs.GetInt("IncreasedJumpHight") != 0 && Player.Instance != null){
			Player.Instance.jumpHeightIncreased = true;
		}else{
			Player.Instance.jumpHeightIncreased = false;
//			Player.Instance.DecreaseJumpHeight();
		}

	}

	public void save(){

		PlayerPrefs.SetString("CurrentScene", currentScene);
		PlayerPrefs.SetString("LastScene", lastScene);

		if(Player.Instance.jumpHeightIncreased){
			PlayerPrefs.SetInt("IncreasedJumpHight", 1);
			Debug.Log("jump height saved");
		}
		
		if(crawling){
			PlayerPrefs.SetInt("Crawling", 1);
		}

		Debug.Log("game saved");

		updatePlayerData();

	}

	public void changeScene(string sceneName){
		lastScene = Application.loadedLevelName;
		currentScene = sceneName;
		Application.LoadLevel(sceneName);
	}

	public void updatePlayerData(){

//		PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
//		PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
//		PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
//
//		PlayerPrefs.SetFloat("PlayerRotX", player.transform.rotation.x);
//		PlayerPrefs.SetFloat("PlayerRotY", player.transform.rotation.y);
//		PlayerPrefs.SetFloat("PlayerRotZ", player.transform.rotation.z);
//		PlayerPrefs.SetFloat("PlayerRotW", player.transform.rotation.w);
		if(Player.Instance.GetComponent<MotionController>().IsGrounded && !Player.Instance.killed){

			PlayerPrefs.SetFloat(Application.loadedLevelName + "PlayerPosX", player.transform.position.x);
			PlayerPrefs.SetFloat(Application.loadedLevelName + "PlayerPosY", player.transform.position.y);
			PlayerPrefs.SetFloat(Application.loadedLevelName + "PlayerPosZ", player.transform.position.z);
			
			PlayerPrefs.SetFloat(Application.loadedLevelName + "PlayerRotX", player.transform.rotation.x);
			PlayerPrefs.SetFloat(Application.loadedLevelName + "PlayerRotY", player.transform.rotation.y);
			PlayerPrefs.SetFloat(Application.loadedLevelName + "PlayerRotZ", player.transform.rotation.z);
			PlayerPrefs.SetFloat(Application.loadedLevelName + "PlayerRotW", player.transform.rotation.w);

			Debug.Log(Application.loadedLevelName + "PlayerPos Position saved: " + player.transform.position);
		}
//
//		PlayerData pd = new PlayerData();
//		pd.position = player.transform.position;
//		pd.rotation = player.transform.rotation;
//		if(!playerDataPerScene.ContainsKey(Application.loadedLevelName)){
//			DontDestroyOnLoad(pd);
//			playerDataPerScene.Add(Application.loadedLevelName, pd);
//		}else{
//			playerDataPerScene[Application.loadedLevelName] = pd;
//		}

//		foreach(KeyValuePair<string, PlayerData> pData in playerDataPerScene){
//			Debug.Log(pData.Key + ": " + pData.Value.position + ", " + pData.Value.rotation);
//		}
	}

	public void EndDemo(){
//		Application.LoadLevel("start");
		Application.Quit();
	}

}
