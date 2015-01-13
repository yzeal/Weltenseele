using UnityEngine;
using System.Collections;

public class Ende : MonoBehaviour {

	public GameObject message;

	private bool ende;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		if(!ende && other.CompareTag("Player")){
			ende = true;
			Instantiate(message);
		}
	}
}
