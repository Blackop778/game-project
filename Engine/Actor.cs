using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject.Engine
{
    public abstract class Actor
    {
        public Vector2 Position { get; set; }
        public bool IsDestroyed { get; private set; }

        private readonly List<Component> components;

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

        protected void RenderDebugText(Vector2 position, string text)
        {
            Game.Instance.SpriteBatch.DrawString(Game.Instance.DebugFont, text, position, Color.White);
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

        public Component AddComponent(Component component)
        {
            components.Add(component);

            return component;
        }

        public void Destroy(Actor a)
        {
            a.IsDestroyed = true;
            Game.Instance.Destroy(a);
        }

        internal virtual void Start()
        {

        }

        internal virtual void LoadContent(ContentManager content)
        {
            
        }

        internal void EngineUpdate(GameTime gameTime)
        {
            Update(gameTime);

            foreach (Component component in components)
            {
                component.Update(gameTime);
            }
        }

        internal abstract void Update(GameTime gameTime);

        internal virtual void Draw(GameTime gameTime)
        {

        }
    }
}
