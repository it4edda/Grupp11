using UnityEngine;

[System.Serializable]
public class MinMax
{
    public int min;
    public int max;

    public int GetRandom()
    {
        return Random.Range(min, max +1);
    }
}
