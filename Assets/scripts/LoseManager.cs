using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoseManager : MonoBehaviour {

	public Text scoreText;
	// Use this for initialization
	void Start () {
		scoreText.text = "Your score anyway : " + Cesar.score.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void restart(){
		SceneManager.LoadScene ("prototype");
	}
	public void Menu(){
		SceneManager.LoadScene ("Menu");
	}
}
