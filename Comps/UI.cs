using System;
using GorillaLocomotion;
using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

namespace YizziCamModV2.Comps
{
	// Token: 0x0200000B RID: 11
	internal class UI : MonoBehaviour
	{
		// Token: 0x06000019 RID: 25 RVA: 0x000031E4 File Offset: 0x000013E4
		private void Start()
		{
			this.rigcache = GameObject.Find("Player Objects/RigCache/Rig Parent");
			this.forest = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest");
			this.city = GameObject.Find("Environment Objects/LocalObjects_Prefab/City");
			this.canyon = GameObject.Find("Environment Objects/LocalObjects_Prefab/Canyon");
			this.cave = GameObject.Find("Environment Objects/LocalObjects_Prefab/Cave_Main_Prefab");
			this.mountain = GameObject.Find("Environment Objects/LocalObjects_Prefab/Mountain");
			this.clouds = GameObject.Find("Environment Objects/LocalObjects_Prefab/skyjungle");
			this.cloudsbottom = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Sky Jungle Bottom (1)/CloudSmall (22)");
			this.beach = GameObject.Find("Environment Objects/LocalObjects_Prefab/Beach");
			this.beachthing = GameObject.Find("Environment Objects/LocalObjects_Prefab/ForestToBeach");
			this.basement = GameObject.Find("Environment Objects/LocalObjects_Prefab/Basement");
			this.citybuildings = GameObject.Find("Environment Objects/LocalObjects_Prefab/City/CosmeticsRoomAnchor/rain");
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000032B4 File Offset: 0x000014B4
		private void OnGUI()
		{
			if (this.uiopen)
			{
				GUI.Box(new Rect(30f, 50f, 170f, 270f), "Yizzi's Camera Mod");
				if (GUI.Button(new Rect(35f, 70f, 160f, 20f), "FreeCam"))
				{
					if (this.spectating)
					{
						this.spectating = false;
						this.followobject = null;
					}
					if (this.freecam)
					{
						CameraController.Instance.CameraTablet.transform.position = GTPlayer.Instance.headCollider.transform.position + GTPlayer.Instance.headCollider.transform.forward;
					}
					if (!CameraController.Instance.flipped)
					{
						CameraController.Instance.flipped = true;
						CameraController.Instance.ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
						CameraController.Instance.TabletCameraGO.transform.Rotate(0f, 180f, 0f);
						CameraController.Instance.FakeWebCam.transform.Rotate(-180f, 180f, 0f);
					}
					CameraController.Instance.fpv = false;
					CameraController.Instance.fp = false;
					CameraController.Instance.tpv = false;
					this.freecam = !this.freecam;
				}
				if (GUI.Button(new Rect(35f, 90f, 100f, 20f), "Spectator"))
				{
					if (!this.freecam && PhotonNetwork.InRoom)
					{
						this.specui = !this.specui;
					}
					CameraController.Instance.fpv = false;
					CameraController.Instance.fp = false;
					CameraController.Instance.tpv = false;
				}
				if (GUI.Button(new Rect(140f, 90f, 45f, 20f), "Stop") && this.spectating)
				{
					this.followobject = null;
					CameraController.Instance.CameraTablet.transform.position = GTPlayer.Instance.headCollider.transform.position + GTPlayer.Instance.headCollider.transform.forward;
					this.spectating = false;
				}
				if (GUI.Button(new Rect(35f, 110f, 160f, 20f), "Load All Maps(PRIVS)") && !PhotonNetwork.CurrentRoom.IsVisible)
				{
					this.forest.SetActive(true);
					this.cave.SetActive(true);
					this.canyon.SetActive(true);
					this.beach.SetActive(true);
					this.beachthing.SetActive(true);
					this.city.SetActive(true);
					this.mountain.SetActive(true);
					this.basement.SetActive(true);
					this.clouds.SetActive(true);
					this.cloudsbottom.SetActive(false);
					this.citybuildings.SetActive(false);
				}
				if (this.specui)
				{
					int num = 1;
					foreach (VRRig vrrig in this.rigcache.GetComponentsInChildren<VRRig>())
					{
						if (vrrig.transform.parent.gameObject.active)
						{
							GUI.Label(new Rect(250f, (float)(20 + num * 25), 160f, 20f), vrrig.playerNameVisible);
							if (GUI.Button(new Rect(360f, (float)(20 + num * 25), 67f, 20f), "Spectate"))
							{
								this.followobject = vrrig.gameObject;
								this.spectating = true;
								CameraController.Instance.fp = false;
								CameraController.Instance.fpv = false;
								CameraController.Instance.tpv = false;
								if (CameraController.Instance.flipped)
								{
									CameraController.Instance.flipped = false;
									CameraController.Instance.ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
									CameraController.Instance.TabletCameraGO.transform.Rotate(0f, 180f, 0f);
									CameraController.Instance.FakeWebCam.transform.Rotate(-180f, 180f, 0f);
								}
							}
						}
						num++;
					}
				}
				this.controllerfreecam = GUI.Toggle(new Rect(30f, 130f, 160f, 19f), this.controllerfreecam, "Controller Freecam");
				this.controloffset = GUI.Toggle(new Rect(30f, 150f, 170f, 19f), this.controloffset, "Control Offset with WASD");
				this.speclookat = GUI.Toggle(new Rect(30f, 170f, 170f, 19f), this.speclookat, "Spectator Stare");
				GUI.Label(new Rect(35f, 188f, 160f, 30f), "         Spectator Offset:");
				GUI.Label(new Rect(35f, 200f, 160f, 30f), "     X            Y            Z");
				this.specoffset.x = GUI.HorizontalSlider(new Rect(35f, 215f, 50f, 20f), this.specoffset.x, -3f, 3f);
				this.specoffset.y = GUI.HorizontalSlider(new Rect(90f, 215f, 50f, 20f), this.specoffset.y, -3f, 3f);
				this.specoffset.z = GUI.HorizontalSlider(new Rect(145f, 215f, 50f, 20f), this.specoffset.z, -3f, 3f);
				GUI.Label(new Rect(35f, 232f, 160f, 30f), "          Freecam Speed");
				this.freecamspeed = GUI.HorizontalSlider(new Rect(35f, 250f, 160f, 5f), this.freecamspeed, 0.01f, 0.4f);
				GUI.Label(new Rect(35f, 258f, 160f, 20f), "0                0.5               1");
				GUI.Label(new Rect(35f, 275f, 160f, 30f), "          Freecam Sens");
				this.freecamsens = GUI.HorizontalSlider(new Rect(35f, 293f, 160f, 5f), this.freecamsens, 0.01f, 2f);
				GUI.Label(new Rect(35f, 301f, 160f, 20f), "0                0.5               1");
				if (!PhotonNetwork.InRoom)
				{
					this.specui = false;
					this.followobject = null;
				}
			}
			if (Keyboard.current.tabKey.isPressed)
			{
				if (!this.keyp)
				{
					this.uiopen = !this.uiopen;
				}
				this.keyp = true;
				return;
			}
			this.keyp = false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002124 File Offset: 0x00000324
		private void LateUpdate()
		{
			this.Spec();
			this.Freecam();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000039E4 File Offset: 0x00001BE4
		private void Freecam()
		{
			if (this.freecam && !this.controllerfreecam)
			{
				if (Keyboard.current.wKey.isPressed)
				{
					CameraController.Instance.CameraTablet.transform.position -= CameraController.Instance.CameraTablet.transform.forward * this.freecamspeed;
				}
				if (Keyboard.current.aKey.isPressed)
				{
					CameraController.Instance.CameraTablet.transform.position += CameraController.Instance.CameraTablet.transform.right * this.freecamspeed;
				}
				if (Keyboard.current.sKey.isPressed)
				{
					CameraController.Instance.CameraTablet.transform.position += CameraController.Instance.CameraTablet.transform.forward * this.freecamspeed;
				}
				if (Keyboard.current.dKey.isPressed)
				{
					CameraController.Instance.CameraTablet.transform.position -= CameraController.Instance.CameraTablet.transform.right * this.freecamspeed;
				}
				if (Keyboard.current.qKey.isPressed)
				{
					CameraController.Instance.CameraTablet.transform.position -= CameraController.Instance.CameraTablet.transform.up * this.freecamspeed;
				}
				if (Keyboard.current.eKey.isPressed)
				{
					CameraController.Instance.CameraTablet.transform.position += CameraController.Instance.CameraTablet.transform.up * this.freecamspeed;
				}
				if (Keyboard.current.leftArrowKey.isPressed)
				{
					CameraController.Instance.CameraTablet.transform.eulerAngles += new Vector3(0f, -this.freecamsens, 0f);
				}
				if (Keyboard.current.rightArrowKey.isPressed)
				{
					CameraController.Instance.CameraTablet.transform.eulerAngles += new Vector3(0f, this.freecamsens, 0f);
				}
				if (Keyboard.current.upArrowKey.isPressed)
				{
					CameraController.Instance.CameraTablet.transform.eulerAngles += new Vector3(this.freecamsens, 0f, 0f);
				}
				if (Keyboard.current.downArrowKey.isPressed)
				{
					CameraController.Instance.CameraTablet.transform.eulerAngles += new Vector3(-this.freecamsens, 0f, 0f);
				}
			}
			if (this.freecam && this.controllerfreecam)
			{
				float x = InputManager.instance.GPLeftStick.x;
				float y = InputManager.instance.GPLeftStick.y;
				this.rotX += InputManager.instance.GPRightStick.x * this.freecamsens;
				this.rotY += InputManager.instance.GPRightStick.y * this.freecamsens;
				Vector3 vector;
				vector..ctor(-x, this.posY, -y);
				CameraController.Instance.CameraTablet.transform.Translate(vector * this.freecamspeed);
				this.rotY = Mathf.Clamp(this.rotY, -90f, 90f);
				CameraController.Instance.CameraTablet.transform.rotation = Quaternion.Euler(this.rotY, this.rotX, 0f);
				if (Gamepad.current.rightShoulder.isPressed)
				{
					this.posY = 3f * this.freecamspeed;
					return;
				}
				if (Gamepad.current.leftShoulder.isPressed)
				{
					this.posY = -3f * this.freecamspeed;
					return;
				}
				this.posY = 0f;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003E3C File Offset: 0x0000203C
		private void Spec()
		{
			if (this.followobject != null)
			{
				Vector3 vector = this.followobject.transform.TransformPoint(this.specoffset);
				CameraController.Instance.CameraTablet.transform.position = Vector3.SmoothDamp(CameraController.Instance.CameraTablet.transform.position, vector, ref this.velocity, 0.2f);
				if (this.speclookat)
				{
					Quaternion quaternion = Quaternion.LookRotation(this.followobject.transform.position - CameraController.Instance.CameraTablet.transform.position);
					CameraController.Instance.CameraTablet.transform.rotation = Quaternion.Lerp(CameraController.Instance.CameraTablet.transform.rotation, quaternion, 0.2f);
				}
				else
				{
					CameraController.Instance.CameraTablet.transform.rotation = Quaternion.Lerp(CameraController.Instance.CameraTablet.transform.rotation, this.followobject.transform.rotation, 0.2f);
				}
				if (this.controloffset)
				{
					if (Keyboard.current.wKey.isPressed)
					{
						if ((double)this.specoffset.z >= 3.01)
						{
							this.specoffset.z = 3f;
						}
						this.specoffset.z = this.specoffset.z + 0.02f;
					}
					if (Keyboard.current.aKey.isPressed)
					{
						if ((double)this.specoffset.x <= -3.01)
						{
							this.specoffset.x = -3f;
						}
						this.specoffset.x = this.specoffset.x - 0.02f;
					}
					if (Keyboard.current.sKey.isPressed)
					{
						if ((double)this.specoffset.z <= -3.01)
						{
							this.specoffset.z = -3f;
						}
						this.specoffset.z = this.specoffset.z - 0.02f;
					}
					if (Keyboard.current.dKey.isPressed)
					{
						if ((double)this.specoffset.x >= 3.01)
						{
							this.specoffset.x = 3f;
						}
						this.specoffset.x = this.specoffset.x + 0.02f;
					}
					if (Keyboard.current.qKey.isPressed)
					{
						if ((double)this.specoffset.y <= -3.01)
						{
							this.specoffset.y = -3f;
						}
						this.specoffset.y = this.specoffset.y - 0.02f;
					}
					if (Keyboard.current.eKey.isPressed)
					{
						if ((double)this.specoffset.y >= 3.01)
						{
							this.specoffset.y = 3f;
						}
						this.specoffset.y = this.specoffset.y + 0.02f;
						return;
					}
				}
			}
			else if (this.spectating)
			{
				CameraController.Instance.CameraTablet.transform.position = GTPlayer.Instance.headCollider.transform.position + GTPlayer.Instance.headCollider.transform.forward;
				this.spectating = false;
			}
		}

		// Token: 0x04000043 RID: 67
		public GameObject forest;

		// Token: 0x04000044 RID: 68
		public GameObject cave;

		// Token: 0x04000045 RID: 69
		public GameObject canyon;

		// Token: 0x04000046 RID: 70
		public GameObject mountain;

		// Token: 0x04000047 RID: 71
		public GameObject city;

		// Token: 0x04000048 RID: 72
		public GameObject clouds;

		// Token: 0x04000049 RID: 73
		public GameObject cloudsbottom;

		// Token: 0x0400004A RID: 74
		public GameObject beach;

		// Token: 0x0400004B RID: 75
		public GameObject beachthing;

		// Token: 0x0400004C RID: 76
		public GameObject basement;

		// Token: 0x0400004D RID: 77
		public GameObject citybuildings;

		// Token: 0x0400004E RID: 78
		private GameObject rigcache;

		// Token: 0x0400004F RID: 79
		private bool keyp;

		// Token: 0x04000050 RID: 80
		private bool uiopen;

		// Token: 0x04000051 RID: 81
		private bool specui;

		// Token: 0x04000052 RID: 82
		private bool freecam;

		// Token: 0x04000053 RID: 83
		private bool spectating;

		// Token: 0x04000054 RID: 84
		private bool controllerfreecam;

		// Token: 0x04000055 RID: 85
		private bool speclookat;

		// Token: 0x04000056 RID: 86
		private bool controloffset;

		// Token: 0x04000057 RID: 87
		private GameObject followobject;

		// Token: 0x04000058 RID: 88
		private float freecamspeed = 0.1f;

		// Token: 0x04000059 RID: 89
		private float freecamsens = 1f;

		// Token: 0x0400005A RID: 90
		private float rotX;

		// Token: 0x0400005B RID: 91
		private float rotY;

		// Token: 0x0400005C RID: 92
		private float posY;

		// Token: 0x0400005D RID: 93
		private Vector3 specoffset = new Vector3(0.3f, 0.1f, -1.5f);

		// Token: 0x0400005E RID: 94
		private Vector3 velocity = Vector3.zero;

		// Token: 0x0400005F RID: 95
		private float fov = 0.001f;
	}
}
