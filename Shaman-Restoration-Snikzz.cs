using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Frozen.Helpers;

namespace Frozen.Rotation
{
    public class Restoration : CombatRoutine
    {
        private Stopwatch healingTotem = new Stopwatch();
        private Stopwatch Pets = new Stopwatch();

        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Supported build: 3000123", Color.Green);
            Log.Write("Frozen Restoration");
        }

        public override void Stop()
        {
        }

        public void TimerReset()
        {
            if (healingTotem.Elapsed.Seconds >= 8)
            {
                healingTotem.Reset();
            }
            if (Pets.Elapsed.Seconds >= 15)
            {
                Pets.Reset();
            }
        }

        public override void Pulse()
        {
            TimerReset();
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here

                //if (WoW.HasTarget && WoW.TargetIsEnemy) //First things go first
                //{

                    /*	if (WoW.TargetIsCasting && WoW.IsSpellInRange("Wind Shear")) //interupt every spell - need to add kickable spells
						{
							WoW.CastSpell("Wind Shear");
							return;
						}*/
                    if (WoW.IsMoving && WoW.PartyLowestHealthPercent <= 70 && WoW.CanCast("Spiritwalker's Grace") && !WoW.PlayerHasBuff("Spiritwalker's Grace"))
                    {
                        WoW.CastSpell("Spiritwalker's Grace");
                        return;
                    }
                    if (WoW.CanCast("Riptide"))
                    {
                        WoW.CastSpell("Riptide");
                        return;
                    }
                    if (WoW.CanCast("Cloudburst Totem"))
                    {
                        WoW.CastSpell("Cloudburst Totem");
                        return;
                    }
                    if (WoW.CanCast("Healing Stream Totem"))
                    {
                        WoW.CastSpell("Healing Stream Totem");
                        return;
                    }
                    if (WoW.CanCast("Healing Tide Totem") && WoW.CountAlliesUnderHealthPercentage(60) >= 6)
                    {
                        WoW.CastSpell("Healing Tide Totem");
                        return;
                    }

                    if (WoW.CountAlliesUnderHealthPercentage(80) >= 3 && WoW.CanCast("Chain Heal"))
                    {
                        WoW.TargetMember(WoW.PartyMemberIdWithLowestHealthPercent);
                        WoW.CastSpell("Chain Heal");
                        return;
                    }
                    if (WoW.CanCast("Healing Surge") && WoW.PlayerHasBuff("Tidal Waves") && WoW.PartyLowestHealthPercent <= 60)
                    {
                        WoW.TargetMember(WoW.PartyMemberIdWithLowestHealthPercent);
                        WoW.CastSpell("Healing Surge");
                        return;
                    }
                    if (WoW.CanCast("Healing Wave") && WoW.PartyLowestHealthPercent <= 80)
                    {
                        WoW.TargetMember(WoW.PartyMemberIdWithLowestHealthPercent);
                        WoW.CastSpell("Healing Wave");
                        return;
                    }
               // }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Snikzz
AddonName=Snikzz
WoWVersion=Legion - 70300
[SpellBook.db]
Spell,77472,Healing Wave
Spell,8004,Healing Surge
Spell,1064,Chain Heal
Spell,108280,Healing Tide Totem
Spell,98008,Spirit Link Totem
Spell,5394,Healing Stream Totem
Spell,157153,Cloudburst Totem
Spell,198838,Earthen Shield Totem
Spell,61295,Riptide
Spell,79206,Spiritwalker's Grace
Aura,79206,Spiritwalker's Grace
Aura,53390,Tidal Waves
*/
