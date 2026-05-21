using System;
using System.Reflection;
using HarmonyLib;

namespace YizziCamModV2
{
	// Token: 0x02000004 RID: 4
	public class HarmonyPatches
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002058 File Offset: 0x00000258
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000205F File Offset: 0x0000025F
		public static bool IsPatched { get; private set; }

		// Token: 0x06000008 RID: 8 RVA: 0x00002067 File Offset: 0x00000267
		internal static void ApplyHarmonyPatches()
		{
			if (!HarmonyPatches.IsPatched)
			{
				if (HarmonyPatches.instance == null)
				{
					HarmonyPatches.instance = new Harmony("com.yizzi.gorillatag.yizzicammodv2");
				}
				HarmonyPatches.instance.PatchAll(Assembly.GetExecutingAssembly());
				HarmonyPatches.IsPatched = true;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000209B File Offset: 0x0000029B
		internal static void RemoveHarmonyPatches()
		{
			if (HarmonyPatches.instance != null && HarmonyPatches.IsPatched)
			{
				HarmonyPatches.instance.UnpatchSelf();
				HarmonyPatches.IsPatched = false;
			}
		}

		// Token: 0x04000034 RID: 52
		private static Harmony instance;

		// Token: 0x04000036 RID: 54
		public const string InstanceId = "com.yizzi.gorillatag.yizzicammodv2";
	}
}
