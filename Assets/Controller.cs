using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{

	public GameObject SwingPrefab;
	
	// Use this for initialization
	void Start()
	{
		StartCoroutine(controll());
	}

	const int N = 5, M = 6;
//	const int N = 2, M = 2;
	//const int N = 1, M = 1;
	//const int NUM_GENE = 32;

	GameObject[] Swings = new GameObject[N * M];
	uint[] Genes = new uint[N * M];

	IEnumerator controll()
	{
		yield return new WaitForSeconds(1f);
		makeNewGenes();
		//float period = 4.65f;
		int gen = 0;
        for (; true; )
		{
			makeNewSwings();
			yield return new WaitForSeconds(1.5f);
			for (int i = 0; i < N * M; i++) (Swings[i].GetComponent("Swing") as Swing).Human.SendMessage("StartPlay", Genes[i]);
			yield return new WaitForSeconds(2.0f + 4.65f * 8);
			float[] Heights = new float[N * M];
			for (int i = 0; i < N * M; i++)
			{
				Heights[i] = (Swings[i].GetComponent("Swing") as Swing).getMaxHeight();
				if (Heights[i] > 10) goto loop_end;
			}
			System.Array.Sort(Heights, Genes);
			StringBuilder builder = new StringBuilder();
			builder.AppendFormat("\nGeneration {0}\n\n", gen);
			for (int i = N*M-1; i >= 0; i--)
			{
				builder.AppendFormat("{0}\t{1}\n", System.Convert.ToString(Genes[i], 2).PadLeft(32, '0'), Heights[i]);
			}
			Debug.Log(builder.ToString());
			gen++;
			UpdateGenes();
			loop_end:
			RemoveSwings();
			yield return new WaitForSeconds(1f);
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
			Genes[i] = (((uint)Random.Range(0, 0x10000)) << 16) + (uint)Random.Range(0, 0x10000);
		}
	}

	void UpdateGenes()
	{
		uint[] newGenes = new uint[N * M];
		for (int i = 0; i < 10; i++) newGenes[i] = Genes[N * M - i - 1];
		for (int i = 10; i < 20; i++)
		{
			int x = Random.Range(0, 10);
			int y = Random.Range(0, 9);
			if (y >= x) y++;
			int t = Random.Range(0, 31);
			for (int j = 0; j < 32; j++)
			{
				if (j <= t) newGenes[i] |= newGenes[x] & (1u << j);
				else newGenes[i] |= newGenes[y] & (1u << j);
			}
		}
		for (int i = 20; i < 30; i++)
		{
			newGenes[i] = newGenes[i - 20];
			for (int j = 0; j < 32; j++)
			{
				if (Random.Range(0, 20) == 0) newGenes[i] ^= 1u << j;
			}
		}
		HashSet<uint> set = new HashSet<uint>(newGenes);
		while(set.Count < 30)
		{
			int x = Random.Range(0, 10);
			int t = Random.Range(0, 32);
			set.Add(newGenes[x] ^ (1u << t));
		}
		set.CopyTo(Genes);
	}
	
}
