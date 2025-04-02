using UnityEngine;

public class GrabGun : MonoBehaviour
{
    private bool isGrabbing = false;
    private GameObject grabbedObject;

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            if (!isGrabbing)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("gun"))
                {
                    grabbedObject = hit.collider.gameObject;
                    grabbedObject.transform.SetParent(transform);
                    grabbedObject.transform.localPosition = Vector3.zero;
                    grabbedObject.transform.localRotation = Quaternion.identity;

                    isGrabbing = true;
                }
            }
            else
            {
                grabbedObject.transform.SetParent(null);
                grabbedObject = null;

                isGrabbing = false;
            }
        }
    }
}
