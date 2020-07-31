using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class RoadController : MonoBehaviour
{
    public GameObject[] roadsPrefabs;
    public GameObject lastRoad;
    public List<GameObject> roads = new List<GameObject>();

    private int shouldGetHardRoad = 5; // 0 - 10

    private void Start() {
        roads.Add(lastRoad);
    }

    public void spawnRoad() {
        GameObject nextPrefab = getPrefabs();
        GameObject nextRoad = PrefabUtility.InstantiatePrefab(
            nextPrefab
        ) as GameObject;

        Road road = nextRoad.GetComponent<Road>();

        shouldGetHardRoad = road.inDir == road.outDir ? shouldGetHardRoad + 1 : 0;

        nextRoad.transform.parent = transform;

        setNextPosition(nextRoad.gameObject);

        setNextOutDir(nextRoad.gameObject);

        tryFlip(nextRoad.gameObject);

        lastRoad = nextRoad;

        roads.Add(nextRoad);

        if (roads.Count > 3) {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
    }

    private GameObject getPrefabs() {
        GameObject[] prefabs = roadsPrefabs.Where(filterPrefab).ToArray();
        
        return prefabs[Random.Range(0, prefabs.Length)];
    }

    private bool filterPrefab(GameObject go) {
        Road road = go.GetComponent<Road>();
        
        if (road.inDir != road.outDir) {
            return Random.Range(1, 10) < shouldGetHardRoad; 
        }

        return true;
    }

    private void setNextPosition(GameObject nextRoad) {
        Road lastRoadComp = lastRoad.GetComponent<Road>();
        Vector3 lastOutDir = lastRoadComp.outDir;
        Vector3 lastBodyPos = lastRoad.transform.position;
        Vector3 lastBodySize = lastRoadComp.body.GetComponent<Collider>().bounds.size;
        Road nextRoadComp = nextRoad.GetComponent<Road>();
        Vector3 nextInDir = nextRoad.GetComponent<Road>().inDir;

        nextRoad.transform.position = lastBodyPos + lastOutDir * lastBodySize.z;

        if (lastOutDir != nextInDir) {
            nextRoad.transform.rotation = Quaternion.Euler(0, Quaternion.FromToRotation(nextInDir, lastOutDir).eulerAngles.y, 0);
        }
    }

    private void setNextOutDir(GameObject nextRoad) {
        Quaternion nextRot = nextRoad.transform.rotation;
        Road nextRoadComp = nextRoad.GetComponent<Road>();

        nextRoadComp.inDir = nextRot * nextRoadComp.inDir;
        nextRoadComp.outDir = nextRot * nextRoadComp.outDir;
    }

    private void tryFlip(GameObject nextRoad) {
        Road nextRoadComp = nextRoad.GetComponent<Road>();

        if (nextRoadComp.inDir != nextRoadComp.outDir && Random.Range(0, 10) > 5) {
            nextRoad.transform.rotation *= Quaternion.Euler(0, 90f, 0);
            nextRoadComp.outDir = nextRoadComp.outDir * -1;
        }
    }
}
