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

            return true;
        }

        return false;
    }

    public static int getBalance()
    {
        return balance;
    }

    public static void givePrize(int rows)
    {
        balance += slotCost * (rows * 5);
    }

}
