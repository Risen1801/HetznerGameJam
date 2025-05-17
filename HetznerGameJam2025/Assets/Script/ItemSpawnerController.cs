using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ItemSpawnerController: MonoBehaviour
{
    public GameObject FirePotion;
    public GameObject FreezePotion;
    public GameObject MushroomPotion;
    public GameObject HealPotion;
    public bool spawnFirePotion;
    public bool spawnFreezePotion;
    public bool spawnMushroomPotion;
    public bool spawnHealPotion;

    private WeightController _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<WeightController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnFirePotion)
        {
            Instantiate(FirePotion, _player.Hand.transform.position, FirePotion.transform.rotation, _player.Hand.transform);
            spawnFirePotion = false;
        }

        if (spawnFreezePotion)
        {
            Instantiate(FirePotion, _player.Hand.transform.position, FirePotion.transform.rotation, _player.Hand.transform);
            spawnFreezePotion = false;
        }

        if (spawnMushroomPotion)
        {
            Instantiate(FirePotion, _player.Hand.transform.position, FirePotion.transform.rotation, _player.Hand.transform);
            spawnMushroomPotion = false;
        }

        if (spawnHealPotion)
        {
            Instantiate(FirePotion, _player.Hand.transform.position, FirePotion.transform.rotation, _player.Hand.transform);
            spawnHealPotion = false;
        }
    }
}
