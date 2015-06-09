using UnityEngine;
using System.Collections;

public class HumanSwing : HumanBehaviour
{

	// Use this for initialization
	new void Start()
	{
		base.Start();
		up();
	}

	public void StartPlayP(float p)
	{
		period = p;
		gene = 0xF8000000u;
		StartCoroutine(playWithGene());
	}

	public void StartPlay(uint Gene)
	{
		gene = Gene;
		StartCoroutine(playWithGene());
	}

	const int NUM_GENE = 32;
	const int NUM_LOOP_CNT = 8;
	uint gene = 0;

	float period = 4.65f;

	IEnumerator playWithGene()
	{
		for (int i = 1; i <= NUM_LOOP_CNT; i++ )
		{
			for(int j=0; j<NUM_GENE; j++)
			{
				if ( ((gene >> j)&1) == 1) up();
				else down();
				yield return new WaitForSeconds(period / NUM_GENE);
			}
		}
	}
	
	void up()
	{
		Vector3 X = new Vector3(1, 0, 0);
		RightLap.targetRotation = Quaternion.AngleAxis(-10, X);
		LeftLap.targetRotation = Quaternion.AngleAxis(-10, X);
		RightL.targetRotation = Quaternion.AngleAxis(-10, X);
		LeftL.targetRotation = Quaternion.AngleAxis(-10, X);

		RightElbow.targetRotation = Quaternion.AngleAxis(-10, X);
		LeftElbow.targetRotation = Quaternion.AngleAxis(-10, X);
		RightS.targetRotation = Quaternion.AngleAxis(-10, X);
		LeftS.targetRotation = Quaternion.AngleAxis(-10, X);
	}

	void down()
	{
        Vector3 X = new Vector3(1, 0, 0);
        RightLap.targetRotation = Quaternion.AngleAxis(180, X);
		LeftLap.targetRotation = Quaternion.AngleAxis(180, X);
		RightL.targetRotation = Quaternion.AngleAxis(180, X);
		LeftL.targetRotation = Quaternion.AngleAxis(180, X);

		RightElbow.targetRotation = Quaternion.AngleAxis(170, X);
		LeftElbow.targetRotation = Quaternion.AngleAxis(170, X);

		RightS.targetRotation = Quaternion.AngleAxis(170, X);
		LeftS.targetRotation = Quaternion.AngleAxis(170, X);
        //
	}

}
