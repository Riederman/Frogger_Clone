using UnityEngine;
using UnityEngine.Assertions;

public static class Utilities
{
    public static int GetRandomIntExcluding(int min, int max, int num)
    {
        Assert.IsTrue(num > min && num < max);

        int random = 0;

        while (true)
        {
            random = Random.Range(min, max);

            if (random != num)
            {
                break;
            }
        }

        return random;
    }
}