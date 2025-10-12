using UnityEngine;

public class DiceGameController : MonoBehaviour
{
    [Header("Ссылки на компоненты")]
    [SerializeField] private DiceUIManager uiManager;
    [SerializeField] private DiceManager diceManager;
    [SerializeField] private DiceModel model;

    private int _totalResult;

    private void OnEnable()
    {
        model.OnValuesChanged += UpdateFromModel;
        uiManager.OnRollPressed.AddListener(RollDice);
        FaceSensor.OnFaceDetected += AddScore;
    }

    private void OnDisable()
    {
        model.OnValuesChanged -= UpdateFromModel;
        uiManager.OnRollPressed.RemoveListener(RollDice);
        FaceSensor.OnFaceDetected -= AddScore;
    }

    private void Start()
    {
        uiManager.Initialize(model);
        UpdateFromModel();
    }

    private void UpdateFromModel()
    {
        diceManager.UpdateDiceCount(model.DiceCount);
        uiManager.UpdateUIFromModel();
    }

    private void RollDice()
    {
        _totalResult = 0;
        diceManager.ThrowDice();
    }

    private void AddScore(int value)
    {
        _totalResult += value;
        uiManager.ShowResult(_totalResult, model);
    }
}