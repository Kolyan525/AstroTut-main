using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField]
    Text healthText;

    [SerializeField]
    Text speedText;
    
    [SerializeField]
    float healthMultiplier = 1.1f;
    
    [SerializeField]
    float movementSpeedMultiplier = 1.1f;
    
    [SerializeField]
    int upgradeCost = 50;

    PlayerStats stats;

    void OnEnable()
    {
        stats = PlayerStats.instance;
        AudioManager.instance.PlaySound("ZaWarudooo");
        UpdateValues();
    }

    void UpdateValues()
    {
        healthText.text = "HEALTH: " + stats.maxHealth.ToString();
        speedText.text = "SPEED: " + stats.movementSpeed.ToString();
    }

    public void UpgradeHealth()
    {
        if (GameMasteR.Money < upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }

        stats.maxHealth = (int)(stats.maxHealth * healthMultiplier);

        GameMasteR.Money -= upgradeCost;
        AudioManager.instance.PlaySound("Money");

        UpdateValues();
    }

    public void UpgradeSpeed()
    {
        if (GameMasteR.Money < upgradeCost)
        {
            AudioManager.instance.PlaySound("NoMoney");
            return;
        }

        stats.movementSpeed = Mathf.Round(stats.movementSpeed * movementSpeedMultiplier);

        GameMasteR.Money -= upgradeCost;
        AudioManager.instance.PlaySound("Money");

        UpdateValues();
    }
}
