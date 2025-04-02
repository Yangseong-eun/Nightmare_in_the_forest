using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandVisual : MonoBehaviour
{
    public XRController controller;
    public GameObject handPrefab;

    private XRInteractorLineVisual lineVisual;
    private XRInteractorReticleVisual reticleVisual;
    private GameObject handObject;

    private void Start()
    {
        lineVisual = controller.GetComponent<XRInteractorLineVisual>();
        reticleVisual = controller.GetComponent<XRInteractorReticleVisual>();

        // Disable the default XRInteractorLineVisual
        lineVisual.enabled = false;

        // Create the hand prefab
        handObject = Instantiate(handPrefab, transform);
        handObject.SetActive(false);
    }

    private void Update()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.triggerButton, out bool isTriggerPressed))
        {
            // Activate the hand object when the trigger button is pressed, deactivate it otherwise
            handObject.SetActive(isTriggerPressed);
        }

        // Update the hand's position and rotation
        handObject.transform.position = controller.transform.position;
        handObject.transform.rotation = controller.transform.rotation;

        // Disable the reticleVisual when the hand is active
        reticleVisual.enabled = !handObject.activeSelf;
    }
}
