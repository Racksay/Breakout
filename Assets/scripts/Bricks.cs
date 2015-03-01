using UnityEngine;
using System.Collections;

public class Bricks : MonoBehaviour {

	public GameObject brickParticle;
	private GameObject particle = null;

	void OnCollisionEnter(Collision other){
		particle = (GameObject) Instantiate (brickParticle, transform.position, Quaternion.identity);
		GameManager.instance.OnBlockDestroyed ();
		Destroy (gameObject);
		Destroy (particle, 5f);
	}

}
