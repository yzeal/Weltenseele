using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using  UnityEngine.UI;

public class StartGame : MonoBehaviour {

	public string startLevel = "stadtlevel2";

	public GameObject fortsetzenButton;
	public GameObject startButton;

	private Button fortB;

	public EventSystem eS;

	private GameObject player;

	void Awake(){
		player = GameObject.FindWithTag("Player");
		player.SetActive(false);
	}

	// Use this for initialization
	void Start () {
//		Application.LoadLevel("stadtlevel2");
		if(PlayerPrefs.GetInt("safeGame") == 0){
			fortB = fortsetzenButton.GetComponent<Button>();
			eS.SetSelectedGameObject(startButton);
			fortB.interactable = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGame(){
		PlayerPrefs.DeleteAll();
		GlobalVariables.Instance.load();
		PlayerPrefs.SetInt("safeGame", 1);
		player.SetActive(true);
		Application.LoadLevel(startLevel);
	}

	public void Continue(){
		player.SetActive(true);
		player.GetComponent<Player>().enabled = true;
//		Application.LoadLevel(startLevel);
		Application.LoadLevel(GlobalVariables.Instance.currentScene);
	}
}
