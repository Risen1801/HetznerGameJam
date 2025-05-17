using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] possiblePrefabs;
    [SerializeField] float initialCooldownMin = 2;
    [SerializeField] float initialCooldownMax = 5;

    float nextSpawnTime = float.MaxValue;
    BoxCollider collider;

    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(initialCooldownMin, initialCooldownMax);
        collider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (nextSpawnTime <= Time.time)
        {
            int randomprefabIndex = Random.Range(0, possiblePrefabs.Length);
            float width = collider.bounds.size.x;
            float randomPostition = transform.position.x + Random.Range(-width / 2, width / 2);
            Instantiate(possiblePrefabs[randomprefabIndex], new Vector3(randomPostition, transform.position.y, 0), possiblePrefabs[randomprefabIndex].transform.rotation);
            nextSpawnTime = Time.time + Random.Range(initialCooldownMin, initialCooldownMax);

        }
    }
}
