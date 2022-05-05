using HugsLib;
using Verse;

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
            foreach (PawnKindDef pawnKindDef in DefDatabase<PawnKindDef>.AllDefsListForReading)
            {
                if (!CanBeUndead(pawnKindDef)) continue;
                GeneratePawnKindDef(pawnKindDef);
                GenerateThingDef(pawnKindDef.race);
            }
        }
        private PawnKindDef GeneratePawnKindDef(PawnKindDef sourceKind)
        {
            Logger.Message("Generating PawnKind for " + sourceKind.label);
            PawnKindDef newKind = new PawnKindDef();
            newKind.label = sourceKind.label;
            newKind.defName = "ROTD_" + sourceKind.defName;
            return newKind;
        }
        private ThingDef GenerateThingDef(ThingDef sourceThing)
        {
            Logger.Message("Generating ThingDef for " + sourceThing.label);
            ThingDef newThing = new ThingDef();
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
