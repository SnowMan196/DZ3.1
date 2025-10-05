using UnityEngine;

public class DiceGameController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DiceUIManager uiManager;
    [SerializeField] private DiceManager diceManager;

    private int currentDiceCount = 2;
    private int totalResult;

    private void OnEnable()
    {
        // подписки на события из UI
        uiManager.OnDiceCountChanged.AddListener(UpdateDiceCount);
        uiManager.OnRollPressed.AddListener(RollDice);
        // подписка на событие из FaceSensor
        FaceSensor.OnFaceDetected += AddScore;
    }

    private void OnDisable()
    {
        uiManager.OnDiceCountChanged.RemoveListener(UpdateDiceCount);
        uiManager.OnRollPressed.RemoveListener(RollDice);
        FaceSensor.OnFaceDetected -= AddScore;
    }

    private void UpdateDiceCount(int count)
    {
        currentDiceCount = count;
    }

    private void RollDice()
    {
        totalResult = 0;
        // сбрасываем очки и кидаем кубики
        Debug.Log($"Бросаем {currentDiceCount} кубиков");
        diceManager.OnRoll(new UnityEngine.InputSystem.InputAction.CallbackContext());
    }

    private void AddScore(int value)
    {
        totalResult += value;
        uiManager.ShowResult(totalResult);
    }
}