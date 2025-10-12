using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceManager : MonoBehaviour
{
    [Header("Параметры броска")]
    [SerializeField] private float minForce = 4f;
    [SerializeField] private float maxForce = 8f;
    [SerializeField] private float maxTorque = 6f;

    [Header("Спавн кубиков")]
    [SerializeField] private GameObject dicePrefab;
    [SerializeField] private Transform spawnArea;
    [SerializeField, Range(1, 10)] private int diceCount = 1;

    private readonly List<Rigidbody> _diceList = new();

    public void UpdateDiceCount(int newCount)
    {
        newCount = Mathf.Clamp(newCount, 1, 10);
        diceCount = newCount;
        RebuildDice();
    }

    private void RebuildDice()
    {
        foreach (var rb in _diceList)
        {
            if (rb != null)
                Destroy(rb.gameObject);
        }
        _diceList.Clear();

        for (var i = 0; i < diceCount; i++)
        {
            var pos = spawnArea != null
                ? spawnArea.position + new Vector3(i * 2f, 0, 0)
                : new Vector3(i * 2f, 1f, 0);

            var dice = Instantiate(dicePrefab, pos, Random.rotation);
            if (dice.TryGetComponent<Rigidbody>(out var rb))
                _diceList.Add(rb);
        }
    }

    public void ThrowDice()
    {
        foreach (var rb in _diceList)
        {
            if (rb == null) continue;

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            var dir = new Vector3(Random.Range(-1f, 1f), 1f, Random.Range(-1f, 1f)).normalized;
            var force = dir * Random.Range(minForce, maxForce);
            rb.AddForce(force, ForceMode.Impulse);

            var torque = new Vector3(
                Random.Range(-maxTorque, maxTorque),
                Random.Range(-maxTorque, maxTorque),
                Random.Range(-maxTorque, maxTorque)
            );
            rb.AddTorque(torque, ForceMode.Impulse);
        }
    }
}
