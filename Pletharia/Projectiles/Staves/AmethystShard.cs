using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Pletharia.Projectiles.Staves
{
    public class AmethystShard : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.name = "Amethyst Shard";
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.light = 0.2F;
            projectile.alpha = 100;
            projectile.magic = true;

            projectile.penetrate = 1;
        }

        public override bool PreAI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 0.8F;
            return false;
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
