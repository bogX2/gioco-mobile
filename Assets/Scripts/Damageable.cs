using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public PauseMenu pauseMenu;
    private bool isDead;
    public UnityEvent<int, Vector2> damageableHit;

    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;
    private Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth {
        get {
            return _maxHealth;
        }
        set {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;
    public int Health {
        get {
            return _health;
        }
        set {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            // Controllo se la salute scende sotto 0 e se non è già morto
            if (_health <= 0 && !isDead) {
                IsAlive = false;
                isDead = true;
                
                // Controllo se pauseMenu è assegnato
                if (pauseMenu != null) {
                    pauseMenu.gameOver();
                } else {
                    Debug.LogError("PauseMenu non è assegnato!");
                }

                Debug.Log("Dead");
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;
    public bool IsHit {
        get {
            return animator != null && animator.GetBool(AnimationStrings.isHit);
        }
        private set {
            if (animator != null) {
                animator.SetBool(AnimationStrings.isHit, value);
            } else {
                Debug.LogError("Animator non assegnato!");
            }
        }
    }

    private float timeSinceHit = 0;
    public float invicibilityTime = 0.25f;

    public bool IsAlive {
        get {
            return _isAlive;
        }
        set {
            _isAlive = value;
            if (animator != null) {
                animator.SetBool(AnimationStrings.isAlive, value);
            } else {
                Debug.LogError("Animator non assegnato!");
            }
            Debug.Log("IsAlive set " + value);

            if (value == false) {
                // Controllo se ci sono listener prima di invocare l'evento
                if (damageableDeath != null) {
                    damageableDeath.Invoke();
                } else {
                    Debug.LogWarning("Nessun listener per l'evento damageableDeath");
                }
            }
        }
    }

    public bool LockVelocity {
        get {
            return animator != null && animator.GetBool(AnimationStrings.lockVelocity);
        }
        set {
            if (animator != null) {
                animator.SetBool(AnimationStrings.lockVelocity, value);
            } else {
                Debug.LogError("Animator non assegnato!");
            }
        }
    }

    private void Awake() {
        // Controllo se l'animator viene assegnato correttamente
        animator = GetComponent<Animator>();
        if (animator == null) {
            Debug.LogError("Animator non trovato sul GameObject " + gameObject.name);
        }

        // Se pauseMenu non è assegnato nell'editor, prova a trovarlo
        if (pauseMenu == null) {
            pauseMenu = FindObjectOfType<PauseMenu>();
            if (pauseMenu == null) {
                Debug.LogError("PauseMenu non trovato nella scena!");
            }
        }
    }

    private void Update() {
        if (isInvincible) {
            if (timeSinceHit > invicibilityTime) {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    public bool Hit(int damage, Vector2 knockback) {
        if (IsAlive && !isInvincible) {
            Health -= damage;
            isInvincible = true;

            // Controllo se l'animator è assegnato
            if (animator != null) {
                animator.SetTrigger(AnimationStrings.hitTrigger);
            } else {
                Debug.LogError("Animator non assegnato!");
            }
            LockVelocity = true;

            // Controllo se ci sono listener prima di invocare l'evento
            if (damageableHit != null) {
                damageableHit.Invoke(damage, knockback);
            }

            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }

    public bool Heal(int healthRestore) {
        if (IsAlive && Health < MaxHealth) {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed.Invoke(gameObject, actualHeal);
            return true;
        }
        return false;
    }
}
