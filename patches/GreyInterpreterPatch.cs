using HarmonyLib;
using Miniscript;

[HarmonyPatch]
class GreyInterpreterPatch
{
    private static GreyMap _netSessionType;
    [HarmonyPatch(typeof(GreyInterpreter), "NetSessionType")]
    static bool Prefix(GreyInterpreter __instance, ref GreyMap __result)
    {
        if (_netSessionType == null)
        {
            GreyMap greyMap = new GreyMap();
            greyMap["dump_lib"] = Intrinsic.GetByName("dump_lib").GetFunc();
            greyMap["get_num_users"] = Intrinsic.GetByName("get_num_users").GetFunc();
            greyMap["get_num_portforward"] = Intrinsic.GetByName("get_num_portforward").GetFunc();
            greyMap["get_num_conn_gateway"] = Intrinsic.GetByName("get_num_conn_gateway").GetFunc();
            greyMap["is_any_active_user"] = Intrinsic.GetByName("is_any_active_user").GetFunc();
            greyMap["is_root_active_user"] = Intrinsic.GetByName("is_root_active_user").GetFunc();
            greyMap["is_guest_active_user"] = Intrinsic.GetByName("is_guest_active_user").GetFunc();
            _netSessionType = greyMap;
        }
        __result = _netSessionType;
        return false;
    }
}