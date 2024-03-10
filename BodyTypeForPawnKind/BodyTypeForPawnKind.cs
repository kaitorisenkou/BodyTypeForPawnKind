using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;
using HarmonyLib;
using RimWorld;
using System.Collections.Generic;

namespace BodyTypeForPawnKind {
    [StaticConstructorOnStartup]
    public class BodyTypeForPawnKind {
        static BodyTypeForPawnKind() {
            Log.Message("[BodyTypeForPawnKind] Now active");
            var harmony = new Harmony("kaitorisenkou.BodyTypeForPawnKind");
            harmony.Patch(
                AccessTools.Method(typeof(PawnGenerator), "GenerateBodyType", null, null),
                null,
                new HarmonyMethod(typeof(BodyTypeForPawnKind), nameof(Patch_GenerateBodyType), null),
                null,
                null
                );
            harmony.Patch(
                AccessTools.Method(typeof(Pawn_StoryTracker), "TryGetRandomHeadFromSet", null, null),
                null,
                new HarmonyMethod(typeof(BodyTypeForPawnKind), nameof(Patch_TryGetRandomHeadFromSet), null),
                null,
                null
                );
            harmony.Patch(
                AccessTools.Method(typeof(PawnStyleItemChooser), "RandomHairFor", null, null),
                new HarmonyMethod(typeof(BodyTypeForPawnKind), nameof(Patch_RandomHairFor), null),
                null,
                null,
                null
                );
            harmony.Patch(
                AccessTools.Method(typeof(PawnStyleItemChooser), "RandomBeardFor", null, null),
                new HarmonyMethod(typeof(BodyTypeForPawnKind), nameof(Patch_RandomBeardFor), null),
                null,
                null,
                null
                );
            harmony.Patch(
                AccessTools.Method(typeof(PawnStyleItemChooser), "RandomTattooFor", null, null),
                new HarmonyMethod(typeof(BodyTypeForPawnKind), nameof(Patch_RandomTattooFor), null),
                null,
                null,
                null
                );
            Log.Message("[BodyTypeForPawnKind] Harmony patch complete!");
        }

        public static void Patch_GenerateBodyType(Pawn pawn, PawnGenerationRequest request) {
            var extension = request.KindDef.GetModExtension<ModExtension_BodyTypesOfPawnKind>();
            if (extension == null) return;
            extension.SetAllowedBody(pawn);
        }
        public static void Patch_TryGetRandomHeadFromSet(Pawn ___pawn) {
            var extension = ___pawn.kindDef.GetModExtension<ModExtension_HeadTypesOfPawnKind>();
            if (extension == null) return;
            extension.SetAllowedHead(___pawn);
        }

        public static bool Patch_RandomHairFor(Pawn pawn, ref HairDef __result){
            var extension = pawn.kindDef.GetModExtension<ModExtension_ForcedStyleItem>();
            if (extension == null) 
                return true;
            var result = extension.GetForcedHair();
            if (result != null) {
                __result = result;
                return false;
            }
            return true;
        }
        public static bool Patch_RandomBeardFor(Pawn pawn, ref BeardDef __result) {
            var extension = pawn.kindDef.GetModExtension<ModExtension_ForcedStyleItem>();
            if (extension == null)
                return true;
            var result = extension.GetForcedBeard();
            if (result != null) {
                __result = result;
                return false;
            }
            return true;
        }
        public static bool Patch_RandomTattooFor(Pawn pawn, TattooType tattooType, ref TattooDef __result) {
            var extension = pawn.kindDef.GetModExtension<ModExtension_ForcedStyleItem>();
            if (extension == null)
                return true;
            var result = extension.GetForcedTattoo(tattooType);
            if (result != null) {
                __result = result;
                return false;
            }
            return true;
        }
    }
}
