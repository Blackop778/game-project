using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameProject.Components;

namespace GameProject.Engine
{
    public abstract class Actor : IDestroyable
    {
        public Vector2 Position { get; set; }
        public bool IsDestroyed { get; private set; }

        private List<Component> components;

        public Actor() : this(Vector2.Zero) { }

        public Actor(Vector2 position)
        {
            Position = position;
            components = new List<Component>();
            IsDestroyed = false;
        }

        protected void RenderSprite(Vector2 position, Texture2D sprite)
        {
            Game.Instance.SpriteBatch.Draw(sprite, position.WorldToScreenspace(), null, Color.White, 0,
                new Vector2(sprite.Width / 2, sprite.Height / 2), 1, SpriteEffects.None, 0);
        }

        protected void RenderTextScreenspace(Vector2 position, string text, SpriteFont font)
        {
            RenderTextScreenspace(position, text, font, Color.White);
        }

        protected void RenderTextScreenspace(Vector2 position, string text, SpriteFont font, Color color)
        {
            Game.Instance.SpriteBatch.DrawString(font, text, position, color);
        }

        protected void RenderTextWorldspace(Vector2 position, string text, SpriteFont font)
        {
            RenderTextScreenspace(position.WorldToScreenspace(), text, font);
        }

        protected void RenderDebugTextScreenspace(Vector2 position, string text)
        {
            RenderTextScreenspace(position, text, Game.Instance.DebugFont);
        }

        public Actor Instantiate(Actor a)
        {
            return Game.Instance.Instantiate(a);
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in components)
            {
                if (component is T casted)
                    return casted;
            }

            return null;
        }

        public T[] GetComponents<T>() where T : Component
        {
            List<T> ret = new List<T>();
            foreach (Component component in components)
            {
                if (component is T casted)
                    ret.Add(casted);
            }

            return ret.ToArray();
        }

        public T AddComponent<T>(T component) where T : Component
        {
            components.Add(component);

            component.Start();

            return component;
        }

        public void Destroy()
        {
            if (IsDestroyed)
                return;

            IsDestroyed = true;
            Game.Instance.Destroy(this);

            foreach (Component c in components)
            {
                if(!c.IsDestroyed)
                    c.Destroy();
            }
        }

        public virtual void FinalDestroy()
        {
            components = null;
        }

        internal virtual void Start()
        {

        }

        internal virtual void LoadContent(ContentManager content)
        {
            
        }

        internal void EngineUpdate()
        {
            Update();

            foreach (Component c in components)
            {
                if (!c.IsDestroyed)
                    c.Update();
            }
        }

        internal abstract void Update();

        internal void EngineFixedUpdate()
        {
            FixedUpdate();

            foreach (Component c in components)
            {
                if (!c.IsDestroyed)
                    c.FixedUpdate();
            }
        }

        internal virtual void FixedUpdate()
        {

        }

        internal virtual void Draw()
        {

        }

        internal virtual void OnTriggerEnter(RectangleCollider collider, Actor other, RectangleCollider otherCollider)
        {

        }
    }
}
