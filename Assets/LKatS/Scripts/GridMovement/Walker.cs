using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    public ChunkMap currentChunkMap;

    //public Transform CurrentNode;

	public StoreGenerator storeGenerator;
	public Transform NextNode;
	public Transform PreviousNode;

	public float Speed;

	private void Awake()
	{
		storeGenerator = StoreGenerator.Singleton;
	}
	public void Setup(ChunkMap cm, Transform sp)
	{
		currentChunkMap = cm;
		NextNode = sp;
	}

	private void Update()
	{
		if (NextNode == null|| currentChunkMap == null) return;
		if( Vector3.Distance(transform.position,NextNode.position) < 1)
		{
			(ChunkMap,Transform) CT = currentChunkMap.GetNeighbour(NextNode, PreviousNode);
			PreviousNode = NextNode;
			
			currentChunkMap = CT.Item1;
			NextNode = CT.Item2;
			if (NextNode == null)
			{
				Debug.LogWarning("shit");
			}
		} 
		else
		{
			MoveTowards();
		}
	}

	private void MoveTowards()
	{
		Vector3 newPos = Vector3.MoveTowards(transform.position, NextNode.position, Speed * Time.deltaTime);
		Vector3 direction = newPos - transform.position;

		transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.LookRotation(direction.normalized, Vector3.up), 0.1f);
		transform.position = Vector3.MoveTowards(transform.position, NextNode.position, Speed * Time.deltaTime);
	}
}
