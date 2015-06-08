using UnityEngine;
using System.Collections;

public class SwingScript : MonoBehaviour {

	Rigidbody body;

	// Use this for initialization
	void Start () {
		body = gameObject.transform.FindChild ("Cube").GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		body.AddForce (0, 0, 10);
	}
}
