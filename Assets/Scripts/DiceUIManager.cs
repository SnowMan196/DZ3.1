using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DiceUIManager : MonoBehaviour
{
    [Header("UI элементы")]
    [SerializeField] private TMP_InputField diceCountInput;
    [SerializeField] private TMP_InputField drawPointsInput;
    [SerializeField] private TMP_InputField winPointsInput;
    [SerializeField] private TMP_InputField losePointsInput;
    [SerializeField] private TMP_Text resultText;
    [SerializeField] private Button rollButton;

    public UnityEvent OnRollPressed;
    private DiceModel _model;

    public void Initialize(DiceModel model)
    {
        _model = model;
        _model.OnValuesChanged += UpdateUIFromModel;

        diceCountInput.onEndEdit.AddListener(OnDiceCountChanged);
        winPointsInput.onEndEdit.AddListener(OnWinPointsChanged);
        drawPointsInput.onEndEdit.AddListener(OnDrawPointsChanged);
        losePointsInput.onEndEdit.AddListener(OnLosePointsChanged);
        rollButton.onClick.AddListener(() => OnRollPressed?.Invoke());

        UpdateUIFromModel();
    }

    private void OnDiceCountChanged(string value)
    {
        if (int.TryParse(value, out var count))
            _model.DiceCount = count;
    }

    private void OnWinPointsChanged(string value)
    {
        if (int.TryParse(value, out var val))
            _model.WinPoints = val;
    }

    private void OnDrawPointsChanged(string value)
    {
        if (int.TryParse(value, out var val))
            _model.DrawPoints = val;
    }

    private void OnLosePointsChanged(string value)
    {
        if (int.TryParse(value, out var val))
            _model.LosePoints = val;
    }

    public void UpdateUIFromModel()
    {
        if (_model == null) return;
        diceCountInput.text = _model.DiceCount.ToString();
        drawPointsInput.text = _model.DrawPoints.ToString();
        winPointsInput.text = _model.WinPoints.ToString();
        losePointsInput.text = _model.LosePoints.ToString();
    }

    public void ShowResult(int rolledSum, DiceModel model)
    {
        string status;
        if (rolledSum >= model.WinPoints)
            status = "Победа!";
        else if (rolledSum >= model.DrawPoints)
            status = "Ничья";
        else
            status = "Поражение";

        resultText.text =
            $"Выпало: {rolledSum}\n" +
            $"Ничья: {model.DrawPoints}, Победа: {model.WinPoints}, Поражение: {model.LosePoints}\n" +
            $"Результат: {status}";
    }
}
