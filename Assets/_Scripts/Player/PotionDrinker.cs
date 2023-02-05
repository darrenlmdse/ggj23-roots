using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDrinker : MonoBehaviour
{
    [SerializeField]
    public float health = 50f;

    [SerializeField]
    private ScytheHandler scytheHandler;

    [SerializeField]
    private PlayerInputController playerInputController;

    public delegate void HealthCallback(float health, float max);
    public HealthCallback OnHealthChanged;

    public float maxHealth = 100f;

    struct Boost
    {
        public bool isBoosting;
        public float timeElapsed;
        public float timeMax;
    }

    Boost attackBoost;
    Boost speedBoost;

    private float timeElapsed;
    private float healthDeductTime;

    private void Awake()
    {
        attackBoost.timeMax = 5f;
        attackBoost.timeElapsed = 0f;
        attackBoost.isBoosting = false;

        speedBoost.timeMax = 5f;
        speedBoost.timeElapsed = 0f;
        speedBoost.isBoosting = false;

        timeElapsed = 0f;
        healthDeductTime = 60f;
    }

    private void Update()
    {
        if (attackBoost.isBoosting)
        {
            attackBoost.timeElapsed += Time.deltaTime;
            if (attackBoost.timeElapsed >= attackBoost.timeMax)
            {
                attackBoost.isBoosting = false;
                attackBoost.timeElapsed = 0;
                scytheHandler.DamageMultiplier = 1f;
            }
        }

        if (speedBoost.isBoosting)
        {
            speedBoost.timeElapsed += Time.deltaTime;
            if (speedBoost.timeElapsed >= speedBoost.timeMax)
            {
                speedBoost.isBoosting = false;
                speedBoost.timeElapsed = 0;
                playerInputController.SpeedModifier = 1f;
            }
        }

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= healthDeductTime)
        {
            ChangeHealth(-10);
            timeElapsed -= healthDeductTime;
        }
    }

    public bool DrinkPotion(PotionData potionData)
    {
        scytheHandler.SetElement(potionData.ElementType);
        switch (potionData.BuffType)
        {
            case BuffType.Health:
                ChangeHealth(+10);
                return true;
            case BuffType.Attack:
                scytheHandler.DamageMultiplier = 2f;
                attackBoost.isBoosting = true;
                return true;
            case BuffType.Speed:
                playerInputController.SpeedModifier = 2f;
                speedBoost.isBoosting = true;
                return true;
        }
        return false;
    }

    public void ChangeHealth(float delta)
    {
        health += delta;
        OnHealthChanged?.Invoke(health, maxHealth);
    }
}
