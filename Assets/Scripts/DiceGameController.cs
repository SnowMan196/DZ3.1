using UnityEngine;
using UnityEngine.InputSystem; // обязательно нужно для InputAction

public class DiceGameController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DiceUIManager uiManager;
    [SerializeField] private DiceManager diceManager;

    private int totalResult;

    private void OnEnable()
    {
        uiManager.OnDiceCountChanged.AddListener(diceManager.UpdateDiceCount);
        uiManager.OnRollPressed.AddListener(RollDice);
        FaceSensor.OnFaceDetected += AddScore;
    }

    private void OnDisable()
    {
        uiManager.OnDiceCountChanged.RemoveListener(diceManager.UpdateDiceCount);
        uiManager.OnRollPressed.RemoveListener(RollDice);
        FaceSensor.OnFaceDetected -= AddScore;
    }

    // 🔹 Этот метод нужен для PlayerInput (вызов через Input System)
    public void OnRoll(InputAction.CallbackContext ctx)
    {
        // Выполнится только в момент нажатия
        if (!ctx.performed) return;

        RollDice();
    }

    // 🔹 Основной метод броска кубиков
    public void RollDice()
    {
        totalResult = 0;
        diceManager.RollAllDice();
        Debug.Log("🎲 Кубики брошены!");
    }

    // 🔹 Добавляем очки, когда FaceSensor сообщает верхнюю грань
    private void AddScore(int value)
    {
        totalResult += value;
        uiManager.ShowResult(totalResult);
    }
}