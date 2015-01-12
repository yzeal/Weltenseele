using UnityEngine;
using System.Collections;
using com.ootii.AI.Controllers;
using com.ootii.Cameras;

public class Player : MonoBehaviour {

	public static Player Instance { get; private set; }

	public AdventureRig camRig;

	public float originalJumpImpulse = 31f;
	public float increasedJumpImpulse = 50f;
	public bool jumpHeightIncreased;

	private Jump jump;

	void Awake(){
		
		if(Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		
		Instance = this;
		
		DontDestroyOnLoad(gameObject);
		DontDestroyOnLoad(GetComponent<MotionController>());

		camRig = GameObject.FindWithTag("MainCameraRig").GetComponent<AdventureRig>();
		camRig._Controller = GetComponent<MotionController>();
		GetComponent<MotionController>()._CameraRig = camRig;
		GetComponent<MotionController>()._CameraTransform = GameObject.FindWithTag("MainCameraRig").transform;

	}

	void OnLevelWasLoaded(){
		camRig = GameObject.FindWithTag("MainCameraRig").GetComponent<AdventureRig>();
		camRig._Controller = GetComponent<MotionController>();
		GetComponent<MotionController>()._CameraRig = camRig;
		GetComponent<MotionController>()._CameraTransform = GameObject.FindWithTag("MainCameraRig").transform;

		Debug.Log(jumpHeightIncreased);
		if(jumpHeightIncreased){
			jump = (Jump)GameObject.FindWithTag("Player").GetComponent<MotionController>().GetMotion(0,typeof(Jump));
			jump.Impulse = increasedJumpImpulse;
			jumpHeightIncreased = true;
		}else{
			DecreaseJumpHeight();
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//dirty bug fix
		if(camRig._Controller == null){
			camRig._Controller = GetComponent<MotionController>();
		}
//		if(jumpHeightIncreased && jump.Impulse < increasedJumpImpulse){
//			jump = (Jump)GameObject.FindWithTag("Player").GetComponent<MotionController>().GetMotion(0,typeof(Jump));
//			jump.Impulse = increasedJumpImpulse;
//			jumpHeightIncreased = true;
//		}
	}

	public void IncreaseJumpHight(){
		
		jump = (Jump)GameObject.FindWithTag("Player").GetComponent<MotionController>().GetMotion(0,typeof(Jump));
		jump.Impulse = increasedJumpImpulse;
		jumpHeightIncreased = true;

		if(GlobalVariables.Instance.autoSave) GlobalVariables.Instance.save();
	}

	public void DecreaseJumpHeight(){
		
		jump = (Jump)GameObject.FindWithTag("Player").GetComponent<MotionController>().GetMotion(0,typeof(Jump));
		jump.Impulse = originalJumpImpulse;
		jumpHeightIncreased = false;
		
		if(GlobalVariables.Instance.autoSave) GlobalVariables.Instance.save();
	}
}
