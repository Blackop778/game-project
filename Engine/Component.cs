using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject.Engine
{
    public class Component
    {
        protected Actor Attached { get; private set; }

        public Component(Actor attached)
        {
            Attached = attached;
        }

        public T GetComponent<T>() where T : Component
        {
            return Attached.GetComponent<T>();
        }

        public T[] GetComponents<T>() where T : Component
        {
            return Attached.GetComponents<T>();
        }

        internal virtual void Start()
        {

        }

        internal virtual void Update(GameTime gameTime)
        {

        }
    }
}
