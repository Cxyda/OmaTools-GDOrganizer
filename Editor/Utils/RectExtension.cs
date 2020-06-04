using UnityEngine;

namespace Plugins.O.M.A.Games.GDOrganizer.Editor.Utils
{
    /// <summary>
    /// TODO:
    /// </summary>
    public static class RectExtension
    {
        public static Rect ShrinkFromLeft(this Rect rect, float width)
        {
            return new Rect(rect.x, rect.y, width, rect.height);
        }
        public static Rect ShrinkFromRight(this Rect rect, float width)
        {
            return new Rect(rect.x + width, rect.y, width, rect.height);
        }
        public static Rect ShrinkFromTop(this Rect rect, float height)
        {
            return new Rect(rect.x, rect.y, rect.width, height);
        }
        public static Rect ShrinkFromBottom(this Rect rect, float height)
        {
            return new Rect(rect.x, rect.y + rect.height - height, rect.width, height);
        }
        public static Rect MoveDown(this Rect rect, float amount)
        {
            return new Rect(rect.x, rect.y + amount, rect.width, rect.height);
        }
    }
}