using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public CharacterScriptableObject characterData;

    // Current character stats
    private float currentMoveSpeed;
    private float currentHealth;
    private float currentRecovery;
    private float currentMight;
    private float currentProjectileSpeed;

    // Experience variables
    public int exp = 0;
    public int level = 1;
    public int expCap;

    // Class LevelRange will determine how much will the exp required to level up increase in certain level ranges
    // Ex: Level 1 -> 4: each time you level up the exp required is increased by 2
    //     Level 5 -> 10: each time you level up the exp required is increased by 4
    // Use System.Serializable to expose the class's variables in the inspector
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int expCapIncrease;
    }
    // A list of LevelRange to set level ranges, exp increase in each level range
    public List<LevelRange> levelRanges;

    void Awake()
    {
        // Assign current stats to the starting stats
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMoveSpeed = characterData.MoveSpeed;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }
    // Start is called before the first frame update
    void Start()
    {
        // Initialize expCap as the first expCapIncrease so the character can level up immediately
        expCap = levelRanges[0].expCapIncrease;
    }

    // Increase exp on pick up exp gems/defeated a boss
    public void IncreaseExp(int amount)
    {
        exp += amount;
        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        if (exp > expCap)
        {
            level += 1;
            exp -= expCap;

            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    expCap += range.expCapIncrease;
                    break;
                }
            }
        }
    }
}
