using UnityEngine;
using System.Collections;

public class HumanBehaviour : MonoBehaviour {

	protected ConfigurableJoint RightLap, LeftLap, RightElbow, LeftElbow, RightL, LeftL, RightS, LeftS, Spine;

	// Use this for initialization
	public void Start ()
	{
		RightLap = gameObject.transform.FindChild ("RightLeg").GetComponent<ConfigurableJoint> ();
		LeftLap = gameObject.transform.FindChild ("LeftLeg").GetComponent<ConfigurableJoint> ();
		LeftElbow = gameObject.transform.FindChild ("LeftArm").GetComponent<ConfigurableJoint> ();
		RightElbow = gameObject.transform.FindChild ("RightArm").GetComponent<ConfigurableJoint> ();
		RightL = gameObject.transform.FindChild ("RightHip").GetComponent<ConfigurableJoint> ();
		LeftL = gameObject.transform.FindChild ("LeftHip").GetComponent<ConfigurableJoint> ();
		LeftS = gameObject.transform.FindChild ("LeftShoulder").GetComponent<ConfigurableJoint> ();
		RightS = gameObject.transform.FindChild ("RightShoulder").GetComponent<ConfigurableJoint> ();
		Spine = gameObject.transform.FindChild ("Waist").GetComponent<ConfigurableJoint> ();

	}
	
}
