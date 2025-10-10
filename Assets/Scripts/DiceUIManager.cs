using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DiceUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_InputField diceCountInput;
    [SerializeField] private TMP_InputField drawPointsInput;
    [SerializeField] private TMP_InputField winPointsInput;
    [SerializeField] private TMP_InputField losePointsInput;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Button rollButton;

    [Header("Events")]
    public UnityEvent<int> OnDiceCountChanged;
    public UnityEvent OnRollPressed;

    private int drawPoints;
    private int winPoints;
    private int losePoints;

    private void Awake()
    {
        drawPoints = Random.Range(8, 13);
        winPoints = Random.Range(13, 19);
        losePoints = Random.Range(3, 8);

        drawPointsInput.text = drawPoints.ToString();
        winPointsInput.text = winPoints.ToString();
        losePointsInput.text = losePoints.ToString();

        resultText.text = " Введите количество и бросьте кубики!";

        diceCountInput.onValueChanged.AddListener(OnDiceCountInputChanged);
        rollButton.onClick.AddListener(RollButtonClicked);
    }

    private void OnDiceCountInputChanged(string value)
    {
        if (int.TryParse(value, out var count))
            OnDiceCountChanged?.Invoke(Mathf.Clamp(count, 1, 10));
    }

    private void RollButtonClicked()
    {
        OnRollPressed?.Invoke();
    }

    public void ShowResult(int rolledSum)
    {
        var status = rolledSum >= winPoints ? " Победа!" :
            rolledSum >= drawPoints ? "⚖ Ничья" : " Поражение";

        resultText.text =
            $" Выпало: {rolledSum}\n" +
            $" Ничья: {drawPoints},  Победа: {winPoints},  Поражение: {losePoints}\n" +
            $"Результат: {status}";
    }
}