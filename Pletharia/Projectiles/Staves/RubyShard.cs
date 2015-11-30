using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Pletharia.Projectiles.Staves
{
    public class RubyShard : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Ruby Shard";
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.light = 0.2F;
            projectile.alpha = 100;
            projectile.magic = true;
            projectile.hide = true;

            projectile.penetrate = 3;
        }

        public override bool PreAI()
        {
            if (projectile.ai[0] == 1)
            {
                projectile.rotation = projectile.velocity.ToRotation() + 0.8F;
                projectile.hide = false;
            }

            projectile.ai[1]++;
            if (projectile.ai[1] == 30)
                projectile.Kill();

            return false;
        }

        public override void Kill(int timeLeft)
        {
            if (projectile.ai[0] == 1)
            {
                // Spawn two additional projectiles.
                float rotInDegrees = MathHelper.ToDegrees(projectile.rotation);

                // Spawns a projectile that moves to the RIGHT of this projectile.
                float newRot = MathHelper.ToRadians(rotInDegrees + 45);
                Vector2 velocity = new Vector2((float)Math.Cos(newRot), (float)Math.Sin(newRot));
                velocity.Normalize();
                int newProj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, velocity.X * 16, velocity.Y * 16, projectile.type, projectile.damage, projectile.knockBack, projectile.owner);
                Main.projectile[newProj].rotation = MathHelper.ToRadians(rotInDegrees + 90);
                Main.projectile[newProj].hide = false;

                // Spawns a projectile that moves to the LEFT of this projectile.
                newRot = MathHelper.ToRadians(rotInDegrees - 135);
                velocity = new Vector2((float)Math.Cos(newRot), (float)Math.Sin(newRot));
                velocity.Normalize();
                newProj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, velocity.X * 16, velocity.Y * 16, projectile.type, projectile.damage, projectile.knockBack, projectile.owner);
                Main.projectile[newProj].rotation = MathHelper.ToRadians(rotInDegrees - 90);
                Main.projectile[newProj].hide = false;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, new Rectangle?(), Color.White * 0.75F, projectile.rotation, origin, projectile.scale, SpriteEffects.None, 0);
            return false;
        }
    }
}
