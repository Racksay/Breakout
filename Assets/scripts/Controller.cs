using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
	
		public float ypos = -75;
		public float zpos = 0f;
		public float speed = 250f;
		public float xboundary = 134.3f;
		public float maxboundary = 9f;
		public GameObject ballPrefab;
		public GameObject attachedBall = null;
		public Rigidbody ballRigidbody;
		public float ballspeed = 5000f;

	
		// Use this for initialization
		void Start ()
		{
				spawnBall ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetAxis ("Horizontal") != 0) {
						transform.position = new Vector3 (transform.position.x + Input.GetAxis ("Horizontal") * speed * Time.deltaTime, ypos, zpos);
			
						if (transform.position.x < -xboundary + maxboundary) {
								transform.position = new Vector3 (-xboundary + maxboundary, ypos, zpos);
						} else if (transform.position.x > xboundary - maxboundary) {
								transform.position = new Vector3 (xboundary - maxboundary, ypos, zpos);
						}
				}
				if (attachedBall) {
						ballRigidbody = attachedBall.rigidbody;
						ballRigidbody.position = transform.position + new Vector3 (0f, 5.5f, 0f);

						if (Input.GetButtonDown ("Jump")) {
								ballRigidbody.isKinematic = false;
								ballRigidbody.AddForce (420, ballspeed, 0);
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
			if(contact.thisCollider == collider){
				float ballangle = contact.point.x - transform.position.x;
				contact.otherCollider.rigidbody.AddForce(100*ballangle,200,0);
			}
		}

	}
}

