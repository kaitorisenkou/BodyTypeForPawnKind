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
    }
}
