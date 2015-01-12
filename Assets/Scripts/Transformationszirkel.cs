using UnityEngine;
using System.Collections;

public class Transformationszirkel : MonoBehaviour {

	public GameObject message;

	public bool done;

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("IncreasedJumpHight") != 0){
			done = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(){
		Debug.Log("Zirkel getriggert.");
		if(!done){
			done = true;
			Player.Instance.IncreaseJumpHight();
			Instantiate(message);
			Debug.Log ("Message: ");
		}
	}
}
