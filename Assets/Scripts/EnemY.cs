using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class EnemY : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;

        int _curHealth;
        public int currentHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public int damage = 40;

        public void Init()
        {
            currentHealth = maxHealth;
        }
    }

    public EnemyStats stats = new EnemyStats();

    public Transform deathParticles;

    public float shakeAmt = 0.1f;
    public float shakeLength = 0.1f;

    public string deathSoundName = "Explosion";

    public int moneyDrop = 10;

    [Header("Optional: ")]
    [SerializeField]
    StatusIndicator statusIndicator;

    void Start()
    {
        stats.Init();

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
        }

        GameMasteR.gm.onToggleUpgradeMenu += OnUpgradeMenuToggle;

        if (deathParticles == null)
        {
            Debug.LogError("No death particles referenced on Enemy");
        }
    }

    void OnUpgradeMenuToggle(bool active)
    {
        // handle what happens when the upgrade menu is toggled
        GetComponent<EnemyAI>().enabled = !active;
    }

    // TODO: Here you can calculate hit count
    public void DamageEnemy(int damage)
    {
        stats.currentHealth -= damage;
        if (stats.currentHealth <= 0)
        {
            GameMasteR.KillEnemy(this);
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.currentHealth, stats.maxHealth);
        }
    }

    void OnCollisionEnter2D(Collision2D _colInfo)
    {
        PlayeR _player = _colInfo.collider.GetComponent<PlayeR>();
        if (_player != null)
        {
            _player.DamagePlayer(stats.damage);
            DamageEnemy(9999999);
        }
    }

    void OnDestroy()
    {
        GameMasteR.gm.onToggleUpgradeMenu -= OnUpgradeMenuToggle;
    }
}