using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public Cesar cesar;

	/*Camera*/
	public Camera lacamera = null;
	public float cameraOffsetY = 0;

	/*Lose Collider*/
	public Collider2D loseCollider = null;
	public float minLoseDistance = 1f;

	/*Nuages*/
	public Transform[] nuages;
	public float cloudCreationDistance = 0;
	public float cloudCreationMaxOffset = 0;
	public float largeurCreationDistance = 0;
	public float intervalleApparitionCloud;

	/*Youtubeur*/
	public Transform[] youtubeurs;
	public float youtubeurCreationDistance;
	public float youtubeurCreationMaxOffset;
	public float largeurCreationYoutubeur;
	public float intervalleApparitionYoutubeur;

	/*Dislike*/
	public Transform dislike;
	public float dislikeCreationDistance;
	public float dislikeCreationMaxOffset;
	public float largeurCreationDislike;
	public float intervalleApparitionDislike;

	/*BG*/
	public Transform background;

	private float differenceBGCam;

	// Use this for initialization
	void Start () {
		differenceBGCam = lacamera.transform.position.y - background.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		//Camera follow...
		if (this.isProgressing()){
			cameraFollowCesar ();
		}

		/*Lose Collider Follow*/
		loseColliderFollow ();

		/* BG Follow */ 
		backgroundFollow ();
	}

	public void gameBegin(){
		InvokeRepeating ("createClouds", 2f, intervalleApparitionCloud);
		InvokeRepeating ("createYoutubeur", 1f, intervalleApparitionYoutubeur);
		InvokeRepeating ("createDislike", 2f, intervalleApparitionDislike);
	}

	void cameraFollowCesar(){
		lacamera.transform.position = new Vector3 (lacamera.transform.position.x, cesar.transform.position.y + cameraOffsetY, lacamera.transform.position.z);
	}

	void loseColliderFollow(){
		if (cesar.transform.position.y - loseCollider.transform.position.y > minLoseDistance) {
			loseCollider.transform.position = new Vector3 (loseCollider.transform.position.x, cesar.transform.position.y - minLoseDistance, loseCollider.transform.position.z);
		}
	}

	void createClouds(){
		float xOffSet = Random.Range (-1*largeurCreationDistance,largeurCreationDistance );
		float yOffSet = Random.Range (0, cloudCreationMaxOffset);
		Vector2 creationPosition = new Vector2 (cesar.transform.position.x + xOffSet, cesar.transform.position.y + cloudCreationDistance + yOffSet);
		int cloudType = Random.Range (0, 4); 
		Instantiate (nuages [cloudType], creationPosition, Quaternion.identity);
	}

	void createYoutubeur(){
		float xOffSet = Random.Range (-1*largeurCreationDislike,largeurCreationDislike );
		float yOffSet = Random.Range (0, youtubeurCreationMaxOffset);
		Vector2 creationPosition = new Vector2 (cesar.transform.position.x + xOffSet, cesar.transform.position.y + youtubeurCreationDistance + yOffSet);
		int youtubeype = Random.Range (0, 3); 
		Instantiate (youtubeurs [youtubeype], creationPosition, Quaternion.identity);
	}

	void createDislike(){
		float xOffSet = Random.Range (-1*largeurCreationYoutubeur,largeurCreationYoutubeur );
		float yOffSet = Random.Range (0, dislikeCreationMaxOffset);
		Vector2 creationPosition = new Vector2 (cesar.transform.position.x + xOffSet, cesar.transform.position.y + dislikeCreationDistance + yOffSet);
		Instantiate (dislike, creationPosition, Quaternion.identity);
	}

	bool isProgressing(){
		return (cesar.getDirection () == 1) && (cesar.transform.position.y + cameraOffsetY > lacamera.transform.position.y);
	}

	void backgroundFollow(){
		background.transform.position = new Vector3 (background.position.x, lacamera.transform.position.y + differenceBGCam, background.position.z);
	}
}
