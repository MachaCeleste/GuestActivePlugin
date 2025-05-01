using HarmonyLib;
using Miniscript;
using System.Reflection;

[HarmonyPatch]
public class GreyInterpreterPatch
{
    [HarmonyPatch(typeof(GreyInterpreter), "NetSessionType")]
    class NetSessionTypePatch
    {
        static void Postfix(GreyInterpreter __instance, ref GreyMap __result)
        {
            FieldInfo fieldInfo = AccessTools.Field(typeof(GreyInterpreter), "_netSessionType");
            GreyMap _netSessionType = fieldInfo.GetValue(__instance) as GreyMap;
            if (!_netSessionType.ContainsKey("is_guest_active_user"))
            {
                _netSessionType["is_guest_active_user"] = Intrinsic.GetByName("is_guest_active_user").GetFunc();
            }
            fieldInfo.SetValue(__instance, _netSessionType);
            __result = _netSessionType;
        }
    }
}