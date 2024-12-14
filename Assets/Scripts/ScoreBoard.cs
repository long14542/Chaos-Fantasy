using System.Collections.Generic;
using System.IO;
using TMPro;

public class ScoreBoard
{
    //Khởi tạo file scoreboard khi play(trong SceneManager)
    private static ScoreBoard instance;

    private ScoreBoard() { }

    public static ScoreBoard Instance
    {
        get
        {
            if (instance == null)
                instance = new ScoreBoard();
            return instance;
        }
    }
    public void ResetScoreboard()
    {
        instance = new ScoreBoard();
    }
    //instance methods
    public int enemyKilled = 0;
    public TextMeshProUGUI timeScoreboard;
    public int lvPlayer = 0;
    public List<Inventory.Slot> weaponSlots = new();
    public List<Inventory.Slot> passiveSlots = new();
    public float totalDamage = 0;
    // Phương thức lưu dữ liệu vào file
    public void SaveToFile(string filePath)
    {
        
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine($"Enemy Killed: {enemyKilled}");
            writer.WriteLine($"Time Scoreboard: {timeScoreboard.text}");
            writer.WriteLine($"Player Level: {lvPlayer}");

            writer.WriteLine("Weapon Slots:");
            foreach (var weapon in weaponSlots)
            {
                if (weapon.item != null)
                {
                    writer.WriteLine($"- {weapon.item.itemName}");
                }
            }
            writer.WriteLine("Passive Slots:");
            foreach (var passive in passiveSlots)
            {
                if (passive.item != null)
                writer.WriteLine($"- {passive.item.itemName}");
            }

            writer.WriteLine($"Total Damage: {totalDamage}");
        }
    }
}
