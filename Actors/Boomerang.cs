using GameProject.Engine.Components;
using GameProject.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace GameProject.Actors
{
    class Boomerang : Actor
    {
        private Vector2 destination;
        private Actor user;
        private bool returning;
        private Rigidbody2D rigidbody;
        private Texture2D spritesheet;
        private SoundEffect enemyDeathSound;
        private SoundEffectInstance flightSound;
        private SoundEffect catchSound;
        private int enemiesKilled = 0;
        private int animationFrame = 0;
        private float animationTime;

        private readonly float maxVelocity = 600;
        private readonly float minVelocity = 400;
        private readonly float maximumInteractRange = 50;
        private readonly float animationFrameTime = 0.25f;
        private readonly int spriteSize = 48;

        public Boomerang(Vector2 position, Vector2 destination, Actor user) : base(position) {
            this.destination = destination;
            this.user = user;
        }

        internal override void Start()
        {
            base.Start();

            rigidbody = AddComponent(new Rigidbody2D(this));
            rigidbody.MaxVelocity = maxVelocity;
            rigidbody.Drag = 140f;

            AddComponent(new RectangleCollider(this, Position, spriteSize, spriteSize));

            returning = false;
        }

        protected override void Update()
        {
            animationTime += Time.DeltaTime;
            animationFrame = (int)(animationTime / animationFrameTime) % 4;
        }

        protected override void FixedUpdate()
        {
            if (returning)
                destination = user.Position;

            if (Vector2.Distance(Position, destination) < maximumInteractRange)
            {
                if (!returning)
                    returning = true;
            }

            Vector2 instantVel = destination - Position;

            if (instantVel.LengthSquared() < (minVelocity * minVelocity))
                instantVel = instantVel.SetLength(minVelocity);
            else if (instantVel.LengthSquared() > (maxVelocity * maxVelocity))
                instantVel = instantVel.SetLength(maxVelocity);

            rigidbody.Velocity += instantVel * Time.DeltaTime;
        }

        protected override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            spritesheet = content.Load<Texture2D>("boomerang");
            enemyDeathSound = content.Load<SoundEffect>("enemy_death");

            flightSound = content.Load<SoundEffect>("boomerang_flight").CreateInstance();
            flightSound.IsLooped = true;
            flightSound.Play();

            catchSound = content.Load<SoundEffect>("boomerang_catch");
        }

        protected override void Draw()
        {
            base.Draw();

            RenderSpriteFromSheet(Position, spritesheet, spriteSize, spriteSize, 0, 0, animationFrame);
        }

        internal override void OnTriggerEnter(RectangleCollider collider, Actor other, RectangleCollider otherCollider)
        {
            base.OnTriggerEnter(collider, other, otherCollider);

            if (returning && other == user)
            {
                Destroy();
                catchSound.Play();
            }
            else if (other is Enemy)
            {
                other.Destroy();
                Game.Instance.SpawnEnemy();
                enemyDeathSound.Play();

                enemiesKilled++;

                Scoreboard s = Game.Instance.GetActor<Scoreboard>();
                if (s != null)
                {
                    s.EnemiesKilled++;
                    if (enemiesKilled > s.EnemiesKilledStreak)
                        s.EnemiesKilledStreak = enemiesKilled;
                }
            }
        }

        public override void FinalDestroy()
        {
            base.FinalDestroy();

            flightSound.Stop();

            user = null;
            rigidbody = null;
            flightSound = null;
        }
    }
}
