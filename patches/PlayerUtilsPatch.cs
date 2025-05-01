using HarmonyLib;
using System.Collections.Generic;

[HarmonyPatch]
public class PlayerUtilsPatch
{
    [HarmonyPatch(typeof(PlayerUtils), "ConfigLanguage")]
    class ConfigLanguagePatch
    {
        private static Dictionary<string, string> contents = new Dictionary<string, string>()
        {
            { "DOC_NETSESSION_IS_GUEST_ACTIVE_USER", "<mark=#00D0124D><b>[Parameters]</b></mark>\nNone\n\n<mark=#00D0124D><b>[Description]</b></mark>\nReturns true if the guest user is active on the system, false otherwise." }
        };

        static void Postfix()
        {
            foreach (var content in contents)
            {
                TranslationSystem.Singleton.rootTexts["English"].content.Add(content.Key, content.Value);
            }
        }
    }
}