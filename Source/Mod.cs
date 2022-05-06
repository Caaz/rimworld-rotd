using HugsLib;
using HugsLib.Utils;
using Verse;
using RimWorld;
using UnityEngine;

namespace RimOfTheDead
{
    public class Mod : ModBase
    {
        public override void DefsLoaded()
        {
            GenerateDefs();
        }
        private void GenerateDefs()
        {
            List<PawnKindDef> pawnkinds = DefDatabase<PawnKindDef>.AllDefsListForReading;
            foreach (PawnKindDef pawnKindDef in pawnkinds)
            {
                if (!CanBeUndead(pawnKindDef)) continue;
                ThingDef zombieDef = GenerateThingDef(pawnKindDef.race);
                GeneratePawnKindDef(pawnKindDef, zombieDef);
            }
        }
        private PawnKindDef GeneratePawnKindDef(PawnKindDef sourceKind, ThingDef zombieDef)
        {
            Logger.Message("Generating PawnKind for " + sourceKind.label);
            PawnKindDef newKind = new PawnKindDef();

            newKind.label = "Zombie " + sourceKind.label;
            newKind.defName = "ROTD_" + sourceKind.defName;
            newKind.race = zombieDef;

            InjectedDefHasher.GiveShortHashToDef(newKind, typeof(PawnKindDef));
            DefDatabase<PawnKindDef>.Add(newKind);
            return newKind;
        }
        private ThingDef GenerateThingDef(ThingDef sourceThing)
        {
            string defName = "ROTD_Zombie_" + sourceThing.defName;
            ThingDef newThing = DefDatabase<ThingDef>.GetNamedSilentFail(defName);
            if (newThing != null) return newThing;

            Logger.Message("Generating ThingDef for " + sourceThing.label);
            newThing = new ThingDef();

            newThing.thingClass = sourceThing.thingClass;
            newThing.category = sourceThing.category;

            if (sourceThing.graphicData != null)
            {
                newThing.graphicData = new GraphicData();
                newThing.graphicData.CopyFrom(sourceThing.graphicData);
                newThing.graphicData.color = new Color(1f, 0f, 0f, 1f);
            }

            InjectedDefHasher.GiveShortHashToDef(newThing, typeof(ThingDef));
            DefDatabase<ThingDef>.Add(newThing);
            return newThing;
        }
        private bool CanBeUndead(PawnKindDef pawnKindDef)
        {
            return (
                (pawnKindDef.RaceProps.IsFlesh)
                && (!pawnKindDef.label.ToLower().Contains("zombie"))
            );
        }
    }
}
