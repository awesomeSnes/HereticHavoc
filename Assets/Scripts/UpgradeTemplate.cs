using UnityEngine;

public class UpgradeTemplate : MonoBehaviour
{
    [SerializeField] string upgradeType;
    [SerializeField] int upgradeLevel;
    [SerializeField] public Sprite upgradeSprite;

    public void Upgrade(PlayerController player)
    {
        if(upgradeType == "Health")
        {
            UpgradeHealth(player);
        }
        else if(upgradeType == "Damage")
        {
            UpgradeDamage(player);
        }
        else if(upgradeType == "Fireball")
        {
            UpgradeFireball(player);
        }
        else if(upgradeType == "Lightning")
        {
            UpgradeLightning(player);
        }
        else if(upgradeType == "Spikes")
        {
            UpgradeSpikes(player);
        } 
        else if(upgradeType == "Speed")
        {
            UpgradeWalk(player);
        }
        else if(upgradeType == "Reload")
        {
            UpgradeReload(player);
        }else if(upgradeType == "IFrames")
        {
            UpgradeIFrames(player);
        }
    }

    void UpgradeHealth(PlayerController player)
    {
        player.maxHealth += 50 * upgradeLevel;
        player.health = player.maxHealth;
        player.UpdateHealthBar(); 
    }

    void UpgradeDamage(PlayerController player)
    {
        for(int i = 0; i < player.spellList.Count; i++)
        {
            player.spellList[i].GetComponent<Spell>().damage += upgradeLevel;
        }
    }

    void UpgradeFireball(PlayerController player)
    {
        if(upgradeLevel == 3)
        {
            player.spellList[1].GetComponent<Spell>().isPiercing = true;
        }
        else{
        player.spellList[1].GetComponent<Spell>().damage += 5 * upgradeLevel;
        }
    }

    void UpgradeLightning(PlayerController player)
    {
        if(upgradeLevel == 3)
        {
            player.spellList[1].GetComponent<Spell>().isSeeking = true;
        }
        else{
        player.spellList[1].GetComponent<Spell>().damage += 5 * upgradeLevel;
        }
    }

    void UpgradeSpikes(PlayerController player)
    {
        if(upgradeLevel == 3)
        {
            player.spellList[1].GetComponent<Spell>().canFlicker = true;
        }
        player.spellList[1].GetComponent<Spell>().damage += 5 * upgradeLevel;
    }

    void UpgradeWalk(PlayerController player)
    {
        player.moveSpeed +=  upgradeLevel;
    }

    void UpgradeReload(PlayerController player)
    {
        player.shotDelay -= (float)(0.25 * upgradeLevel);
    }

    void UpgradeIFrames(PlayerController player)
    {
        player.invincibilityTime += (float)(0.5 * upgradeLevel);
    }
}
