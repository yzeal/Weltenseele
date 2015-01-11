using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using  UnityEngine.UI;

public class StartGame : MonoBehaviour {

	public GameObject fortsetzenButton;
	public GameObject startButton;

	private Button fortB;

	public EventSystem eS;

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
		Application.LoadLevel("stadt");
	}

	public void Continue(){
		Application.LoadLevel("stadt");
	}
}
