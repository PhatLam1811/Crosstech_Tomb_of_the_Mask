using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CPlayerLevelData
{
    public static CPlayerLevelData Instance => CGameDataManager.Instance._gameData?._playerLevelData;

    public int level;
    public int requiredExpToNextLevel;
    public int exp;

    private const int INITIAL_LEVEL = 1;
    private const int INITIAL_EXP = 0;
    private const int MAX_LEVEL = 20;
    private const int REQUIRE_EXP_TO_NEXT_LEVEL = 100;

    public void CreateNew()
    {
        this.level = INITIAL_LEVEL;
        this.exp = INITIAL_EXP;
        this.requiredExpToNextLevel = REQUIRE_EXP_TO_NEXT_LEVEL;
    }

    public void AddEXPValue(int value)
    {
        this.exp += value;

        if (this.exp >= this.requiredExpToNextLevel)
        {
            this.PlayerLevelUp();
        }
    }

    private void PlayerLevelUp()
    {
        if (this.level < MAX_LEVEL)
        {
            this.level++;
            this.exp -= this.requiredExpToNextLevel;
        }
        else
        {
            this.exp = this.requiredExpToNextLevel;
        }
    }
}
