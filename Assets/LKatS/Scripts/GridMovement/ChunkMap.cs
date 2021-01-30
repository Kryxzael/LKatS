using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RNG = System.Random;

public class ChunkMap : MonoBehaviour
{
    public int WorldX, WorldZ;
    public int Rotation;

    
    public Transform NorthBorder;
    public Transform EastBorder;
    public Transform SouthBorder;
    public Transform WestBorder;

    private List<Transform> borders;

    //public List<Transform> Nodes;

    public List<Path> Paths;

    StoreGenerator storeGenerator;
	private void Awake()
	{

        borders = new List<Transform>() { NorthBorder,EastBorder,SouthBorder,WestBorder };

        storeGenerator = StoreGenerator.Singleton;
    }

	public (ChunkMap, Transform) GetNeighbour (Transform oldTransform)
	{
		RNG rand = new RNG();

        List<Path> possiblePaths = Paths.Where(x => x.One == oldTransform || x.Two == oldTransform).ToList();

        if (borders.Contains(oldTransform))
		{
            ChunkMap borderCM;
            Transform borderNode = null;

            switch ((BorderToDirection(oldTransform)))
            {
                case 0:
                    borderCM = storeGenerator.GetChunkMapFromCoords(WorldX, WorldZ + 1);
                    break;
                case 1:
                    borderCM = storeGenerator.GetChunkMapFromCoords(WorldX +1, WorldZ);
                    break;
                case 2:
                    borderCM = storeGenerator.GetChunkMapFromCoords(WorldX, WorldZ -1);
                    break;
                case 3:
                    borderCM = storeGenerator.GetChunkMapFromCoords(WorldX -1, WorldZ);
                    break;
                default:
                    borderCM = null;
                    break;
            }
            if( borderCM != null)
			{
                int direction = BorderToDirection(oldTransform);
                int borderCMRotation = borderCM.Rotation;
                int cmBorderInt = direction + 2 % 4;

                borderNode = borderCM.DirectionToBorder(cmBorderInt);

                if (borderNode != null)
				{
                    possiblePaths.Add(new Path(oldTransform, borderNode));

                }
            }





            if (possiblePaths.Count < 1)
            {
                Debug.LogError("Walker found no paths");
                Debug.Break();
                return (this, oldTransform);
            }
            Path path = possiblePaths[rand.Next(possiblePaths.Count)];
            Transform newTransform = path.One == oldTransform ? path.Two : path.One;

            if (borderNode == newTransform)
            {
                return (borderCM, newTransform);
            }
            else
            {
                return (this, newTransform);
            }
        } else
		{
            
            if(possiblePaths.Count < 1)
			{
                Debug.LogError("Walker found no paths" + possiblePaths);
                Debug.Break();
                return (this, oldTransform);
            }
            Path path = possiblePaths[rand.Next(possiblePaths.Count)];
            Transform newTransform = path.One == oldTransform?path.Two:path.One;

            return (this, newTransform);
		}
        
	}
    
    public int BorderToDirection(Transform t) {
        int x = Rotation;

        if (t == EastBorder)
        {
            x += 1;
        } else if (t == SouthBorder)
        {
            x += 2;
        } else 
        if (t == WestBorder)
        {
            x += 3;
        }
        return x% 4;
    }

    public Transform DirectionToBorder(int direction)
    {
		switch (direction)
		{
            case 0 :
                return NorthBorder;
            case 1:
                return EastBorder;
            case 2:
                return SouthBorder;
            case 3:
                return WestBorder;
            default:
                Debug.LogWarning("Shit 2");
                return null;
		}
	}
}
