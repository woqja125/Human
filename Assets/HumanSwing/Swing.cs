using UnityEngine;
using System.Collections;

public class Swing : MonoBehaviour {
	
	Rigidbody body;
	GameObject _human;
	public GameObject Human
	{
		get { return _human; }
	}

	public float getMaxHeight()
	{
		return maxHeight;
	}

	float maxHeight = 0;
	float startTime;

	// Use this for initialization
	void Start ()
	{
		body = gameObject.transform.FindChild ("Cube").GetComponent<Rigidbody> ();
		_human = gameObject.transform.FindChild("Human").gameObject;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (body.position.y >= maxHeight) maxHeight = body.position.y;
	}

}
