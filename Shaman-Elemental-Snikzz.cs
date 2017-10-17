using System.Drawing;
using System.Windows.Forms;
using Frozen.Helpers;

namespace Frozen.Rotation
{
    public class Elemental : CombatRoutine
    {
        public override Form SettingsForm { get; set; }

        public override void Initialize()
        {
            Log.Write("Supported build: 3002331, 3002332, 3002333", Color.Green);
            Log.Write("Frozen Elemental");
        }

        public override void Stop()
        {
        }

        public override void Pulse()
        {
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
                if (WoW.HasTarget && WoW.TargetIsEnemy) //First things go first
                {
                    if (WoW.CanCast("Wind Shear") && WoW.TargetIsCastingAndSpellIsInterruptible && WoW.IsSpellInRange("Wind Shear"))
                    {
                        WoW.CastSpell("Wind Shear");
                        return;
                    }
                    if (WoW.IsMoving)
                    {//Stormkeeper
                        if (WoW.CanCast("Flame Shock") && !WoW.TargetHasDebuff("Flame Shock"))
                        {
                            WoW.CastSpell("Flame Shock");
                            return;
                        }
                        if (WoW.PlayerHasBuff("Lava Surge") && WoW.CanCast("Lava Burst"))
                        {
                            WoW.CastSpell("Lava Burst");
                            return;
                        }
                        if (WoW.PlayerHasBuff("Stormkeeper") && WoW.CanCast("Lightning Bolt"))
                        {
                            WoW.CastSpell("Lightning Bolt");
                            return;
                        }
                        if (WoW.CanCast("Totem Mastery") && !WoW.PlayerHasBuff("Totem Mastery"))
                        {
                            WoW.CastSpell("Totem Mastery");
                            return;
                        }
                        return;
                    }
                    if (WoW.CanCast("Flame Shock") && (!WoW.TargetHasDebuff("Flame Shock") || (WoW.TargetDebuffTimeRemaining("Flame Shock") <= 9 && WoW.Maelstrom >= 20 && WoW.PlayerHasBuff("Elemental Focus"))))
                    {
                        WoW.CastSpell("Flame Shock");
                        return;
                    }
                    if (WoW.CanCast("Fire Elemental") && WoW.CooldownsOn)
                    {
                        WoW.CastSpell("Fire Elemental");
                        return;
                    }
                    if (WoW.Talent(7) == 1 && WoW.CanCast("Ascendance") && WoW.CooldownsOn) // && WoW.IsBoss) //use Ascendance
                    {
                        WoW.CastSpell("Ascendance");
                        return;
                    }
                    if (WoW.CanCast("Totem Mastery") && !WoW.PlayerHasBuff("Totem Mastery"))
                    {
                        WoW.CastSpell("Totem Mastery");
                        return;
                    }
                    if (WoW.CanCast("Elemental Blast"))
                    {
                        WoW.CastSpell("Elemental Blast");
                        return;
                    }
                    if (WoW.Talent(7) == 3 && WoW.CanCast("Icefury") && WoW.Maelstrom < 101)
                    {
                        WoW.CastSpell("Icefury");
                    }
                    if (WoW.PlayerHasBuff("Icefury") && WoW.Maelstrom >= 20 && WoW.CanCast("Frost Shock"))
                    {
                        WoW.CastSpell("Frost Shock");
                    }
                    if (WoW.CanCast("Earth Shock") && WoW.Maelstrom >= 117)
                    {
                        WoW.CastSpell("Earth Shock");
                        return;
                    }
                    if (WoW.CanCast("Lava Burst") && WoW.PlayerHasBuff("Lava Surge"))
                    {
                        WoW.CastSpell("Lava Burst");
                        return;
                    }
                    if (WoW.CanCast("Lightning Bolt") && WoW.PlayerHasBuff("Power of the Maelstrom"))
                    {
                        WoW.CastSpell("Lightning Bolt");
                        return;
                    }
                    if (WoW.CanCast("Earth Shock") && !WoW.PlayerHasBuff("Lava Surge") && WoW.Maelstrom >= 111)
                    {
                        WoW.CastSpell("Earth Shock");
                        return;
                    }
                    if (WoW.CanCast("Stormkeeper") && !WoW.PlayerHasBuff("Ascendance")) //use stormkeeper after ascendance
                    {
                        WoW.CastSpell("Stormkeeper");
                        return;
                    }
                    if (WoW.CanCast("Lava Burst"))
                    {
                        WoW.CastSpell("Lava Burst");
                        return;
                    }
                    if (WoW.CanCast("Lightning Bolt"))
                    {
                        WoW.CastSpell("Lightning Bolt");
                        return;
                    }
                }

            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy) //First things go first
                {
                    if (WoW.CanCast("Elemental Blast"))
                    {
                        WoW.CastSpell("Elemental Blast");
                        return;
                    }
                    if (WoW.CanCast("Flame Shock") && !WoW.TargetHasDebuff("Flame Shock"))
                    {
                        WoW.CastSpell("Flame Shock");
                        return;
                    }
                    if (WoW.CanCast("Lava Burst") && WoW.PlayerHasBuff("Lava Surge"))
                    {
                        WoW.CastSpell("Lava Burst");
                        return;
                    }
                    if (WoW.PlayerHasBuff("Ascendance") && WoW.CanCast("Lava Beam"))
                    {
                        WoW.CastSpell("Lava Beam");
                        return;
                    }
                    if (WoW.CanCast("Chain Lightning"))
                    {
                        WoW.CastSpell("Chain Lightning");
                        return;
                    }
                }
            }
        }
    }
}

/*
[AddonDetails.db]
AddonAuthor=Snikzz
AddonName=Snikzz
WoWVersion=Legion - 70300
[SpellBook.db]  
Spell,188389,Flame Shock,D3
Spell,108271,Astral Shift,D6
Spell,198067,Fire Elemental,F7
Spell,114074,Lava Beam,E
Spell,188196,Lightning Bolt,D1
Spell,51505,Lava Burst,D2
Spell,198103,Earth Elemental,F8
Spell,188443,Chain Lightning,E
Spell,16166,Elemental Mastery,D4
Spell,114050,Ascendance,D8
Spell,61882,Earthquake,F2
Spell,108281,Ancestral Guidance,D7
Spell,205495,Stormkeeper,D0
Spell,210643,Totem Mastery,F
Spell,8042,Earth Shock,Q
Spell,51490,Thunderstorm,D5
Spell,117014,Elemental Blast
Spell,210714,Icefury
Spell,196840,Frost Shock
Spell,57994,Wind Shear
Aura,188389,Flame Shock
Aura,210659,Totem Mastery
Aura;16166,Elemental Mastery
Aura,114050,Ascendance
Aura,16164,Elemental Focus
Aura,77762,Lava Surge
Aura,205495,Stormkeeper
Aura,210714,Icefury
Aura,191861,Power of the Maelstrom
*/
