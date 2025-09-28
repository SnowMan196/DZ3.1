using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class DiceManager : MonoBehaviour
{
    [SerializeField] private float minForce = 4f;
    [SerializeField] private float maxForce = 8f;
    [SerializeField] private float maxTorque = 6f;

    // Список Rigidbody всех кубиков
    private readonly List<Rigidbody> diceList = new();

    // Регистрируем кубик
    public void RegisterDice(GameObject dice)
    {
        if (dice.TryGetComponent<Rigidbody>(out var rb))
        {
            diceList.Add(rb);
        }
        else
        {
            Debug.LogWarning($"{dice.name} не имеет Rigidbody!");
        }
    }

    // Метод для броска (подключается к InputSystem)
    public void OnRoll(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        foreach (var rb in diceList)
        {
            // Сбрасываем старое движение
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Рандомная сила
            var dir = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
            var force = dir * Random.Range(minForce, maxForce);
            rb.AddForce(force, ForceMode.Impulse);

            // Рандомный крутящий момент
            var torque = new Vector3(
                Random.Range(-maxTorque, maxTorque),
                Random.Range(-maxTorque, maxTorque),
                Random.Range(-maxTorque, maxTorque)
            );
            rb.AddTorque(torque, ForceMode.Impulse);
        }
    }
}