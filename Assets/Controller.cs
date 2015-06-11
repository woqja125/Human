using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

	public GameObject SwingPrefab;
	public Text FrameRate, Status;

	void Awake()
	{
		Application.targetFrameRate = 60;
	}

	private float accum = 0; // FPS accumulated over the interval
	private int frames = 0; // Frames drawn over the interval
	private float timeleft = 0.5f; // Left time for current interval

	void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		++frames;

		// Interval ended - update GUI text and start new interval
		if (timeleft <= 0.0)
		{
			// display two fractional digits (f2 format)
			float fps = accum / frames;
			string format = string.Format("{0:F2}", fps);
			FrameRate.text = format;

			timeleft = 0.5f;
			accum = 0.0F;
			frames = 0;
		}
	}

	// Use this for initialization
	void Start()
	{
		StartCoroutine(controll());
	}

	const int N = 5, M = 6;

	GameObject[] Swings = new GameObject[N * M];
	uint[] Genes = new uint[N * M];

	IEnumerator controll()
	{
		yield return new WaitForSeconds(5f);
		makeNewGenes();
		var file = File.CreateText("result.txt");
		int gen = 0;
		uint mgene=0;
		float max = 0;
        for (; true; )
		{
			Status.text = "Gen  " + gen + "\nMax  "+max+"\n"+ System.Convert.ToString(mgene, 2).PadLeft(32, '0');
			makeNewSwings();
			yield return new WaitForSeconds(1.5f);
			for (int i = 0; i < N * M; i++) (Swings[i].GetComponent("Swing") as Swing).Human.SendMessage("StartPlay", Genes[i]);
			yield return new WaitForSeconds(2.0f + 4.65f * 10);
			float[] Heights = new float[N * M];
			for (int i = 0; i < N * M; i++)
			{
				Heights[i] = (Swings[i].GetComponent("Swing") as Swing).getMaxHeight();
				if (Heights[i] > 20) goto loop_end;
			}
			System.Array.Sort(Heights, Genes);
			max = Heights[N * M - 1];
			mgene = Genes[N * M - 1];

			file.WriteLine("Generation {0}", gen);
			file.WriteLine("");
			for (int i = N*M-1; i >= 0; i--)
			{
				file.WriteLine("{0}\t{1}", System.Convert.ToString(Genes[i], 2).PadLeft(32, '0'), Heights[i]);
			}
			file.WriteLine();
			file.Flush();
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
		for (int i = 0; i < 4; i++) newGenes[i] = Genes[N * M - i - 1];
		for (int i = 4; i < 15; i++)
		{
			int x = Random.Range(0, 4);
			int y = Random.Range(0, 4);
			int t = Random.Range(0, 31);
			for (int j = 0; j < 32; j++)
			{
				if (j <= t) newGenes[i] |= newGenes[x] & (1u << j);
				else newGenes[i] |= newGenes[y] & (1u << j);
			}
		}
		for (int i = 15; i < 30; i++)
		{
			newGenes[i] = newGenes[Random.Range(0, 4)];
			for (int j = 0; j < 32; j++)
			{
				if (Random.Range(0, 15) == 0) newGenes[i] ^= 1u << j;
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
