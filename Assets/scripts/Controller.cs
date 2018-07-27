using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
	
	public float ypos = -75;
	public float zpos = 0f;
	public float speed = 250f;
	public float xMaxPos = 134.3f;
	public float xOffset = 9f;
	public GameObject ballPrefab;
	public GameObject attachedBall = null;
	private GameObject ballClone;
	public GameObject deadZone;
	public Rigidbody ballRigidbody;
	public float ballspeed = 4000f;
	public static Controller instance;


	
		// Use this for initialization
	void Start (){
		spawnBall ();
		instance = this; 
	}
	
	// Update is called once per frame
	void Update (){
		if (Input.GetAxis ("Horizontal") != 0) {
			transform.position = new Vector3 (transform.position.x + Input.GetAxis ("Horizontal") * speed * Time.deltaTime, ypos, zpos);
			
				if (transform.position.x < -xMaxPos + xOffset) {
					transform.position = new Vector3 (-xMaxPos + xOffset, ypos, zpos);
					} else if (transform.position.x > xMaxPos - xOffset) {
						transform.position = new Vector3 (xMaxPos - xOffset, ypos, zpos);
					}
				}
			if (attachedBall) {
				ballRigidbody = attachedBall.GetComponent<Rigidbody>();
				ballRigidbody.position = transform.position + new Vector3 (0f, 5.5f, 0f);

			if (Input.GetButtonDown ("Jump")) {
				ballRigidbody.isKinematic = false;
				ballRigidbody.AddForce (420, ballspeed, 0);
				ballClone = attachedBall;
				attachedBall = null;
			}
						

		}
	}

	void spawnBall ()
	{
		attachedBall = Instantiate (ballPrefab, transform.position + new Vector3 (0, 40, 0), Quaternion.identity) as GameObject;
	}


	void OnCollisionEnter(Collision col){
		foreach(ContactPoint contact in  col.contacts){
			if(contact.thisCollider == GetComponent<Collider>()){
				float ballangle = contact.point.x - transform.position.x;
				contact.otherCollider.GetComponent<Rigidbody>().AddForce(200*ballangle,300,0);
			}
		}

	}

	public void DestroyBall(){
		Destroy (ballClone);
	}

}

