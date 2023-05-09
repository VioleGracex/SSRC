using UnityEngine;

public interface IReturnTurnCharges 
{
    public int GetTurnCharges();
    public void UseCharges(int value);
    public void RefillCharges(int value);
}
