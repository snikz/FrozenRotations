using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Frozen.Helpers;

namespace Frozen.Rotation
{
    public class Enhancement : CombatRoutine
    {
        private Stopwatch Crash = new Stopwatch();
        private Stopwatch Pets = new Stopwatch();
        private Stopwatch PetsCd = new Stopwatch();
		private int stopwatchMinutes = 0;
        private bool useOpener = true;

        private bool rockbiterOpener = false;
        private bool flametongueOpener = false;
        private bool feralSpiritOpener = false;
        private bool crashLightningOpener = false;
        private bool doomWindsOpener = false;
        private bool stormStrikeOpener = false;
		private bool ascendanceOpener = false;
		

        public override Form SettingsForm { get; set; }

        public override void OutOfCombatPulse()
        {
			TimerReset();
            resetOpenerState();
            base.OutOfCombatPulse();
        }

        public override void Initialize()
        {
            Log.Write("Supported build: 3002111", Color.Green);
            Log.Write("Frozen Enhancement");
        }

        public override void Stop()
        {
        }

        private void resetOpenerState()
        {
            useOpener = true;
            rockbiterOpener = false;
            flametongueOpener = false;
            feralSpiritOpener = false;
            crashLightningOpener = false;
            doomWindsOpener = false;
            stormStrikeOpener = false;
            ascendanceOpener = false;
			
        }

        public void TimerReset()
        {
            if (Crash.Elapsed.Seconds >= 8)
            {
                Crash.Reset();
            }
            if (Pets.Elapsed.Seconds >= 15)
            {
                Pets.Reset();
            }
			if(PetsCd.Elapsed.Seconds >= 59)
            {
				int tempCounter = PetsCd.Elapsed.Seconds;
                PetsCd.Reset();
				stopwatchMinutes++;
				if(stopwatchMinutes < 2){
					PetsCd.Start();
				} else if(stopwatchMinutes == 2 && tempCounter <= 2){
					PetsCd.Start();
				} else{
					stopwatchMinutes = 0;
				}
            }
        }

