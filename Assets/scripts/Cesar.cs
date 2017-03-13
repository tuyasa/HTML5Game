using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Cesar : MonoBehaviour {

	public float strength = 20f;
	public Collider2D loseCollider;
	public float moveSpeed = 6f;
	public float maxLeft = -3.163f;
	public float maxRight = 3.163f;
	public GameObject levelManager;
	static public int score = 0;

	private Vector2 prevLocation;

	// Use this for initialization
	void Start () {
		prevLocation = transform.position;
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) == true && transform.GetComponent<Rigidbody2D> ().isKinematic) {
			transform.GetComponent<Rigidbody2D> ().isKinematic = false;
			score = 0;
			levelManager.GetComponent<LevelManager> ().gameBegin ();
			this.boost (strength);
			Debug.Log ("click");
		}
		if (Input.GetKey ("q") && transform.position.x > maxLeft) {
			transform.Translate (Vector2.left * moveSpeed * Time.deltaTime);
		}

		if (Input.GetKey ("d") && transform.position.x <maxRight) {
			transform.Translate (Vector2.right * moveSpeed * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (collider == loseCollider || collider.transform.tag.CompareTo("Dislike") == 0) {
			score = ((int)transform.position.y) * 10;
			SceneManager.LoadScene ("Lose");
		}

		if (collider.GetComponent<Youtubeur> () != null) {
			this.boost (collider.GetComponent<Youtubeur> ().boost);
		}
	}

	public int getDirection(){ 
		int ans = 0;
		Vector2 currentPosition = transform.position;
		Vector2 vel = (currentPosition - prevLocation) / Time.deltaTime;
		if (vel.y > 0) {
			ans = 1;
		} else if (vel.y < 0) {
			ans = -1;
		} 
		this.prevLocation = currentPosition;

		return ans;
	}

	public void boost(float astrength){
		transform.GetComponent<Rigidbody2D> ().velocity =  new Vector2(0f,astrength);
	}
		
}
