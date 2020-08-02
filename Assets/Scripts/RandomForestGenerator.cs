
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForestGenerator : MonoBehaviour {

    public int elementSpacing = 3;
    public int grassSpacing = 1;

    public Element[] elements;
    public Element grass;

    private void Start() {
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 size = collider.size;
        Vector3 center = transform.position;

        for (float x = center.x - size.x / 2f; x <= center.x + size.x / 2f; x += elementSpacing) {
            for (float z = center.z - size.z / 2f; z <= center.z + size.z / 2f; z += elementSpacing) {

                // For each position, loop through each element...
                for (int i = 0; i < elements.Length; i++) {

                    if (grass.CanPlace()) {
                        Plant(grass.GetRandom(), x, z, transform);
                    }

                    // Get the current element.
                    Element element = elements[i];

                    // Check if the element can be placed.
                    if (element.CanPlace()) {
                        Plant(element.GetRandom(), x, z, transform);
                        break;
                    }

                }

            }
        }

    }

    private void Plant(GameObject obj, float x, float z, Transform parentTransform) {
        // Add random elements to element placement.
        Vector3 position = new Vector3(x, 0f, z);
        Vector3 offset = new Vector3(Random.Range(-0.75f, 0.75f), 0f, Random.Range(-0.75f, 0.75f));
        Vector3 rotation = new Vector3(Random.Range(0, 5f), Random.Range(0, 360f), Random.Range(0, 5f));
        Vector3 scale = Vector3.one * Random.Range(0.75f, 1.25f);

        // Instantiate and place element in world.
        GameObject newObject = Instantiate(obj);
        newObject.transform.SetParent(transform);
        newObject.transform.position = position + offset;
        newObject.transform.eulerAngles = rotation;
        newObject.transform.localScale = scale;
        newObject.transform.RotateAround(parentTransform.position, Vector3.up, parentTransform.rotation.eulerAngles.y);
    }

}

[System.Serializable]
public class Element {
    [Range(1, 10)]
    public int density;
    public GameObject[] prefabs;

    public bool CanPlace () {
        return Random.Range(0, 10) < density;
    }
    public GameObject GetRandom() {
        return prefabs[Random.Range(0, prefabs.Length)];
    }
}