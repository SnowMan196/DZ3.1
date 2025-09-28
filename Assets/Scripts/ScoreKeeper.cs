using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    private int score;

    private void OnEnable() => FaceSensor.OnFaceDetected += AddScore;
    private void OnDisable() => FaceSensor.OnFaceDetected -= AddScore;

    private void AddScore(int value)
    {
        score += value;
        Debug.Log($"Счёт: {score}");
    }
}