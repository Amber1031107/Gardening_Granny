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


        if (Shop.shopIsOpen)
            return;

        if (Input.GetMouseButtonDown(0))
        {
                      
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                Debug.Log($"[Interaction] Hit: {hitInfo.collider.gameObject.name} on {hitInfo.collider.gameObject.layer}");
                IInteractable interactObj = hitInfo.collider.GetComponentInParent<IInteractable>();
                if (interactObj != null)
                    interactObj.InteractLeftClick();
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
                     
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                Debug.Log($"[Interaction] Hit: {hitInfo.collider.gameObject.name} on {hitInfo.collider.gameObject.layer}");
                IInteractable interactObj = hitInfo.collider.GetComponentInParent<IInteractable>();
                if (interactObj != null)
                    interactObj.InteractRightClick();
            }
        }
    }

}
