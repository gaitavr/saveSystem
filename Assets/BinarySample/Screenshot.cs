using UnityEngine;

public static class Screenshot
{
    private const int DIVIDER = 10;
    public static int Width => Screen.width / DIVIDER;
    public static int Height => Screen.height / DIVIDER;
}
