using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using GameProject.Components;

namespace GameProject.Engine
{
    public class Component : IDestroyable
    {
        protected Actor Attached { get; private set; }
        public bool IsDestroyed { get; private set; }

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

        internal virtual void Update()
        {

        }

        internal virtual void FixedUpdate()
        {

        }

        public void Destroy()
        {
            if (IsDestroyed)
                return;

            IsDestroyed = true;
            Game.Instance.Destroy(this);
        }

        public virtual void FinalDestroy()
        {
            Attached = null;
        }
    }
}
