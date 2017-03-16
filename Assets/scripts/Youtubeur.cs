using UnityEngine;
using System.Collections;

public class Youtubeur : MonoBehaviour {

	public float boost;
	public float movingDuration;
	public float speed;

	float movingTime = 0;
	float movingDirection = 1;
	bool movingHalf = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		moveYoutubeur ();
	}

	void moveYoutubeur(){
		movingTime += Time.deltaTime;
		transform.Translate(new Vector2(movingDirection*speed, 0));

		//Une fois...
		if (movingHalf && movingTime > movingDuration / 2) {
			movingTime = 0;
			movingDirection *= -1;
			movingHalf = false;
		}
		if (movingTime > movingDuration) {
			movingTime = 0;
			movingDirection *= -1;
		}
	}
}
