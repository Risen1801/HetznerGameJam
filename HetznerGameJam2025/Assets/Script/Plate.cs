using UnityEngine;

public class Plate : MonoBehaviour
{
    public GameObject forest;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("MushroomPotion"))
        {
            forest.SetActive(true);
        }
    }
}
