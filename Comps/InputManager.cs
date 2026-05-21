using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using Valve.VR;

namespace YizziCamModV2.Comps
{
	// Token: 0x02000008 RID: 8
	public class InputManager : MonoBehaviour
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000020EF File Offset: 0x000002EF
		private void Start()
		{
			InputManager.instance = this;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002FE0 File Offset: 0x000011E0
		private void Update()
		{
			this.LeftGrip = ControllerInputPoller.instance.leftGrab;
			this.RightGrip = ControllerInputPoller.instance.rightGrab;
			this.LeftPrimaryButton = SteamVR_Actions.gorillaTag_LeftSecondaryClick.stateDown;
			this.RightPrimaryButton = SteamVR_Actions.gorillaTag_RightSecondaryClick.stateDown;
			if (Gamepad.current != null)
			{
				this.GPLeftStick = Gamepad.current.leftStick.ReadValue();
				this.GPRightStick = Gamepad.current.rightStick.ReadValue();
			}
		}

		// Token: 0x0400003A RID: 58
		public static InputManager instance;

		// Token: 0x0400003B RID: 59
		private XRNode lHandNode = 4;

		// Token: 0x0400003C RID: 60
		private XRNode rHandNode = 5;

		// Token: 0x0400003D RID: 61
		public bool LeftGrip;

		// Token: 0x0400003E RID: 62
		public bool RightGrip;

		// Token: 0x0400003F RID: 63
		public bool LeftPrimaryButton;

		// Token: 0x04000040 RID: 64
		public bool RightPrimaryButton;

		// Token: 0x04000041 RID: 65
		public Vector2 GPLeftStick;

		// Token: 0x04000042 RID: 66
		public Vector2 GPRightStick;
	}
}
