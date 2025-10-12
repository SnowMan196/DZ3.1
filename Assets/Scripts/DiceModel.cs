using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DiceModel", menuName = "Game/Dice Model")]
public class DiceModel : ScriptableObject
{
    [SerializeField, Range(1, 10)] private int diceCount = 2;
    [SerializeField] private int drawPoints = 8;
    [SerializeField] private int winPoints = 13;
    [SerializeField] private int losePoints = 4;

    public event Action OnValuesChanged;

    public int DiceCount
    {
        get => diceCount;
        set
        {
            diceCount = Mathf.Clamp(value, 1, 10);
            OnValuesChanged?.Invoke();
        }
    }

    public int DrawPoints
    {
        get => drawPoints;
        set { drawPoints = value; OnValuesChanged?.Invoke(); }
    }

    public int WinPoints
    {
        get => winPoints;
        set { winPoints = value; OnValuesChanged?.Invoke(); }
    }

    public int LosePoints
    {
        get => losePoints;
        set { losePoints = value; OnValuesChanged?.Invoke(); }
    }
}