using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class RoadController : MonoBehaviour
{
    public GameObject[] roadsPrefabs;
    public GameObject lastRoad;
    public Transform lastOutput;
    public Vector3 lastOutputDir;
    public List<GameObject> roads = new List<GameObject>();

    private void Start() {
        roads.Add(lastRoad);
        // spawnRoad();
    }

    public void spawnRoad() {
        GameObject nextPrefab = getPrefab();
        GameObject nextRoad = PrefabUtility.InstantiatePrefab(nextPrefab) as GameObject;

        nextRoad.transform.parent = transform;

        setNextPosition(nextRoad.gameObject);

        lastRoad = nextRoad;

        roads.Add(nextRoad);

        if (roads.Count > 3) {
            Destroy(roads[0]);
            roads.RemoveAt(0);
        }
    }

    private GameObject getPrefab() {
        GameObject[] prefabs = roadsPrefabs.Where(filterPrefab).ToArray();

        if (prefabs.Length == 0) {
            return roadsPrefabs[Random.Range(0, roadsPrefabs.Length)];
        }
        
        return prefabs[Random.Range(0, prefabs.Length)];
    }

    private bool filterPrefab(GameObject go) {
        return go.GetComponent<Road>().frequency > Random.Range(0, 1);
    }

    private void setNextPosition(GameObject nextRoad) {
        Road nextRoadComp = nextRoad.GetComponent<Road>();
        int nextInputIndex = Random.Range(0, nextRoadComp.inputs.Length);
        int nextOutputIndex = nextInputIndex == 0 ? 1 : 0;
        Transform nextInput = nextRoadComp.inputs[nextInputIndex];
        Transform nextOutput = nextRoadComp.inputs[nextOutputIndex];
        Vector3 nextInputDir = getDirection(nextInput) * -1;
        Vector3 nextOutputDir = getDirection(nextOutput);

        if (lastOutputDir != nextInputDir) {
            nextRoad.transform.rotation =
                Quaternion.Euler(0, 360f - Quaternion.FromToRotation(lastOutputDir, nextInputDir).eulerAngles.y, 0);
        }

        nextRoad.transform.position =
            transform.TransformPoint(lastOutput.position) +
            transform.TransformPoint(nextInput.position) * -1;

        lastOutput = nextOutput;
        lastOutputDir = nextRoad.transform.rotation * nextOutputDir;
    }

    private Vector3 getDirection(Transform trans) {
        bool xOrZ = Mathf.Abs(trans.position.x) > Mathf.Abs(trans.position.z);
        Vector3 axle = xOrZ ? Vector3.right : Vector3.forward;

        if (xOrZ) {
            axle *= Mathf.Sign(trans.position.x);
        } else {
            axle *= Mathf.Sign(trans.position.z);
        }

        return axle;
    }
}
