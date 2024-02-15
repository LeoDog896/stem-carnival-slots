using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotObjectHelper
{

    private static System.Random rand = new System.Random();

    private static GameObject[] gameObjects;
    private static double[] ranges;
    private static int[] vals;

    public static void setup(GameObject[] spinnerItems, double[] chances, int[] values) {
        gameObjects = spinnerItems;
        double totalChance = 0; 

        foreach (double chance in chances)
        {
            totalChance += chance;
        }
        ranges = new double[chances.Length];
        //go again, and this time, get the percent of the total and fill the percents array
        double rangesTotal = 0;
        for (int i = 0; i < chances.Length; i++)
        {
            ranges[i] = chances[i] / totalChance + rangesTotal;
            rangesTotal += chances[i] / totalChance;
        }

        vals = values;
    }

    public static SlotDisplayHelper randomObject()
    {
        //get the sum
        double randomNumber = rand.NextDouble();
        for (int c = 0; c < ranges.Length; c++)
        {
            if (randomNumber < ranges[c])
            {
                return new SlotDisplayHelper(gameObjects[c], vals[c]);
            }
        }
        return new SlotDisplayHelper(gameObjects[0], vals[0]);



    }
}

public class SlotDisplayHelper
{
    public GameObject gameObject;
    public int value;
    public GameObject cloned;
    public SlotDisplayHelper(GameObject gameObject, int value)
    {
        this.gameObject = gameObject;
        this.value = value;
    }
}
