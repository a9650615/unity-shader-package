using UnityEngine;

public class SplitData
{
    public static Material material;
    public static bool isSet = false;

    public static void SetMaterial(Material m)
    {
        if (isSet == false)
        {
            material = m;
            isSet = true;
        }
    }

}