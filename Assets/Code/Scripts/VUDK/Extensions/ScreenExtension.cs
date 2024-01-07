namespace VUDK.Extensions
{
    using UnityEngine;

    public enum ScreenInputLocation
    {
        Any,
        Top,
        Bottom,
        Right,
        Left,
        Center,
    }

    public static class ScreenExtension
    {
        public static Vector2 ScreenPosition(this ScreenInputLocation location) => location switch
        {
            ScreenInputLocation.Top => new Vector2(Screen.width / 2, Screen.height),
            ScreenInputLocation.Bottom => new Vector2(Screen.width / 2, 0),
            ScreenInputLocation.Center => new Vector2(Screen.width / 2, Screen.height / 2),
            ScreenInputLocation.Right => new Vector2(Screen.width, Screen.height / 2),
            ScreenInputLocation.Left => new Vector2(0, Screen.height / 2),
            _ => Vector2.zero,
        };
    }
}