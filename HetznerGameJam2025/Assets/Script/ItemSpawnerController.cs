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
            Instantiate(FreezePotion, _player.Hand.transform.position, FreezePotion.transform.rotation, _player.Hand.transform);
            spawnFreezePotion = false;
        }

        if (spawnMushroomPotion)
        {
            Instantiate(MushroomPotion, _player.Hand.transform.position, MushroomPotion.transform.rotation, _player.Hand.transform);
            spawnMushroomPotion = false;
        }

        if (spawnHealPotion)
        {
            Instantiate(HealPotion, _player.Hand.transform.position, HealPotion.transform.rotation, _player.Hand.transform);
            spawnHealPotion = false;
        }
    }
}
