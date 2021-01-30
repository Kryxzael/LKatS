using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using RNG = System.Random;

public class StoreGenerator : MonoBehaviour
{
    public static StoreGenerator Singleton;

    public StoreChunk[] Chunks = new StoreChunk[0];

    public Vector2 StoreSize = new Vector2(10, 10);
    public Vector2 ChunkSize = new Vector2(5f, 5f);

    public int Seed;
    public bool UseRandomSeed;
    
    private List<ChunkMap> chunkMaps = new List<ChunkMap>();

	private void Awake()
	{
		if (Singleton != null)
		{
            Debug.LogWarning("Singleton already instantiated, destroyed extra StoreGenerator");
            Destroy(this);
		}
		else
		{
            Singleton = this;
		}

        if (UseRandomSeed)
            Seed = UnityEngine.Random.Range(0, int.MaxValue);

        RNG rng = new RNG(Seed);

        for (int x = 0; x < StoreSize.x; x++)
        {
            for (int z = 0; z < StoreSize.y; z++)
            {
                rng.Next(4);
                int rotationInt = 0;//rng.Next(4);
                float rotation = rotationInt * 90f;

                ChunkMap newChunkMap = Instantiate(GetStoreChunk(rng), new Vector3(x * ChunkSize.x, 0f, z * ChunkSize.y), Quaternion.Euler(0f, rotation, 0f), transform).GetComponentInChildren<ChunkMap>();

                newChunkMap.WorldX = x;
                newChunkMap.WorldZ = z;
                newChunkMap.Rotation = rotationInt;
                chunkMaps.Add(newChunkMap);

            }
        }

    }

    public ChunkMap GetChunkMapFromCoords(int x, int z)
	{
        return chunkMaps.SingleOrDefault(c => c.WorldX == x && c.WorldZ == z);
	}
    private StoreChunk GetStoreChunk(RNG rng)
    {
        if (Chunks == null || !Chunks.Any() || Chunks.Sum(i => i.SpawnChance) == 0f)
            throw new InvalidOperationException("No chunks accessible. Store cannot be generated");

        StoreChunk candidate;

        do
        {
            candidate = Chunks[rng.Next(0, Chunks.Length)];
        } while (rng.NextDouble() > candidate.SpawnChance);

        return candidate;
    }
}
