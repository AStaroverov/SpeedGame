using UnityEngine;

public class RoadSpawn : MonoBehaviour
{
    private bool canSpawn = true;

    private void OnTriggerEnter(Collider collider) {
        if (canSpawn && collider.gameObject.tag == "Player") {
            transform.parent.parent.GetComponent<RoadController>().spawnRoad();
            canSpawn = false;
        }
    }
}
