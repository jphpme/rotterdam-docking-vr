using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


/// <summary>
/// Ensures an XR rig with basic locomotion is present.
/// Drop this on an empty GameObject in the scene, then Play once.
/// </summary>
public class LocomotionInstaller : MonoBehaviour
{
    void Awake()
    {
        var rig = FindObjectOfType<XROrigin>();
        if (!rig)
        {
            var go = new GameObject("XR Origin");
            rig = go.AddComponent<XROrigin>();
            go.AddComponent<ActionBasedContinuousMoveProvider>();
            go.AddComponent<ActionBasedContinuousTurnProvider>();
        }


        var sys = FindObjectOfType<XRInteractionManager>();
        if (!sys)
        {
            new GameObject("XR Interaction Manager").AddComponent<XRInteractionManager>();
        }

        var moveProvider = rig.GetComponent<ActionBasedContinuousMoveProvider>();
        if (moveProvider)
        {
            moveProvider.useGravity = false;
        }

        if (!rig.GetComponent<OculusFlyLocomotion>())
        {
            rig.gameObject.AddComponent<OculusFlyLocomotion>();
        }
    }
}