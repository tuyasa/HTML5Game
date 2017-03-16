using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Cesar : MonoBehaviour
{

//	public float strength = 10f;
	public Collider2D loseCollider;
	//Horizontal speed;
	public float moveSpeed = 6f;
	public float maxLeft = -3.163f;
	public float maxRight = 3.163f;
	public GameObject levelManager;
	static public int score = 0;
	Animator animator;

	private Vector2 prevLocation;

	// Use this for initialization
	public float maxSpeed;
	public float minSpeed;
	public float currentSpeed;
	private bool started;

	private Vector2 currentVelocity;
	private Rigidbody2D rgbd;

	void Start ()
	{
		rgbd = GetComponent<Rigidbody2D>();
		prevLocation = transform.position;
		score = 0;
		started = false;
		animator = transform.GetChild (0).GetComponent<Animator>();
	}

	
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space) == true && transform.GetComponent<Rigidbody2D> ().isKinematic) {
			transform.GetComponent<Rigidbody2D> ().isKinematic = false;
			score = 0;
			levelManager.GetComponent<LevelManager> ().gameBegin ();
			this.boost (1f);
			this.animateJump ();
//			Debug.Log ("click");
			started = true;
		}
		if (Input.GetKey ("q") && transform.position.x > maxLeft) {
			transform.Translate (Vector2.left * moveSpeed * Time.deltaTime);
		}

		if (Input.GetKey ("d") && transform.position.x < maxRight) {
			transform.Translate (Vector2.right * moveSpeed * Time.deltaTime);
		}

	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider == loseCollider || collider.transform.tag.Equals ("Dislike")) {
			score = ((int)transform.position.y) * 10;		
			SceneManager.LoadScene ("Lose");
		}

		if (collider.GetComponent<Youtubeur> () != null) {			
			this.boost (collider.GetComponent<Youtubeur> ().boost);
		}
	}

	public int getDirection ()
	{ 
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

	public void boost (float astrength = 1f)
	{				
		//Calculate the next velocity accordingly to maxSpeed
		float  currentVelocity = rgbd.velocity.y;
		currentVelocity = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
		rgbd.velocity =  new Vector2(0f,currentVelocity * astrength);
		this.animateBoost ();
	}

	private void animateJump(){
		this.animator.SetBool ("jump", true);
	}

	public void animateFall(){
		this.animator.SetInteger("fall", 1);
	}

	public void animateBoost(){
		this.animator.SetInteger("fall", -1);
	}
		
}
