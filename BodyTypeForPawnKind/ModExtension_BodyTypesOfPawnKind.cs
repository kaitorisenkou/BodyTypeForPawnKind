using System.Collections.Generic;

using Verse;
using RimWorld;
using System.Linq;

namespace BodyTypeForPawnKind {
    public class ModExtension_BodyTypesOfPawnKind : DefModExtension {
        public List<BodyTypeDef> whitelist = new List<BodyTypeDef>();
        public List<BodyTypeDef> blacklist = new List<BodyTypeDef>();
        public List<BodyTypeDef> priority = new List<BodyTypeDef> { BodyTypeDefOf.Male };
        public bool checkGender = true;

        public void SetAllowedBody(Pawn pawn) {
            if (ModsConfig.BiotechActive && pawn.DevelopmentalStage.Juvenile())
                return;
            if (priority.NullOrEmpty()) {
                IEnumerable<BodyTypeDef> newPriority = DefDatabase<BodyTypeDef>.AllDefsListForReading;
                if (!whitelist.NullOrEmpty()) {
                    newPriority = newPriority.Where(t => whitelist.Contains(t));
                }
                if (!blacklist.NullOrEmpty()) {
                    newPriority = newPriority.Where(t => !blacklist.Contains(t));
                }
                priority = newPriority.ToList();
            }
            if ((!blacklist.NullOrEmpty() &&
                blacklist.Contains(pawn.story.bodyType)) ||
                (!whitelist.NullOrEmpty() ||
                !whitelist.Contains(pawn.story.bodyType))
                ) {
                var result = priority.RandomElement();
                if (checkGender) {
                    if(pawn.gender == Gender.Female && result == BodyTypeDefOf.Male) {
                        result = BodyTypeDefOf.Female;
                    }
                    if (pawn.gender == Gender.Male && result == BodyTypeDefOf.Female) {
                        result = BodyTypeDefOf.Male;
                    }
                }
                pawn.story.bodyType = result;
            }
        }
    }
}
