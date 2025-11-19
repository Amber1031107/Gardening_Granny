using UnityEngine;
using System.Collections;

interface IInteractable
{
    public void InteractLeftClick();
    public void InteractRightClick();
}

public class Interaction : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.InteractLeftClick();
                    Debug.Log("Interacted");
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.InteractRightClick();
                    Debug.Log("Interacted");
                }
            }
        }
    }

}
