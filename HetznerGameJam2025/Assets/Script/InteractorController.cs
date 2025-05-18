using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InteractorController : MonoBehaviour
{

    //Bug still existing: All Gems combinable together and multiple Items in Hand


    public static GameObject HeldesItem = null;

    private ItemSpawnerController _spawnController;
    private WeightController _player;

    private Rigidbody _rb;
    private GameObject _currentItem;
    private GameObject _activeFireItem;
    private GameObject _activeFreezeItem;
    private GameObject _activeMushroomItem;
    private GameObject _activeHealItem;

    
    private bool _isInTrigger = false;
    private bool _isItemInHand = false;
    private bool _isCombinableGem1 = false;
    private bool _isCombinableGem2 = false;
    private bool _isCombinableGem3 = false;
    private bool _isCombinableGem4 = false;
    private bool _isFireItem = false;
    private bool _isFreezeItem = false;
    private bool _isMushroomItem = false;
    private bool _isHealItem = false;
    

    private string _heldItemTag = "";
    private string _targetItemTag = "";
    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<WeightController>();
        _spawnController = GameObject.Find("ItemSpawner").GetComponent<ItemSpawnerController>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInteractionInput();
        HandleItemInHand();
        HandleCombinationInput();
    }

    private void HandleInteractionInput()
    {
        if (_isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (HeldesItem != null)
            {
                Debug.Log("Spieler hat bereits ein Item in der Hand: " + HeldesItem.name);
                return;
            }

            Debug.Log("Pressed E while in trigger!");
            PickUpObject();
        }
    }

    private void HandleItemInHand()
    {
        if (!_isItemInHand) return;

        _player.InteractCanvas.SetActive(false);
        _player.LetGoCanvas.SetActive(true);

        _currentItem.transform.position = _player.Hand.transform.position;
        _isInTrigger = false;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReleaseObject();
        }
    }

    private void HandleCombinationInput()
    {
        if (_isCombinableGem1 && Input.GetKeyDown(KeyCode.E))
        {
            _player.CombineCanvas.SetActive(false);
            _player.LetGoCanvas.SetActive(false);
            Destroy(gameObject);

            _currentItem = null;
            _isItemInHand = false;
        }

        if (_isCombinableGem2 && Input.GetKeyDown(KeyCode.E))
        {
            _player.CombineCanvas.SetActive(false);
            _player.LetGoCanvas.SetActive(false);
            Destroy(gameObject);

            _currentItem = null;
            _isItemInHand = false;
        }

        if (_isCombinableGem3 && Input.GetKeyDown(KeyCode.E))
        {
            _player.CombineCanvas.SetActive(false);
            _player.LetGoCanvas.SetActive(false);
            Destroy(gameObject);

            _currentItem = null;
            _isItemInHand = false;
        }

        if (_isCombinableGem4 && Input.GetKeyDown(KeyCode.E))
        {
            _player.CombineCanvas.SetActive(false);
            _player.LetGoCanvas.SetActive(false);
            Destroy(gameObject);

            _currentItem = null;
            _isItemInHand = false;
        }

        if (_isFireItem && Input.GetKeyDown(KeyCode.E))
        {
            _player.CombineCanvas.SetActive(false);
            _player.LetGoCanvas.SetActive(false);
            Destroy(gameObject);
            _spawnController.spawnFirePotion = true;
            Debug.Log("Spawn FireItem! From: " + gameObject.name);

            _currentItem = _spawnController.FirePotion;
            _isItemInHand = true;
            _spawnController.FirePotion.GetComponent<Rigidbody>().useGravity = false;
        }

        if (_isFreezeItem && Input.GetKeyDown(KeyCode.E))
        {
            _player.CombineCanvas.SetActive(false);
            _player.LetGoCanvas.SetActive(false);
            Destroy(gameObject);
            _spawnController.spawnFreezePotion = true;
            Debug.Log("Spawn FreezeItem! From: " + gameObject.name);

            _currentItem = _spawnController.FreezePotion;
            _isItemInHand = true;
            _spawnController.FirePotion.GetComponent<Rigidbody>().useGravity = false;
        }

        if (_isMushroomItem && Input.GetKeyDown(KeyCode.E))
        {
            _player.CombineCanvas.SetActive(false);
            _player.LetGoCanvas.SetActive(false);
            Destroy(gameObject);
            _spawnController.spawnFreezePotion = true;
            Debug.Log("Spawn FreezeItem! From: " + gameObject.name);

            _currentItem = _spawnController.MushroomPotion;
            _isItemInHand = true;
            _spawnController.FirePotion.GetComponent<Rigidbody>().useGravity = false;
        }

        if (_isHealItem && Input.GetKeyDown(KeyCode.E))
        {
            _player.CombineCanvas.SetActive(false);
            _player.LetGoCanvas.SetActive(false);
            Destroy(gameObject);
            _spawnController.spawnFreezePotion = true;
            Debug.Log("Spawn FreezeItem! From: " + gameObject.name);

            _currentItem = _spawnController.HealPotion;
            _isItemInHand = true;
            _spawnController.FirePotion.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInTrigger = true;
            Debug.Log("Entered Triggerbox!");
            _player.InteractCanvas.SetActive(true);
            _currentItem = gameObject;
        }

        if (other.CompareTag("Gem1"))
        {
            _isCombinableGem1 = true;
            _player.CombineCanvas.SetActive(true);
            _player.LetGoCanvas.SetActive(false);
        }

        if (other.CompareTag("Gem2"))
        {
            _isCombinableGem2 = true;
            _player.CombineCanvas.SetActive(true);
            _player.LetGoCanvas.SetActive(false);
        }

        if (other.CompareTag("Gem3"))
        {
            _isCombinableGem3 = true;
            _player.CombineCanvas.SetActive(true);
            _player.LetGoCanvas.SetActive(false);
        }

        if (other.CompareTag("Gem4"))
        {
            _isCombinableGem4 = true;
            _player.CombineCanvas.SetActive(true);
            _player.LetGoCanvas.SetActive(false);
        }

        if (other.CompareTag("FireItem"))
        {
            if (_activeFireItem == null)
            {
                _activeFireItem = this.gameObject;
                _isFireItem = true;
                _player.CombineCanvas.SetActive(true);
                _player.LetGoCanvas.SetActive(false);
            }
        }

        if (other.CompareTag("FreezeItem"))
        {
            if (_activeFreezeItem == null)
            {
                _activeFreezeItem = this.gameObject;
                _isFreezeItem = true;
                _player.CombineCanvas.SetActive(true);
                _player.LetGoCanvas.SetActive(false);
            }
        }

        if (other.CompareTag("MushroomItem"))
        {
            if (_activeMushroomItem == null)
            {
                _activeMushroomItem = this.gameObject;
                _isMushroomItem = true;
                _player.CombineCanvas.SetActive(true);
                _player.LetGoCanvas.SetActive(false);
            }
        }

        if (other.CompareTag("HealItem"))
        {
            if (_activeHealItem == null)
            {
                _activeHealItem = this.gameObject;
                _isMushroomItem = true;
                _player.CombineCanvas.SetActive(true);
                _player.LetGoCanvas.SetActive(false);
            }
        }

        if (_isItemInHand)
        {
            _targetItemTag = other.tag;
            _player.CombineCanvas.SetActive(true);
            _player.LetGoCanvas.SetActive(false);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isInTrigger = false;
            Debug.Log("Exited Triggerbox!");
            _player.InteractCanvas.SetActive(false);
        }

        if (other.CompareTag("Gem1"))
        {
            _isCombinableGem1 = false;
            _player.CombineCanvas.SetActive(false);
            _player.LetGoCanvas.SetActive(true);
        }

        if (other.CompareTag("Gem2"))
        {
            _isCombinableGem2 = false;
            _player.CombineCanvas.SetActive(false);
            _player.LetGoCanvas.SetActive(true);
        }

        if (other.CompareTag("Gem3"))
        {
            _isCombinableGem3 = false;
            _player.CombineCanvas.SetActive(false);
            _player.LetGoCanvas.SetActive(true);
        }

        if (other.CompareTag("Gem4"))
        {
            _isCombinableGem4 = false;
            _player.CombineCanvas.SetActive(false);
            _player.LetGoCanvas.SetActive(true);
        }

        if (other.CompareTag("FireItem"))
        {
            if (_activeFireItem == this)
            {
                _activeFireItem = null;
                _isFireItem = false;
                _player.CombineCanvas.SetActive(false);
                _player.LetGoCanvas.SetActive(true);
            }
        }

        if (other.CompareTag("FreezeItem"))
        {
            if (_activeFreezeItem == null)
            {
                _activeFreezeItem = this.gameObject;
                _isFreezeItem = true;
                _player.CombineCanvas.SetActive(false);
                _player.LetGoCanvas.SetActive(true);
            }
        }

        if (other.CompareTag("MushroomItem"))
        {
            if (_activeMushroomItem == null)
            {
                _activeMushroomItem = this.gameObject;
                _isMushroomItem = true;
                _player.CombineCanvas.SetActive(false);
                _player.LetGoCanvas.SetActive(true);
            }
        }

        if (other.CompareTag("HealItem"))
        {
            if (_activeHealItem == null)
            {
                _activeHealItem = this.gameObject;
                _isMushroomItem = true;
                _player.CombineCanvas.SetActive(false);
                _player.LetGoCanvas.SetActive(true);
            }
        }
    }

    private void PickUpObject()
    {
        _currentItem.transform.parent = _player.Hand.transform;
        _rb.useGravity = false;
        _isItemInHand = true;

        HeldesItem = _currentItem;
        _heldItemTag = _currentItem.tag;
    }


    private void ReleaseObject()
    {
        _currentItem.transform.parent = null;
        _rb.useGravity = true;
        _isItemInHand = false;
        HeldesItem = null;

        _player.LetGoCanvas.SetActive(false);
        _currentItem = null;
        _heldItemTag = "";
    }

}
