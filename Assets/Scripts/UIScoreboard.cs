using TMPro;
using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{
    public TextMeshProUGUI enemyKilledText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI playerLevelText;
    public TextMeshProUGUI totalDamageText;

    public GameObject gameOverPanel;

    public void ShowGameOverScreen()
    {
        // Gán dữ liệu từ ScoreBoard
        var scoreboard = ScoreBoard.Instance;
        enemyKilledText.text = $"Enemies Killed: {scoreboard.enemyKilled}";
        timeText.text = $"Time: {scoreboard.timeScoreboard.text}";
        playerLevelText.text = $"Player Level: {scoreboard.lvPlayer}";
        totalDamageText.text = $"Total Damage: {scoreboard.totalDamage}";

        // Hiển thị bảng Game Over
        gameOverPanel.SetActive(true);
    }
}
