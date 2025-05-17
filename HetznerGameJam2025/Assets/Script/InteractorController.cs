using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class InteractorController : MonoBehaviour
{
    private WeightController _player;
    private bool _isInTrigger = false;
    private bool _isItemInHand = false;
    private GameObject _currentItem;
    private Rigidbody _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<WeightController>();
    }

    private void Update()
    {
        if (_isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E while in trigger!");
            ObjectPickedUp();
        }

        if(_isItemInHand)
        {

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInTrigger = true;
            Debug.Log("Entered Triggerbox!");
            _player.InteractCanvas.SetActive(true);
            _currentItem = this.gameObject;
        }
    }

    void ObjectPickedUp()
    {
        _rb = GetComponent<Rigidbody>();
        _currentItem.transform.parent = _player.Hand.transform;

        _rb.useGravity = false;
        _isItemInHand = true;
    }
}
