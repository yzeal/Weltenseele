using UnityEngine;
using System.Collections;
using  UnityEngine.UI;

public class Minimap : MonoBehaviour {

	private GameObject player;
	private RectTransform map;
	private RectTransform playerImage;
	private RectTransform cvs;

	private float factor;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
		map = GameObject.Find("ImageMinimap").GetComponent<RectTransform>();
		cvs = GameObject.Find("MinimapCanvas").GetComponent<RectTransform>();
		playerImage = GetComponent<RectTransform>();

		factor = Screen.width / Screen.height;

		Debug.Log(cvs.rect.height);

//		playerImage.position = new Vector3(Screen.width - 50f + player.transform.position.x/2f, player.transform.position.z/2f + 50f, 0f);
//		playerImage.anchoredPosition = new Vector2(Screen.width/2f - 50f + player.transform.position.x/2f, player.transform.position.z/2f + 50f);
	}
	
	// Update is called once per frame
	void Update () {
//		playerImage.position = new Vector3(Screen.width - 50f + player.transform.position.x/2f, player.transform.position.z/2f + 50f, 0f);
//		playerImage.anchoredPosition = new Vector2(Screen.width/2f - 50f + player.transform.position.x/2f, player.transform.position.z/2f + 50f);
		playerImage.anchoredPosition = new Vector2(-player.transform.position.x*0.2f*1.5f, -player.transform.position.z*0.3f*1.5f);
		playerImage.localRotation = new Quaternion(playerImage.localRotation.x, playerImage.localRotation.y, player.transform.localRotation.y, playerImage.rotation.w);
	}
}
