using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class lvlpillarspawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    
    public float spawnInterval = 0.4f;
    public float width;
    public float moveDown;
    public Transform parent;

    public int dir = 1;

    private void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        int spawnCount = 0;
        while (spawnCount < 20)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnObject(spawnCount);
            spawnCount++;
            yield return new WaitForSeconds(spawnInterval);
            SpawnObject(spawnCount);
            spawnCount++;
            dir *= -1;
        }
    }

    void SpawnObject(int currentLevel)
    {
        GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation, parent);
        transform.Translate(new Vector3(width / 2 * dir, -moveDown, 0));
        foreach (Transform t in spawnedObject.transform)
        {
            foreach (Transform t2 in t.transform)
            {
                if(t2.GetComponent<Button>() != null)
                {
                    t2.name = currentLevel.ToString();
                }
                TextMeshProUGUI textMeshPro = t2.GetComponentInChildren<TextMeshProUGUI>();
                if (textMeshPro != null)
                {
                    // Change the text of the TextMeshPro component
                    textMeshPro.text = currentLevel.ToString();
                }
            }
            
        }
    }
}
