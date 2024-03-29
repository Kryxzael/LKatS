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

	public (ChunkMap, Transform) GetNeighbour(Transform currentTransform, Transform previousTransform)
	{
		RNG rand = new RNG();

        List<Path> possiblePaths = Paths.Where(x => x.One == currentTransform || x.Two == currentTransform).ToList();

        if (borders.Contains(currentTransform))
		{
            ChunkMap borderCM;
            Transform borderNode = null;

            switch ((BorderToDirection(currentTransform)))
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
                int direction = BorderToDirection(currentTransform);

                int cmBorderInt = ( (borderCM.Rotation +direction + 2)) % 4;

                borderNode = borderCM.DirectionToBorder(cmBorderInt);

                if (borderNode != null)
				{
                    possiblePaths.Add(new Path(currentTransform, borderNode));

                }
            }

            if (possiblePaths.Count < 1)
            {
                Debug.LogError("Walker found no paths", currentTransform);
                Debug.Break();
                return (this, currentTransform);
            }

            if (possiblePaths.Count > 1)
            {
                possiblePaths.RemoveAll(i => i.One == previousTransform || i.Two == previousTransform);
            }

            Path path = possiblePaths[rand.Next(possiblePaths.Count)];
            Transform newTransform = path.One == currentTransform ? path.Two : path.One;

            if (borderNode == newTransform)
            {
                return (borderCM, newTransform);
            }
            else
            {
                return (this, newTransform);
            }
        } 
        else
		{
            
            if(possiblePaths.Count < 1)
			{
                Debug.LogError("Walker found no paths" + possiblePaths);
                Debug.Break();
                return (this, currentTransform);
            }
            if (possiblePaths.Count > 1)
            {
                possiblePaths.RemoveAll(i => i.One == previousTransform || i.Two == previousTransform);
            }

            Path path = possiblePaths[rand.Next(possiblePaths.Count)];
            Transform newTransform = path.One == currentTransform?path.Two:path.One;

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

    private void OnDrawGizmos()
    {
        foreach (var i in Paths)
        {
            Gizmos.DrawWireSphere(i.One.position + (Vector3.up * 1f),4f);
            Gizmos.DrawWireSphere(i.Two.position + (Vector3.up * 1f), 4f);
            Gizmos.DrawLine(i.One.position + (Vector3.up * 1f), i.Two.position + (Vector3.up * 1f));
        }
    }
}
