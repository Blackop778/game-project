﻿using GameProject.Engine.Components;
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
        public Rigidbody2D Rigidbody { get; private set; }

        private Vector2 destination;
        private Actor user;
        private bool returning;
        private Texture2D spritesheet;
        private SoundEffect enemyDeathSound;
        private SoundEffectInstance flightSound;
        private SoundEffect catchSound;
        private int enemiesKilled = 0;
        private int animationFrame = 0;
        private float animationTime = 0f;

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

            Rigidbody = AddComponent(new Rigidbody2D(this));
            Rigidbody.MaxVelocity = maxVelocity;
            Rigidbody.Drag = 140f;

            AddComponent(new RectangleCollider(this, Position, spriteSize, spriteSize));

            returning = false;
        }

        protected override void Update()
        {
            base.Update();

            animationTime += Time.DeltaTime;
            animationFrame = (int)(animationTime / animationFrameTime) % 4;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

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

            Rigidbody.Velocity += instantVel * Time.DeltaTime;
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

                if (enemiesKilled % 2 == 0)
                    Game.Instance.SpawnBomb();

                Scoreboard s = Game.Instance.GetActor<Scoreboard>();
                if (s != null)
                {
                    s.Score += enemiesKilled;
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
            Rigidbody = null;
            flightSound = null;

            Bomb[] bombs = Game.Instance.GetActors<Bomb>();
            foreach (Bomb bomb in bombs)
                bomb.Destroy();
        }
    }
}
