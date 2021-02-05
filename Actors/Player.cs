using GameProject.Components;
using GameProject.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Actors
{
    internal class Player : Actor
    {
        private Texture2D sprite;
        private Rigidbody2D rigidbody;
        private Actor boomerang;
        private RectangleCollider collider;

        internal override void Start()
        {
            base.Start();

            rigidbody = AddComponent(new Rigidbody2D(this));
            rigidbody.MaxVelocity = 400;
            rigidbody.Drag = 100f;

            AddComponent(new PlayerController(this));
            collider = AddComponent(new RectangleCollider(this, Position, 64, 128));
        }

        internal override void Update()
        {
            if (boomerang != null && boomerang.IsDestroyed)
                boomerang = null;

            if (boomerang == null && InputManager.UseTool && Utilities.IsOnScreen(InputManager.MouseLocation))
                boomerang = Instantiate(new Boomerang(Position, InputManager.MouseLocation.ScreenToWorldspace(), this));
        }

        internal override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            sprite = content.Load<Texture2D>("Oval");
            collider.UpdateDimensions(Position, sprite);
        }

        internal override void Draw()
        {
            base.Draw();

            RenderSprite(Position, sprite);
            /*RenderDebugTextScreenspace(new Vector2(20, 20), rigidbody.Velocity.ToString());
            RenderDebugTextScreenspace(new Vector2(20, 40), rigidbody.Acceleration.ToString());*/
        }

        public override void FinalDestroy()
        {
            base.FinalDestroy();

            rigidbody = null;
            boomerang = null;
            collider = null;
        }
    }
}
