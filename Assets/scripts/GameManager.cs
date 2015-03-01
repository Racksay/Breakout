using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int life = 3;
	public int blocks = 100;
	public int score = 0;
	public float resetDeleay = 1f;
	public Text livesText;
	public Text scoreText;
	public GameObject gameOver;
	public GameObject youWon;
	public GameObject bricksPrefab;
	public GameObject paddle;
	public GameObject deathParticles;
	public static GameManager instance = null;

	public Rigidbody ballRigidbody;



	private GameObject clonePaddle;
	
	
	
	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		
		
		Setup ();
	}
	public void Setup(){
		clonePaddle = Instantiate (paddle, transform.position, Quaternion.identity) as GameObject;
	}
	
	void CheckGameOver(){
		if (blocks < 1) {
			youWon.SetActive(true);
			Invoke("Reset", resetDeleay);
		}
		if (life < 1) {
			gameOver.SetActive(true);
			Invoke("Reset", resetDeleay);
		}
	}
	void Reset(){
		Application.LoadLevel (Application.loadedLevel);
	}
	
	public void LoseLife(){
		life--;
		livesText.text = "Lives: " + life;
		Instantiate (deathParticles, clonePaddle.transform.position, Quaternion.identity);
		Destroy (clonePaddle);
		Invoke ("SetupPaddle", resetDeleay);
		CheckGameOver ();
	}
	
	void SetupPaddle(){
		clonePaddle = Instantiate (paddle, transform.position, Quaternion.identity) as GameObject;
	}
	
	public void DestroyBrick(){
		blocks--;
		score++;
		scoreText.text = "Score: " + score;
		CheckGameOver ();
	}
	
	// Update is called once per frame
	void Update () {

		

	}

}
