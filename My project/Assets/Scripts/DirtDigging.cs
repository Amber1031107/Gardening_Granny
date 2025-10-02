using UnityEngine;

public class DirtDigging : MonoBehaviour
{
    [SerializeField] private GameObject[] DirtTiles;

    void Start()
    {
        
    }
    private void Interact()
    {
        Debug.Log("tileInteracted");
    }
}
