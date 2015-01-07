using UnityEngine;
using System.Collections;

public class PickupItem : MonoBehaviour {

//	private GameObject player;
	private GameObject hand;

	private bool pickedUp;

	// Use this for initialization
	void Start () {
//		player = GameObject.FindWithTag("Player");
		hand = GameObject.FindWithTag("Hand");
	}
	
	// TODO Drop() sollte vom Player aufgerufen werden.
	void Update () {
		if(pickedUp && Input.GetKeyDown("h")){
			Drop();
		}
	}

	public void Drop(){
		SphereCollider[] sc = GetComponents<SphereCollider>();
		foreach(SphereCollider sphereCol in sc){
			sphereCol.enabled = true;
		}
		transform.parent = null;
		rigidbody.isKinematic = false;
	}

	void OnTriggerEnter(Collider other){
		if(!pickedUp && other.CompareTag("Player")){
			pickedUp = true;
			transform.position = hand.transform.position;
			transform.parent = hand.transform;
			rigidbody.isKinematic = true;
			SphereCollider[] sc = GetComponents<SphereCollider>();
			foreach(SphereCollider sphereCol in sc){
				sphereCol.enabled = false;
			}
		}
	}

	void OnTriggerExit(Collider other){
		if(other.CompareTag("Player")){
			pickedUp = false;
		}
	}
}
