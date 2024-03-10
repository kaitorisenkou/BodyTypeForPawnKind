using System.Collections.Generic;

using Verse;
using RimWorld;
using System.Linq;

namespace BodyTypeForPawnKind {
    public class ModExtension_ForcedStyleItem : DefModExtension {
        public HairDef forcedHair = null;
        public BeardDef forcedBeard = null;
        public TattooDef forcedBodyTattoo = null;
        public TattooDef forcedFaceTattoo = null;

        public HairDef GetForcedHair(){
            return forcedHair;
        }
        public BeardDef GetForcedBeard() {
            return forcedBeard;
        }
        public TattooDef GetForcedTattoo(TattooType tattooType) {
            if (tattooType == TattooType.Face)
                return forcedFaceTattoo;
            return forcedBodyTattoo;
        }
    }
}
