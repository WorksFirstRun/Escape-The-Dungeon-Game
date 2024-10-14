using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class BaseHealthScript : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    protected float currentHealth;
    public event EventHandler onTakeDamageAnimation;
    public event EventHandler<CurrentHealthArgs> onHealthUpdateBar;
    public event EventHandler<CurrentHealthArgs> onHealAnimation;
    public event EventHandler onDeathAnimation;
    private bool died;

    public class CurrentHealthArgs : EventArgs
    {
        public float currentHealth;
    }

    public void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        onHealthUpdateBar?.Invoke(this , new CurrentHealthArgs()
        {
            currentHealth = currentHealth/maxHealth
        });
        if (currentHealth != 0)
        {
            onTakeDamageAnimation?.Invoke(this, EventArgs.Empty);
            if (this is EnemyHealth)
            {
                EnemyHealth enemy = this as EnemyHealth;
                if (enemy != null)
                {
                    if (enemy.TryGetComponent<IStateManager>(out IStateManager s))
                    {
                        s.SwitchState(s.GetState(States.KnockBack.ToString()));
                    }
                }
                
            }
            else
            {
                PlayerHealth player = this as PlayerHealth;
                if (player != null)
                {
                    if (player.TryGetComponent<IStateManager>(out IStateManager s))
                    {
                        s.SwitchState(s.GetState(States.Idle.ToString()));
                    }
                }
            }
        }
        else if (currentHealth <= 0 && !died)
        {
            died = true;
            if (this is EnemyHealth)
            {
                EnemyHealth enemy = this as EnemyHealth;
                if (enemy != null)
                {
                    if (enemy.TryGetComponent<IStateManager>(out IStateManager s))
                    {
                        s.DisableStateManager();
                    }
                }

                enemy.GetComponent<BoxCollider2D>().enabled = false;
                onDeathAnimation?.Invoke(this , EventArgs.Empty);
                StartCoroutine(DeathTimer(enemy));
            }
            else
            {
                Destroy(gameObject);
                if (InputManager.Instance !=  null)
                {
                    Destroy(InputManager.Instance.gameObject);
                }
                GameManager.Instance.PauseGame();
                GameManager.Instance.ShowLoseWindow();
            }
            
            
        }
    }

    private IEnumerator DeathTimer(EnemyHealth enemy)
    {
        yield return new WaitForSeconds(1);
        DropSystem.Instance.InvokeDropEvent(enemy ,enemy.GetLoot(),enemy.transform.position);
        Destroy(gameObject);
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetCurrentHealth(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        onHealthUpdateBar?.Invoke(this , new CurrentHealthArgs()
        {
            currentHealth = currentHealth/maxHealth
        });
    }


    public void SetMaxHealth(float amount) // maybe will need it in the xp system
    {
        
    }
    
    

}
