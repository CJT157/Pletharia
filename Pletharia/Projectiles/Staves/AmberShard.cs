using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Pletharia.Projectiles.Staves
{
    public class AmberShard : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Amber Shard";
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = true;
            projectile.light = 0.2F;
            projectile.alpha = 100;
            projectile.magic = true;

            projectile.penetrate = -1; // Penetrates infinitely
        }

        public override bool PreAI()
        {
            if (projectile.ai[0] == 1)
            {
                projectile.rotation = projectile.velocity.ToRotation() + 0.8F;
                if (Main.myPlayer == projectile.owner)
                {
                    if (Main.player[projectile.owner].channel && projectile.ai[1] < 60 &&
                        Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].type == mod.ItemType("SuperAmberStaff"))
                        
                        projectile.ai[1]++;
                    else
                        projectile.Kill();
                }
            }
            return false;
        }

        public override void Kill(int timeLeft)
        {
            if (projectile.ai[0] == 1)
            {
                int[] rotations = new int[5] { 0, 72, 144, 216, 288 };
                for (int i = 0; i < rotations.Length; ++i)
                {
                    float newRot = MathHelper.ToRadians(rotations[i]);
                    Vector2 velocity = new Vector2((float)Math.Cos(newRot), (float)Math.Sin(newRot));
                    velocity.Normalize();
                    int newProj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, velocity.X * 16, velocity.Y * 16, projectile.type, projectile.damage, projectile.knockBack, projectile.owner);
                    Main.projectile[newProj].rotation = MathHelper.ToRadians(rotations[i] + 45);
                }
            }
            projectile.active = false;
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
