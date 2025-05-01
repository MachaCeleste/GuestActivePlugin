using HarmonyLib;
using System.Collections.Generic;

[HarmonyPatch]
public class DocsGreyApiPatch
{
    [HarmonyPatch(typeof(DocsGreyApi), "Awake")]
    class AwakePatch
    {
        private static string _type = "NetSession";
        private static List<DocsGreyApi.Method> _methods = new List<DocsGreyApi.Method>()
        {
            new DocsGreyApi.Method(){ name = "is_guest_active_user" , args = "" }
        };

        static void Postfix(DocsGreyApi __instance)
        {
            foreach (var method in _methods)
            {
                DocsGreyApi.Singleton.clases[_type].methods.Add(method);
            }
        }
    }
}