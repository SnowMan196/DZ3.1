using UnityEngine;

public class DiceSpawner : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private DiceManager diceManager;

    private void Start()
    {
        foreach (var point in spawnPoints)
        {
            var dice = Instantiate(dicePrefab, point.position, Quaternion.identity);
            diceManager.RegisterDice(dice);
        }
    }
}