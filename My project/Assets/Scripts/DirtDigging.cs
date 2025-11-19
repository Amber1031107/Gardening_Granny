using UnityEngine;
using System.Collections;

public class DirtDigging : MonoBehaviour, IInteractable
{
    private Renderer rend;
    public Material Dirt;
    public Material Grass;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = Grass;
    }

    public void InteractLeftClick()
    {
        Debug.Log("tileInteracted");
        rend.material = Dirt;
    }
    public void InteractRightClick()
    {
        Debug.Log("tileInteracted");
        rend.material = Grass;
    }
}
