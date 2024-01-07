namespace VUDK.Extensions
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class TextureExtension
    {
        public static Dictionary<int, Sprite> CreateSpriteSheet(Texture2D textureSpriteSheet, int columns, int rows)
        {
            Dictionary<int, Sprite> spriteDict = new Dictionary<int, Sprite>();
            int spriteWidth = textureSpriteSheet.width / columns;
            int spriteHeight = textureSpriteSheet.height / rows;

            int index = 0;
            for (int row = rows - 1; row >= 0; row--)
            {
                for (int col = 0; col < columns; col++, index++)
                {
                    Rect rect = new Rect(col * spriteWidth, row * spriteHeight, spriteWidth, spriteHeight);

                    Texture2D spriteTexture = new Texture2D((int)rect.width, (int)rect.height);
                    Color[] colors = textureSpriteSheet.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
                    spriteTexture.SetPixels(colors);
                    spriteTexture.Apply();

                    spriteDict.Add(index, Sprite.Create(spriteTexture, new Rect(0, 0, spriteWidth, spriteHeight), new Vector2(0.5f, 0.5f)));
                }
            }

            return spriteDict;
        }
    }
}