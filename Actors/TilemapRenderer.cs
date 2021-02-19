using GameProject.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Actors
{
    class TilemapRenderer : Actor
    {
        private byte[][] tilemap;
        private string spritesheetName;
        private Texture2D spritesheet;
        private int sheetTileCountX;
        private int sheetTileCountY;
        private int tileSize;

        public TilemapRenderer(Vector2 position, byte[][] tilemap, string spritesheetName, int tileSize) : base(position)
        {
            this.tilemap = tilemap;
            this.spritesheetName = spritesheetName;
            this.tileSize = tileSize;
        }

        protected override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            spritesheet = content.Load<Texture2D>(spritesheetName);
            sheetTileCountX = spritesheet.Width / tileSize;
            sheetTileCountY = spritesheet.Height / tileSize;
        }

        protected override void Draw()
        {
            base.Draw();

            for (int y = 0; y < tilemap.Length; y++)
            {
                for (int x = 0; x < tilemap[y].Length; x++)
                {
                    if (tilemap[y][x] != 0)
                    {
                        RenderSpriteFromSheet(Position + new Vector2(x * tileSize, -y * tileSize), spritesheet, tileSize, tileSize, (tilemap[y][x] - 1) % sheetTileCountX, (tilemap[y][x] - 1) / sheetTileCountY, 0);
                    }
                }
            }
        }
    }
}
