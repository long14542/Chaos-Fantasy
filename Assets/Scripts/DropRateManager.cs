using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drop
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }
    public List<Drop> drops;

    // Call this method whenever an enemy dies
    void OnDestroy()
    {
        float randNum = UnityEngine.Random.Range(0f, 100f);

        foreach (Drop item in drops)
        {
            if (randNum <= item.dropRate)
            {
                Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}
