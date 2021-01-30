using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public ChunkMap currentChunkMap;

    //public Transform CurrentNode;

	public StoreGenerator storeGenerator;
	public Transform NextNode;

	public float Speed;

	private void Awake()
	{
		storeGenerator = StoreGenerator.Singleton;
	}

	private void Start()
	{
		currentChunkMap = storeGenerator.GetChunkMapFromCoords(1, 1);
		
		NextNode = currentChunkMap.DirectionToBorder(0);

		int i = 0;
		while (NextNode == null)
		{
			i++;
			NextNode = currentChunkMap.DirectionToBorder(i);
			
			if(i > 3)
			{
				Debug.LogError("Well this is awkward.");
				return;
			}
		}
	}
	private void Update()
	{
		if( Vector3.Distance(transform.position,NextNode.position) < 1)
		{
			(ChunkMap,Transform) CT = currentChunkMap.GetNeighbour(NextNode);
			
			currentChunkMap = CT.Item1;
			NextNode = CT.Item2;
			if (NextNode == null)
			{
				Debug.LogWarning("shit");
			}
		} else
		{
			MoveTowards();
		}
	}

	private void MoveTowards()
	{
		Vector3 newPos = Vector3.MoveTowards(transform.position, NextNode.position, Speed * Time.deltaTime);
		Vector3 direction = newPos - transform.position;
		transform.localRotation = Quaternion.LookRotation(direction,Vector3.up);
		transform.position = Vector3.MoveTowards(transform.position, NextNode.position, Speed * Time.deltaTime);
	}
}
