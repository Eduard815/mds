using UnityEngine;

public static class SeedGeneration
{
    public static int GenSeed(int a, int b)
    {
        unchecked
        {
            int x = a;
            x ^= (int)(b + 0x9E3779B9) + (x << 6) + (x >> 2);
            return x;
        }
    }
}