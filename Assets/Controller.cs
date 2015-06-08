using UnityEngine;
using System.Collections;
using System;

public class Controller : MonoBehaviour
{

	public GameObject SwingPrefab;
	
	// Use this for initialization
	void Start()
	{
		StartCoroutine(controll());
	}

	const int N = 5, M = 6;
	//const int N = 2, M = 2;
	//const int N = 1, M = 1;
	//const int NUM_GENE = 32;

	GameObject[] Swings = new GameObject[N * M];
	uint[] Genes = new uint[N * M];

	IEnumerator controll()
	{
		yield return new WaitForSeconds(1f);
		makeNewGenes();
		float period = 4.02f;
		int gen = 0;
        for (; true; )
		{
			//Debug.Log("Generation : " + gen);
			makeNewSwings();
			yield return new WaitForSeconds(1.5f);
			for (int i = 0; i < N * M; i++) (Swings[i].GetComponent("Swing") as Swing).Human.SendMessage("StartPlayP", period+0.001f*gen);
//			for (int i = 0; i < N * M; i++) (Swings[i].GetComponent("Swing") as Swing).Human.SendMessage("StartPlay", Genes[i]);
			yield return new WaitForSeconds(1.0f + (period + 0.001f * gen) * 40);
			float[] Heights = new float[N * M];
			String log = "p:"+(period + 0.001f * gen);
			for (int i = 0; i < N * M; i++)
			{
				Heights[i] = (Swings[i].GetComponent("Swing") as Swing).getMaxHeight();
                log = log + "\n" + Heights[i];
			}
			Debug.Log(log);
			Array.Sort(Heights, Genes);
			RemoveSwings();
			gen++;
		//	UpdateGenes();
		}
	}

	void RemoveSwings()
	{
		for (int i = 0; i < N * M; i++)
		{
			Destroy(Swings[i]);
			Swings[i] = null;
		}
	}

	void makeNewSwings()
	{
		int c = 0;
		for (int i = 0; i < N; i++)
			for (int j = 0; j < M; j++)
			{
				GameObject swing = MonoBehaviour.Instantiate(SwingPrefab) as GameObject;
				swing.name = "swing" + c;
				swing.transform.localPosition = new Vector3(i * 15, 0, j * 15);
				Swings[c] = swing;
				c++;
			}
	}

	void makeNewGenes()
	{
		for (int i = 0; i < N * M; i++)
		{
//			Genes[i] = 0xFC000000u;
			Genes[i] = (((uint)UnityEngine.Random.Range(0, 0x10000)) << 16) + (uint)UnityEngine.Random.Range(0, 0x10000);
		}
	}

	void UpdateGenes()
	{
		uint[] newGenes = new uint[N * M];
		for (int i = 0; i < 10; i++) newGenes[i] = Genes[N * M - i - 1];
		for (int i = 10; i < 25; i++)
		{
			int x = UnityEngine.Random.Range(0, 10);
			int y = UnityEngine.Random.Range(0, 9);
			if (y >= x) y++;
			int t = UnityEngine.Random.Range(0, 31);
			for (int j = 0; j <= t; j++) newGenes[i] |= Genes[x] & (1u << j);
			for (int j = t + 1; j < 32; j++) newGenes[i] |= Genes[y] & (1u << j);
		}
		for (int i = 25; i < 30; i++)
		{
			newGenes[i] = newGenes[i - 25];
			int x = UnityEngine.Random.Range(0, 32);
			newGenes[i] ^= 1u << x;
		}
		Genes = newGenes;
	}

	// Update is called once per frame
	void Update()
	{
	}
}
