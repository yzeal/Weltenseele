using UnityEngine;
using System.Collections;

public class Preloader : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		GameObject.FindWithTag("Player").SetActive(false);
		Application.LoadLevel("start");
	}

}
