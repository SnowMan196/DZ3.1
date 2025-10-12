using System;
using UnityEngine;

public class FaceSensor : MonoBehaviour
{
    public static event Action<int> OnFaceDetected;

    private void OnTriggerStay(Collider other)
    {
        var parent = transform.parent;
        if (parent == null) return;
        if (!parent.TryGetComponent<Rigidbody>(out var rb)) return;

        if (rb.linearVelocity.magnitude < 0.05f && rb.angularVelocity.magnitude < 0.05f)
        {
            if (int.TryParse(gameObject.tag, out var bottom))
            {
                var top = 7 - bottom;
                OnFaceDetected?.Invoke(top);
                enabled = false;
            }
        }
    }
}