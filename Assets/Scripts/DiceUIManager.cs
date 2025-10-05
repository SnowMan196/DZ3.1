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

    private int diceCount;
    private int drawPoints;
    private int winPoints;
    private int losePoints;

    private void Awake()
    {
        // 🔹 Инициализация случайных значений при запуске
        diceCount = 2;
        drawPoints = Random.Range(8, 13);
        winPoints = Random.Range(13, 19);
        losePoints = Random.Range(3, 8);

        // 🔹 Устанавливаем значения в поля ввода
        diceCountInput.text = diceCount.ToString();
        drawPointsInput.text = drawPoints.ToString();
        winPointsInput.text = winPoints.ToString();
        losePointsInput.text = losePoints.ToString();

        resultText.text = "Брось кубики 🎲";

        // 🔹 Подписываем события полей
        diceCountInput.onValueChanged.AddListener(OnDiceCountInputChanged);

        // Кнопку связываем с методом
        rollButton.onClick.AddListener(RollButtonClicked);
    }

    // Когда пользователь меняет число кубиков
    private void OnDiceCountInputChanged(string value)
    {
        if (int.TryParse(value, out int count))
        {
            diceCount = Mathf.Clamp(count, 1, 10);
            OnDiceCountChanged?.Invoke(diceCount);
        }
    }

    // 🔹 Этот метод появится в списке Unity Button → OnClick()
    public void RollButtonClicked()
    {
        Debug.Log("Кнопка 'Бросить кубики' нажата!");
        OnRollPressed?.Invoke();
    }

    // 🔹 Вывод результата в UI
    public void ShowResult(int rolledSum)
    {
        string status;
        if (rolledSum >= winPoints)
            status = "🏆 Победа!";
        else if (rolledSum >= drawPoints)
            status = "🤝 Ничья";
        else
            status = "💀 Поражение";

        resultText.text =
            $"🎲 Выпало: {rolledSum}\n" +
            $"⚖️ Ничья: {drawPoints}, 🏆 Победа: {winPoints}, 💀 Поражение: {losePoints}\n" +
            $"Результат: {status}";
    }
}
