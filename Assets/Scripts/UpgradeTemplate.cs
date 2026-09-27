using UnityEngine;

public class UpgradeTemplate : MonoBehaviour
{
    [SerializeField] string upgradeType;
    [SerializeField] int upgradeLevel;
    [SerializeField] Sprite upgradeSprite;

    public void Upgrade()
    {
        if(upgradeType == "Health")
        {
            UpgradeHealth();
        }
        else if(upgradeType == "Health")
        {
            UpgradeHealth();
        }
    }

    void UpgradeHealth()
    {
        
    }
}