        public override void Pulse()
        {
            TimerReset();
            if (combatRoutine.Type == RotationType.SingleTarget) // Do Single Target Stuff here
                if (WoW.HasTarget && WoW.TargetIsEnemy) //First things go first
                {
                    if (useOpener && WoW.CooldownsOn)
                    {
                        doOpener();
                        if (useOpener)
                        {
                            return;
                        }
                    }
                    if (!useOpener || !WoW.CooldownsOn)
                    {
                        /*	if (WoW.TargetIsCasting && WoW.IsSpellInRange("Wind Shear")) //interupt every spell - need to add kickable spells
                            {
                                WoW.CastSpell("Wind Shear");
                                return;
                            }*/
                        if (WoW.CanCast("Rockbiter") && (!WoW.PlayerHasBuff("Landslide") || (WoW.PlayerBuffTimeRemaining("Landslide") < 2 || WoW.PlayerSpellCharges("Rockbiter") == 2)))
                        {
                            WoW.CastSpell("Rockbiter");
                            return;
                        }
						//Log.Write("Elapsed time is " + stopwatchMinutes + " minutes and " + PetsCd.Elapsed.Seconds + " seconds");
						if (WoW.CanCast("Feral Spirit") && !PetsCd.IsRunning && WoW.CooldownsOn) 
						{
							Pets.Start();
							PetsCd.Start();
							WoW.CastSpell("Feral Spirit");
							return;
						}
                        if (WoW.CanCast("Crash Lightning") && Pets.IsRunning && !Crash.IsRunning)
                        {
                            Crash.Start();
                            WoW.CastSpell("Crash Lightning");
                            return;
                        }
                        if (WoW.PlayerHasBuff("Ascendance") && WoW.CanCast("Windstrike"))
                        {
                            WoW.CastSpell("Windstrike");
                            return;
                        }
                        if (WoW.CanCast("Flametongue") && (!WoW.PlayerHasBuff("Flametongue") || WoW.PlayerBuffTimeRemaining("Flametongue") < 1))
                        {
                            WoW.CastSpell("Flametongue");
                            return;
                        }
                        if (WoW.CanCast("Doom Winds"))
                        {
                            WoW.CastSpell("Doom Winds");
                            return;
                        }
                        if (WoW.CanCast("Ascendance") && WoW.CooldownsOn)
                        {
                            WoW.CastSpell("Ascendance");
                        }
                        if (WoW.CanCast("Stormstrike") && WoW.PlayerHasBuff("Stormbringer") && WoW.Maelstrom >= 20)
                        {
                            WoW.CastSpell("Stormstrike");
                            return;
                        }
                        if (WoW.SetBonus(20) >= 2 && WoW.CanCast("Crash Lightning") && !WoW.PlayerHasBuff("Lightning Crash"))
                        {
                            WoW.CastSpell("Crash Lightning");
                            return;
                        }
                        if (WoW.CanCast("Stormstrike") && WoW.Maelstrom >= 40)
                        {
                            WoW.CastSpell("Stormstrike");
                            return;
                        }
                        if (WoW.CanCast("Flametongue") && (!WoW.PlayerHasBuff("Flametongue") || WoW.PlayerBuffTimeRemaining("Flametongue") < 5))
                        {
                            WoW.CastSpell("Flametongue");
                            return;
                        }
                        if (WoW.CanCast("Rockbiter") && WoW.Maelstrom < 40)
                        {
                            WoW.CastSpell("Rockbiter");
                            return;
                        }
                        if (WoW.CanCast("Lava Lash") && WoW.Maelstrom > 40)
                        {
                            WoW.CastSpell("Lava Lash");
                            return;
                        }
                    }
                }

            if (combatRoutine.Type == RotationType.AOE)
            {
                if (WoW.HasTarget && WoW.TargetIsEnemy) //First things go first
                {

                }
            }
        }
        private void doOpener()
        {
            if (WoW.CanCast("Rockbiter") && !rockbiterOpener)
            {
                rockbiterOpener = true;
                WoW.CastSpell("Rockbiter");
                return;
            }
            if (WoW.CanCast("Flametongue") && !flametongueOpener && rockbiterOpener)
            {
                flametongueOpener = true;
                WoW.CastSpell("Flametongue");
                return;
            }
            if (WoW.CanCast("Feral Spirit") && !PetsCd.IsRunning && !feralSpiritOpener && flametongueOpener && rockbiterOpener)
            {
                feralSpiritOpener = true;
                Pets.Start();
                PetsCd.Start();
                WoW.CastSpell("Feral Spirit");
				crashLightningOpener = false;
                return;
            }
            else if(PetsCd.IsRunning)
            {
                feralSpiritOpener = true;
            }
            if (WoW.CanCast("Crash Lightning") && Pets.IsRunning && !Crash.IsRunning && !crashLightningOpener && feralSpiritOpener && flametongueOpener && rockbiterOpener)
            {
                crashLightningOpener = true;
                Crash.Start();
                WoW.CastSpell("Crash Lightning");
                return;
            }
            else if (!Pets.IsRunning || Crash.IsRunning)
            {
                crashLightningOpener = true;
            }
            if (WoW.CanCast("Doom Winds") && !doomWindsOpener && crashLightningOpener && feralSpiritOpener && flametongueOpener && rockbiterOpener)
            {
                doomWindsOpener = true;
                WoW.CastSpell("Doom Winds");
                return;
            }
            else if (WoW.IsSpellOnCooldown("Doom Winds"))
            {
                doomWindsOpener = true;
            }
            if (WoW.CanCast("Stormstrike") && !stormStrikeOpener && doomWindsOpener && crashLightningOpener && feralSpiritOpener && flametongueOpener && rockbiterOpener)
            {
                stormStrikeOpener = true;
                WoW.CastSpell("Stormstrike");
                return;
            }
            if (WoW.CanCast("Ascendance") && !ascendanceOpener && stormStrikeOpener && doomWindsOpener && crashLightningOpener && feralSpiritOpener & flametongueOpener && rockbiterOpener)
            {
                ascendanceOpener = true;
                WoW.CastSpell("Ascendance");
                return;
            }
            else if (WoW.IsSpellOnCooldown("Ascendance"))
            {
                ascendanceOpener = true;
            }
            if (ascendanceOpener && stormStrikeOpener && doomWindsOpener && crashLightningOpener && feralSpiritOpener & flametongueOpener && rockbiterOpener)
            {
                Log.Write("Done with opener");
                useOpener = false;
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
Spell,57994,Wind Shear,NumPad1
Spell,196884,Feral Lunge,F9
Spell,51533,Feral Spirit,NumPad0
Spell,196834,Frostbrand,NumPad2
Spell,204945,Doom Winds,F1
Spell,187874,Crash Lightning,NumPad4
Spell,193796,Flametongue,NumPad3
Spell,108271,Astral Shift,F2
Spell,193786,Rockbiter,NumPad5
Spell,60103,Lava Lash,NumPad6
Spell,17364,Stormstrike,NumPad7
Spell,115356,Windstrike,NumPad7
Spell,187837,Lightning bolt,NumPad8
Spell,188070,Healing Surge,NumPad9
Spell,215864,Rainfall,F8
Spell,188089,Earthen spike,F4
Spell,201898,Windsong,F5
Spell,197217,Sundering,F6
Spell,114051,Ascendance,Add
Spell,2645,Ghost Wolf,E
Spell,142173,Collapsing Futures,F12
Aura,194084,Flametongue
Aura,196834,Frostbrand
Aura,187878,Crashing Storm
Aura,187874,Crash Lightning
Aura,201846,Stormbringer
Aura,202004,Landslide
Aura,204945,Doom Winds
Aura,215864,Rainfall
Aura,114051,Ascendance
Aura,201898,Windsong
Aura,201900,Hot Hands
Aura,197211,FoA
Aura,2645,Ghost Wolf
Aura,240842,Legionfall
Aura,234143,Temptation
Aura,242284,Lightning crash
Aura,188089,Earthen spike
*/
