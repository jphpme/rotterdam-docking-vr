using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Adds simple vertical locomotion so the user can "fly" using the right-hand thumbstick.
/// </summary>
[DisallowMultipleComponent]
public class OculusFlyLocomotion : MonoBehaviour
{
    [SerializeField]
    XROrigin m_XrOrigin;

    [SerializeField, Tooltip("Speed, in meters per second, used when climbing or descending.")]
    float m_VerticalSpeed = 2.5f;

    [SerializeField, Tooltip("Deadzone applied to the thumbstick so small movements are ignored.")]
    float m_DeadZone = 0.15f;

    CharacterController m_CharacterController;
    InputDevice m_RightHand;

    void Awake()
    {
        ResolveOrigin();

        if (m_XrOrigin != null)
        {
            m_CharacterController = m_XrOrigin.GetComponent<CharacterController>();

            // Disable gravity on the standard move provider so we can remain airborne while flying.
            var moveProvider = m_XrOrigin.GetComponent<ActionBasedContinuousMoveProvider>();
            if (moveProvider != null)
                moveProvider.useGravity = false;
        }
    }

    void OnEnable()
    {
        InputDevices.deviceConnected += OnDeviceConnected;
        TryInitializeDevices();
    }

    void OnDisable()
    {
        InputDevices.deviceConnected -= OnDeviceConnected;
    }

    void Update()
    {
        if (m_XrOrigin == null)
        {
            ResolveOrigin();
            if (m_XrOrigin == null)
                return;

            m_CharacterController = m_XrOrigin.GetComponent<CharacterController>();
        }

        if (!m_RightHand.isValid)
            TryInitializeDevices();

        if (!m_RightHand.isValid)
            return;

        if (!m_RightHand.TryGetFeatureValue(CommonUsages.primary2DAxis, out var climbAxis))
            m_RightHand.TryGetFeatureValue(CommonUsages.secondary2DAxis, out climbAxis);

        var climbInput = Mathf.Abs(climbAxis.y) > m_DeadZone ? climbAxis.y : 0f;
        if (Mathf.Approximately(climbInput, 0f))
            return;

        var displacement = Vector3.up * (climbInput * m_VerticalSpeed * Time.deltaTime);
        ApplyDisplacement(displacement);
    }

    void ApplyDisplacement(Vector3 displacement)
    {
        if (m_CharacterController != null && m_CharacterController.enabled)
        {
            m_CharacterController.Move(displacement);
        }
        else if (m_XrOrigin != null)
        {
            m_XrOrigin.transform.position += displacement;
        }
    }

    void TryInitializeDevices()
    {
        m_RightHand = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    void OnDeviceConnected(InputDevice device)
    {
        if ((device.characteristics & InputDeviceCharacteristics.Controller) == 0)
            return;

        if ((device.characteristics & InputDeviceCharacteristics.Right) == 0)
            return;

        m_RightHand = device;
    }

    void ResolveOrigin()
    {
        if (m_XrOrigin != null)
            return;

        m_XrOrigin = GetComponent<XROrigin>();
        if (m_XrOrigin == null)
            m_XrOrigin = FindObjectOfType<XROrigin>();

        if (m_XrOrigin != null && m_CharacterController == null)
            m_CharacterController = m_XrOrigin.GetComponent<CharacterController>();
    }
}
