using UnityEngine;
using System.Collections;

public class Ende : MonoBehaviour {

	public GameObject message;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(){
		Instantiate(message);
	}
}
