using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using  UnityEngine.UI;

public class StartGame : MonoBehaviour {

	public string startLevel = "stadt";

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
//		Application.LoadLevel("stadt");
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
		PlayerPrefs.SetInt("safeGame", 1);
		player.SetActive(true);
		Application.LoadLevel(startLevel);
	}

	public void Continue(){
		player.SetActive(true);
		Application.LoadLevel(startLevel);
	}
}
