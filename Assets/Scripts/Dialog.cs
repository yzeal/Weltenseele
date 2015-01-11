using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class Dialog : MonoBehaviour {

	public int id;

	public AudioSource audioS;

	public float[] times; //in Sekunden
	public string[] texts;
	public int[] styles;
	//(Kein Dictionary, da der sich nicht einfach im Editor bearbeiten lässt. Eine Klasse schien dafür etwas viel Overhead.)

	public bool activated;
	
	public GUIStyle[] textStyles;

	private int currentText = 0;
	private Texture2D schwarz;

	public bool followPlayer;
	private GameObject player;


	void Start () {

		if(PlayerPrefs.GetInt(Application.loadedLevelName + "Subtitle" + id) != 0){
			Destroy(gameObject);
		}

		if(times.Length != texts.Length){
			Debug.Log("Untertitel: Anzahl Zeiten passt nicht zur Anzahl Texte.");
			Destroy(gameObject);
		}

		audioS = GetComponent<AudioSource>();

		if(audioS.clip == null){
			Debug.Log("Untertitel: Kein AudioClip vorhanden.");
			Destroy(gameObject);
		}

		schwarz = new Texture2D(1,1);
		schwarz.SetPixel(0,0, new Color(0f,0f,0f,0.5f));
		schwarz.Apply();

		player = GameObject.FindWithTag("Player");
	}
	

	void OnGUI(){
		if(activated){
			if(audioS.isPlaying){
				if(currentText < times.Length - 1 && audioS.time > times[currentText+1]){
					currentText++;
				}
				GUIStyle textStyle = textStyles[styles[currentText]];

				GUI.DrawTexture(new Rect(0f, Screen.height*0.9f, Screen.width, Screen.height*0.1f), schwarz, ScaleMode.StretchToFill);
				GUI.Label(new Rect(Screen.width*0.05f, Screen.height*0.9f, Screen.width*0.9f, Screen.height*0.1f), texts[currentText], textStyle);
			}else{
				Debug.Log("Untertitel: Vorbei.");
				if(GlobalVariables.Instance.autoSave)
					PlayerPrefs.SetInt(Application.loadedLevelName + "Subtitle" + id, 1);
				Destroy(gameObject);
			}
		}
	}

	void Update(){
		if(followPlayer){
			transform.position = player.transform.position;
		}
	}

	public void Activate(){
		if(!activated){
			activated = true;
			audioS.Play();
		}
	}

	public void DeactivatePause(){
		if(activated){
			activated = false;
			audioS.Pause();
		}
	}

	public void DeactivateReset(){
		if(activated){
			activated = false;
			audioS.Stop();
			audioS.time = 0f;
			currentText = 0;
		}
	}
}
