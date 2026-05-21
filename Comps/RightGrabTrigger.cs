using System;
using UnityEngine;

namespace YizziCamModV2.Comps
{
	// Token: 0x0200000A RID: 10
	internal class RightGrabTrigger : MonoBehaviour
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000210D File Offset: 0x0000030D
		private void Start()
		{
			base.gameObject.layer = 18;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003124 File Offset: 0x00001324
		private void OnTriggerStay(Collider col)
		{
			if (col.name.Contains("Right") && (InputManager.instance.RightGrip & !CameraController.Instance.fpv))
			{
				CameraController.Instance.CameraTablet.transform.parent = CameraController.Instance.RightHandGO.transform;
				if (CameraController.Instance.fp)
				{
					CameraController.Instance.fp = false;
				}
			}
			if (!InputManager.instance.RightGrip & CameraController.Instance.CameraTablet.transform.parent == CameraController.Instance.RightHandGO.transform)
			{
				CameraController.Instance.CameraTablet.transform.parent = null;
			}
		}
	}
}
