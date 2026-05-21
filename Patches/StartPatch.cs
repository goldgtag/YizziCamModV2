using System;
using HarmonyLib;
using UnityEngine;

namespace YizziCamModV2.Patches
{
	// Token: 0x02000007 RID: 7
	[HarmonyPatch(typeof(GorillaTagger))]
	[HarmonyPatch("Start", 0)]
	internal class StartPatch
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000020D8 File Offset: 0x000002D8
		private static void Postfix()
		{
			new GameObject().AddComponent<CameraController>();
			CameraController.Instance.YizziStart();
		}
	}
}
