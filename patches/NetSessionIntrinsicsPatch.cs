using HarmonyLib;
using Miniscript;
using System.Collections.Generic;

[HarmonyPatch]
public class NetSessionIntrinsicsPatch
{
    [HarmonyPatch(typeof(NetSessionIntrinsics), "AddInstrinsics")]
    class AddInstrinsicsPatch
    {
        private static bool intrinsicsAdded;

        static void Postfix()
        {
            if (intrinsicsAdded)
                return;
            intrinsicsAdded = true;
            Intrinsic intrinsic = Intrinsic.Create("is_guest_active_user");
            intrinsic.AddParam("self");
            intrinsic.code = delegate (TAC.Context context, Intrinsic.Result partialResult)
            {
                GreyMap greyMap = context.GetVar("self") as GreyMap;
                if (greyMap != null)
                {
                    HelperScript.HelperValFile netSession = ((GreyInterpreter)context.interpreter).hostData.GetNetSession(greyMap);
                    if (netSession != null)
                    {
                        List<Computer.Procesos> procesos = netSession.GetComputer().GetProcesos();
                        bool flag = false;
                        for (int i = 0; i < procesos.Count; i++)
                        {
                            if (!procesos[i].nombreProceso.Equals("Xorg") && !procesos[i].nombreProceso.Equals("kernel_task") && procesos[i].nombreUser.Equals("guest"))
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            return Intrinsic.Result.False;
                        }
                        return Intrinsic.Result.True;
                    }
                }
                return Intrinsic.Result.Null;
            };
        }
    }
}