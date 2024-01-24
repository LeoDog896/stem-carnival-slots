using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyManager : MonoBehaviour
{

    public static int slotCost = 1;
    static int balance = 2;

    public static bool chargeAccount()
    {
        if(balance >= slotCost)
        {
            balance -= slotCost;

            return false;
        }

        return true;
    }

    public static int getBalance()
    {
        return balance;
    }

    public static void givePrize(int rows)
    {
        balance += slotCost * (rows + 1);
    }

}
