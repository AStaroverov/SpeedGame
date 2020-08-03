using UnityEngine;

public class SpawnBarrier : MonoBehaviour
{
    public GameObject[] barriers;
    
    void Start()
    {
        GameObject barrier = barriers[Random.Range(0, barriers.Length)];
        BoxCollider collider = GetComponent<BoxCollider>();
        Barrier barrierComp = barrier.GetComponent<Barrier>();

        float x = collider.transform.position.x;
        float z = collider.transform.position.z;
        float offsetX = collider.size.x / 2;
        float offsetZ = collider.size.z / 2;
        Quaternion minRot = barrierComp.minRotation;
        Quaternion maxRot = barrierComp.maxRotation;

        
        GameObject inst = Instantiate(
            barrier,
            new Vector3(
                Random.Range(x - offsetX, x + offsetX),
                collider.bounds.size.y,
                Random.Range(z - offsetZ, z + offsetZ)
            ),
            Quaternion.Euler(
                0,
                Random.Range(minRot.y, maxRot.y),
                0
            )
        );

        inst.transform.parent = transform;
    }
}
