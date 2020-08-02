using UnityEngine;

public class SpawnBarrier : MonoBehaviour
{
    public GameObject[] barriers;
    
    void Start()
    {
        GameObject barrier = barriers[Random.Range(0, barriers.Length)];
        Collider collider = GetComponent<Collider>();

        float x = collider.transform.position.x;
        float z = collider.transform.position.z;
        float offsetX = collider.bounds.size.x / 2;
        float offsetY = collider.bounds.size.y / 2;
        float offsetZ = collider.bounds.size.z / 2;
        Quaternion minRot = barrier.GetComponent<Barrier>().minRotation;
        Quaternion maxRot = barrier.GetComponent<Barrier>().maxRotation;

        
        GameObject inst = Instantiate(
            barrier,
            new Vector3(
                Random.Range(x - offsetX, x + offsetX),
                offsetY + 0.1f,
                Random.Range(z - offsetZ, z + offsetZ)
            ),
            Quaternion.Euler(
                0,
                Random.Range(minRot.y, maxRot.y),
                Random.Range(minRot.z, maxRot.z)
            )
        );

        inst.transform.parent = transform;
    }
}
