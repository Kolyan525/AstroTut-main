using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

[RequireComponent(typeof(Platformer2DUserControl))]
public class PlayeR : MonoBehaviour
{
    public int fallBoundary = -20;

    public string deathSoundName = "DeathVoice";
    public string damageSoundName = "Grunt";

    AudioManager audioManager;

    [SerializeField]
    StatusIndicator statusIndicator;

    PlayerStats stats;

    void Start()
    {
        stats = PlayerStats.instance;

        stats.curHealth = stats.maxHealth;

        if (statusIndicator == null)
        {
            Debug.LogError("No status indicator referenced on Player");
        }
        else
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

        GameMasteR.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("PANIC! No audioManager in scene.");
        }

        InvokeRepeating("RegenHealth", 1f / stats.healthRegenRate, 1f / stats.healthRegenRate);
    }

    void RegenHealth()
    {
        stats.curHealth += 1;
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }

    void Update()
    {
        if (transform.position.y <= fallBoundary) 
        {
            DamagePlayer(9999999);
        }
    }

    void OnUpgradeMenuToggle(bool active)
    {
        // handle what happens when the upgrade menu is toggled
        GetComponent<Platformer2DUserControl>().enabled = !active;
        WeapoN _weapon = GetComponentInChildren<WeapoN>();
        if (_weapon != null)
        {
            _weapon.enabled = !active;
        }
    }

    void OnDestroy()
    {
        GameMasteR.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
    }

    public void DamagePlayer(int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            // Play death sound
            audioManager.PlaySound(deathSoundName);

            GameMasteR.KillPlayer(this);
        }
        else
        {
            // play damage sound
            audioManager.PlaySound(damageSoundName);
        }

        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }
}
