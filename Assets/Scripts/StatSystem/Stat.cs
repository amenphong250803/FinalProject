using UnityEngine;

[System.Serializable]
public class Stat 
{
    [SerializeField] private float baseValue;

    private float flatModifier = 0f;

    public float GetValue()
    {
        return baseValue + flatModifier;
    }

    public void AddModifier(float amount)
    {
        flatModifier += amount;
    }

    public void RemoveModifier(float amount)
    {
        flatModifier -= amount;
    }

    public void SetBaseValue(float value)
    {
        baseValue = value;
    }
}
