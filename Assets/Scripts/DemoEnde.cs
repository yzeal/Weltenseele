using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using com.ootii.AI.Controllers;

public class DemoEnde : MonoBehaviour {

	private EventSystem es;
	private GameObject button;
	
	// Use this for initialization
	void Start () {
		MotionController mc = Player.Instance.GetComponent<MotionController>();
		mc.QueueMotion(0, mc.GetMotion(0, typeof(CasualIdle)));
		
		mc.UseInput = false;
		es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		button = GameObject.Find("Button");
	}
	
	// Update is called once per frame
	void Update () {
		es.SetSelectedGameObject(button);
	}

	public void Ende(){
		MotionController mc = Player.Instance.GetComponent<MotionController>();
		mc.UseInput = true;
		GlobalVariables.Instance.EndDemo();
		Destroy(gameObject);
	}
}
