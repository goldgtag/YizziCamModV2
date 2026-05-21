using System;
using UnityEngine;

namespace YizziCamModV2.Comps
{
	// Token: 0x02000009 RID: 9
	internal class LeftGrabTrigger : MonoBehaviour
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000210D File Offset: 0x0000030D
		private void Start()
		{
			base.gameObject.layer = 18;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003064 File Offset: 0x00001264
		private void OnTriggerStay(Collider col)
		{
			if (col.name.Contains("Left") && (InputManager.instance.LeftGrip & !CameraController.Instance.fpv))
			{
				CameraController.Instance.CameraTablet.transform.parent = CameraController.Instance.LeftHandGO.transform;
				if (CameraController.Instance.fp)
				{
					CameraController.Instance.fp = false;
				}
			}
			if (!InputManager.instance.LeftGrip & CameraController.Instance.CameraTablet.transform.parent == CameraController.Instance.LeftHandGO.transform)
			{
				CameraController.Instance.CameraTablet.transform.parent = null;
			}
		}
	}
}
