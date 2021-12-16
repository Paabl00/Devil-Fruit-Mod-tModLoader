using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevilFruitMod.Util;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using DevilFruitMod.NPCs;

namespace DevilFruitMod.Players
{

    //Added logia system, still missing flight system, logia dodge texst only works for projectiles and boss system added.
    class LogiaUser : ModPlayer
    {

        public bool logiaDodge = false;
        public int logiaTimer = 0;
        public int logiaDustID = 0;
        public bool canFly = false;
        public bool LogiaDodges(Player player)
        {
            if (player.GetModPlayer<DevilFruitUser>().devilFruitType == DevilFruitUser.LOGIA && player.HasBuff(ModContent.BuffType<Buffs.LogiaDodge>()))
            {
                if (player.GetModPlayer<DevilFruitUser>().fruitLevel == 0 || player.GetModPlayer<DevilFruitUser>().fruitLevel == 1)
                {
                    return true;
                }
                if (player.GetModPlayer<DevilFruitUser>().fruitLevel == 2)
                {
                    return false;
                }
                if(player.GetModPlayer<DevilFruitUser>().fruitLevel == 3)
                {
                    if(canFly)
                    {
                        //fly mechanics ayy
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        public override void PreUpdate()
        {
            if (player.GetModPlayer<DevilFruitUser>().devilFruitType == DevilFruitUser.LOGIA && player.GetModPlayer<Players.LogiaUser>().logiaDodge == true)
            {
                player.noFallDmg = true;
                player.AddBuff(ModContent.BuffType<Buffs.LogiaDodge>(), 30, true);
                player.GetModPlayer<LogiaUser>().logiaTimer++;
                Vector2 vec = TMath.RandomPlayerHitboxPos(player);
                Dust.NewDust(vec, 1, 1, logiaDustID, 0, 0, 255, default, 1.6f);
                Dust.NewDust(vec, 2, 4, logiaDustID, 0, 0, 255, default, 1.6f);
            }
        }

        public override void SetControls()
        {
            if (LogiaDodges(player))
            {
                player.controlJump = false;
                player.controlDown = false;
                player.controlLeft = false;
                player.controlRight = false;
                player.controlUp = false;
                player.controlHook = false;
                player.controlLeft = false;
            }
        }

        public void printLogia()
        {
            if (player.GetModPlayer<LogiaUser>().logiaTimer >= 20)
            {
                CombatText.NewText(player.getRect(), Color.White, "Logia Dodge");
                player.GetModPlayer<LogiaUser>().logiaTimer = 0;
            }
        }

        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (pvp)
            {
                if (player.GetModPlayer<LogiaUser>().logiaDodge)
                {
                    printLogia();
                    return false;
                }
            }
            return true;
        }
        
        public override bool CanBeHitByProjectile(Projectile proj)
        {
            if (player.GetModPlayer<LogiaUser>().logiaDodge)
            {
                printLogia();
                return false;
            }
            return true;
        }
        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            if (player.GetModPlayer<LogiaUser>().logiaDodge)
            {
                switch (npc.type)
                {
                    case NPCID.KingSlime:
                        if (!NPC.downedSlimeKing)
                        {
                            return true;
                        }
                        break;
                    case NPCID.EyeofCthulhu:
                        if (!NPC.downedBoss1)
                        {
                            return true;
                        }
                        break;
                    case NPCID.EaterofWorldsBody:
                        if (!NPC.downedBoss2)
                        {
                            return true;
                        }
                        break;
                    case NPCID.BrainofCthulhu:
                        if (!NPC.downedBoss2)
                        {
                            return true;
                        }
                        break;
                    case NPCID.QueenBee:
                        if (!NPC.downedQueenBee)
                        {
                            return true;
                        }
                        break;
                    case NPCID.SkeletronHand:
                        if (!NPC.downedBoss3)
                        {
                            return true;
                        }
                        break;
                    case NPCID.SkeletronHead:
                        if (!NPC.downedBoss3)
                        {
                            return true;
                        }
                        break;
                    case NPCID.WallofFlesh:
                        if (!Main.hardMode)
                        {
                            return true;
                        }
                        break;
                    case NPCID.WallofFleshEye:
                        if (!Main.hardMode)
                        {
                            return true;
                        }
                        break;
                    case NPCID.TheDestroyerBody:
                        if (!NPC.downedMechBoss1)
                        {
                            return true;
                        }
                        break;
                    case NPCID.Spazmatism:
                        if (!NPC.downedMechBoss2)
                        {
                            return true;
                        }
                        break;
                    case NPCID.Retinazer:
                        if (!NPC.downedMechBoss2)
                        {
                            return true;
                        }
                        break;
                    case NPCID.SkeletronPrime:
                        if (!NPC.downedMechBoss3)
                        {
                            return true;
                        }
                        break;
                    case NPCID.Plantera:
                        if (!NPC.downedPlantBoss)
                        {
                            return true;
                        }
                        break;
                    case NPCID.Golem:
                        if (!NPC.downedGolemBoss)
                        {
                            return true;
                        }
                        break;
                    case NPCID.CultistBoss:
                        if (!NPC.downedAncientCultist)
                        {
                            return true;
                        }
                        break;
                    case NPCID.DukeFishron:
                        if (!NPC.downedFishron)
                        {
                            return true;
                        }
                        break;
                    case NPCID.MoonLordCore:
                        if (!NPC.downedMoonlord)
                        {
                            return true;
                        }
                        break;
                    case NPCID.MoonLordFreeEye:
                        if (!NPC.downedMoonlord)
                        {
                            return true;
                        }
                        break;
                    case NPCID.MoonLordHand:
                        if (!NPC.downedMoonlord)
                        {
                            return true;
                        }
                        break;
                    case NPCID.MoonLordHead:
                        if (!NPC.downedMoonlord)
                        {
                            return true;
                        }
                        break;
                    case NPCID.MoonLordLeechBlob:
                        if (!NPC.downedMoonlord)
                        {
                            return true;
                        }
                        break;
                }

                return false;
            }
            return true;
        }

    }
}
