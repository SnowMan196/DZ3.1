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

        // Проверяем, что кубик почти остановился
        if (rb.linearVelocity.magnitude < 0.05f && rb.angularVelocity.magnitude < 0.05f)
        {
            // Отладка: печатаем имя и тег грани
            Debug.Log($"Сработала грань: {gameObject.name}, тег: {gameObject.tag}");

            if (int.TryParse(gameObject.tag, out var bottomFace))
            {
                var top = 7 - bottomFace; // У кубика d6 противоположные грани в сумме = 7
                Debug.Log($"Верхняя грань = {top}");

                OnFaceDetected?.Invoke(top);

                // Чтобы грань не срабатывала много раз
                enabled = false;
            }
            else
            {
                Debug.LogWarning($"Тег {gameObject.tag} не число! Убедись, что Face_X имеет тег 1–6");
            }
        }
    }
}