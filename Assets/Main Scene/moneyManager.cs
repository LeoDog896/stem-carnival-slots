using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyManager : MonoBehaviour
{

    public static int slotCost = 1;
    static int balance = 50;

    public static bool chargeAccount()
    {
        if(balance >= slotCost)
        {
            balance -= slotCost;
            BioTrack.FinishGame(-1, true, new object());

            return true;
        }

        return false;
    }

    public static void SetBalance(int bal)
    {
        balance = bal;
    }
    public static int getBalance()
    {
        return balance;
    }

    public static void givePrize(int multi)
    {
        BioTrack.FinishGame(slotCost * multi, true, new object());
        balance += slotCost * multi;
    }

}
