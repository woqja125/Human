using UnityEngine;
using System.Collections;

public class HumanSwing : HumanBehaviour
{

	// Use this for initialization
	new void Start()
	{
		base.Start();
		up();
   //   down();
	}

	public void StartPlayP(float p)
	{
		period = p;
		gene = 0xFC000000u;
		StartCoroutine(playWithGene());
	}

	public void StartPlay(uint Gene)
	{
		gene = Gene;
		StartCoroutine(playWithGene());
	}

	const int NUM_GENE = 32;
	const int NUM_LOOP_CNT = 40;
	uint gene = 0;

	float period = 3.711f;

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

	void Update()
	{
    }

	void up()
	{
		Vector3 X = new Vector3(1, 0, 0);
		Quaternion Zero = Quaternion.AngleAxis(0, new Vector3(1, 0, 0));
		RightLap.targetRotation = Quaternion.AngleAxis(-10, X);
		LeftLap.targetRotation = Quaternion.AngleAxis(-10, X);
		//RightLap.targetRotation = Zero;
		//LeftLap.targetRotation = Zero;
		RightL.targetRotation = Zero;
		LeftL.targetRotation = Zero;

		RightElbow.targetRotation = Zero;
		LeftElbow.targetRotation = Zero;

		RightS.targetRotation = Zero;
		LeftS.targetRotation = Zero;
	}

	void down()
	{
        Vector3 X = new Vector3(1, 0, 0);
        RightLap.targetRotation = Quaternion.AngleAxis(150, X);
		LeftLap.targetRotation = Quaternion.AngleAxis(150, X);
		RightL.targetRotation = Quaternion.AngleAxis(165, X);
		LeftL.targetRotation = Quaternion.AngleAxis(165, X);

		RightElbow.targetRotation = Quaternion.AngleAxis(150, X);
		LeftElbow.targetRotation = Quaternion.AngleAxis(150, X);

		RightS.targetRotation = Quaternion.AngleAxis(150, X);
		LeftS.targetRotation = Quaternion.AngleAxis(150, X);
        //
	}

}
