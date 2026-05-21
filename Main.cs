using System;
using BepInEx;
using UnityEngine;

namespace YizziCamModV2
{
	// Token: 0x02000005 RID: 5
	[BepInPlugin("com.yizzi.gorillatag.yizzicammodv2", "YizziCamModV2", "1.0.8")]
	public class Main : BaseUnityPlugin
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020C3 File Offset: 0x000002C3
		private void Awake()
		{
			HarmonyPatches.ApplyHarmonyPatches();
			Object.DontDestroyOnLoad(this);
		}
	}
}
