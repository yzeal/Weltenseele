using UnityEngine;
using System.Collections;

public class PickupItem : MonoBehaviour {

	public float angle = 0f;
	public float offset = 0.1f;

	private Quaternion newRotation = Quaternion.identity;

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
		MeshCollider[] sc = GetComponents<MeshCollider>();
		foreach(MeshCollider sphereCol in sc){
			sphereCol.enabled = true;
		}
		transform.parent = null;
		rigidbody.isKinematic = false;
	}

	void OnTriggerEnter(Collider other){
		if(!pickedUp && other.CompareTag("Player")){
			pickedUp = true;
			transform.position = new Vector3(hand.transform.position.x, hand.transform.position.y + offset, hand.transform.position.z);
			transform.parent = hand.transform;
			rigidbody.isKinematic = true;
			MeshCollider[] sc = GetComponents<MeshCollider>();
			foreach(MeshCollider sphereCol in sc){
				sphereCol.enabled = false;
			}
		}
	}

	void OnTriggerStay(Collider other){
		if(pickedUp && other.CompareTag("Player")){
			newRotation.eulerAngles = new Vector3(hand.transform.rotation.x + angle, hand.transform.rotation.y, hand.transform.rotation.z);
			transform.rotation = hand.transform.rotation;
			transform.localRotation = newRotation;
		}
	}

	void OnTriggerExit(Collider other){
		if(other.CompareTag("Player")){
			pickedUp = false;
		}
	}
}
