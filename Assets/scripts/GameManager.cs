using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int life = 3;
	public int blocks = 100;
	public int score = 0;
	public int restartDelay = 3;
	public Text livesText;
	public Text scoreText;
	public GameObject gameOver;
	public GameObject youWon;
	public GameObject bricksPrefab;
	public GameObject paddle;
	private GameObject clonePaddle;
	public GameObject deathParticles;
	public static GameManager instance;
	public Rigidbody ballRigidbody;


	// Use this for initialization
	void Start () {
		instance = this;
		Setup ();
	}
	public void Setup(){
		clonePaddle = Instantiate (paddle, transform.position, Quaternion.identity) as GameObject;
	}
	void Restart(){
		Application.LoadLevel (1);
	}
	
	void WinOrLoseCheck(){
		if (blocks <= 0) {
			youWon.SetActive(true);
			Invoke("Restart", restartDelay);
		}
		if (life <= 0) {
			gameOver.SetActive(true);
			Invoke("Restart", restartDelay);
		}
	}

	
	public void LoseLife(){
		life--;
		livesText.text = "Lives: " + life;
		Destroy (clonePaddle);
		Controller.instance.DestroyBall ();
		Invoke ("SetupPaddle", 1);
		WinOrLoseCheck ();
	}
	
	void SetupPaddle(){
		clonePaddle = Instantiate (paddle, transform.position, Quaternion.identity) as GameObject;
	}
	
	public void OnBlockDestroyed(){
		blocks--;
		score++; 
		scoreText.text = "Score: " + score;
		WinOrLoseCheck ();
	}
	
}
