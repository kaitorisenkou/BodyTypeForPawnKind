using System.Collections.Generic;

using Verse;
using System.Linq;

namespace BodyTypeForPawnKind {
    public class ModExtension_HeadTypesOfPawnKind : DefModExtension {
        public List<HeadTypeDef> whitelist = new List<HeadTypeDef>();
        public List<HeadTypeDef> blacklist = new List<HeadTypeDef>();
        public List<HeadTypeDef> priority = new List<HeadTypeDef> ();
        public bool checkGender = false;

        public void SetAllowedHead(Pawn pawn) {
            if (priority.NullOrEmpty()) {
                IEnumerable<HeadTypeDef> newPriority = DefDatabase<HeadTypeDef>.AllDefsListForReading;
                if (!whitelist.NullOrEmpty()) {
                    newPriority = newPriority.Where(t => whitelist.Contains(t));
                }
                if (!blacklist.NullOrEmpty()) {
                    newPriority = newPriority.Where(t => !blacklist.Contains(t));
                }
                priority = newPriority.ToList();
            }
            if ((!blacklist.NullOrEmpty() &&
                blacklist.Contains(pawn.story.headType)) ||
                (!whitelist.NullOrEmpty() ||
                !whitelist.Contains(pawn.story.headType))
                ) {
                HeadTypeDef result = checkGender ?
                    priority.Where(t => t.gender == Gender.None || t.gender == pawn.gender).RandomElement() :
                    priority.RandomElement();
                pawn.story.headType = result;
                return;
            }
            Log.Warning("[BodyTypeForPawnKind]Nothing filterd for " + pawn.Name+"'s head");
        }
    }
}
