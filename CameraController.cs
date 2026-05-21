using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using GorillaLocomotion;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using YizziCamModV2.Comps;

namespace YizziCamModV2
{
	// Token: 0x02000002 RID: 2
	public class CameraController : MonoBehaviour
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private void Awake()
		{
			CameraController.Instance = this;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002174 File Offset: 0x00000374
		public void YizziStart()
		{
			base.gameObject.AddComponent<InputManager>().gameObject.AddComponent<UI>();
			this.ColorScreenGO = this.LoadBundle("ColorScreen", "YizziCamModV2.Assets.colorscreen");
			this.CameraTablet = this.LoadBundle("CameraTablet", "YizziCamModV2.Assets.yizzicam");
			this.FirstPersonCameraGO = GorillaTagger.Instance.mainCamera;
			this.ThirdPersonCameraGO = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera");
			this.CMVirtualCameraGO = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera/CM vcam1");
			this.TPVBodyFollower = GorillaTagger.Instance.bodyCollider.gameObject;
			this.CMVirtualCamera = this.CMVirtualCameraGO.GetComponent<CinemachineVirtualCamera>();
			this.FirstPersonCamera = this.FirstPersonCameraGO.GetComponent<Camera>();
			this.ThirdPersonCamera = this.ThirdPersonCameraGO.GetComponent<Camera>();
			this.LeftHandGO = GorillaTagger.Instance.leftHandTransform.gameObject;
			this.RightHandGO = GorillaTagger.Instance.rightHandTransform.gameObject;
			this.CameraTablet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			this.CameraFollower = GameObject.Find("Player Objects/Player VR Controller/GorillaPlayer/TurnParent/Main Camera/Camera Follower");
			this.TabletCameraGO = GameObject.Find("CameraTablet(Clone)/Camera");
			this.TabletCamera = this.TabletCameraGO.GetComponent<Camera>();
			this.FakeWebCam = GameObject.Find("CameraTablet(Clone)/FakeCamera");
			this.LeftGrabCol = GameObject.Find("CameraTablet(Clone)/LeftGrabCol");
			this.RightGrabCol = GameObject.Find("CameraTablet(Clone)/RightGrabCol");
			this.LeftGrabCol.AddComponent<LeftGrabTrigger>();
			this.RightGrabCol.AddComponent<RightGrabTrigger>();
			this.MainPage = GameObject.Find("CameraTablet(Clone)/MainPage");
			this.MiscPage = GameObject.Find("CameraTablet(Clone)/MiscPage");
			this.FovText = GameObject.Find("CameraTablet(Clone)/MainPage/Canvas/FovValueText").GetComponent<Text>();
			this.SmoothText = GameObject.Find("CameraTablet(Clone)/MainPage/Canvas/SmoothingValueText").GetComponent<Text>();
			this.NearClipText = GameObject.Find("CameraTablet(Clone)/MainPage/Canvas/NearClipValueText").GetComponent<Text>();
			this.MinDistText = GameObject.Find("CameraTablet(Clone)/MiscPage/Canvas/MinDistValueText").GetComponent<Text>();
			this.SpeedText = GameObject.Find("CameraTablet(Clone)/MiscPage/Canvas/SpeedValueText").GetComponent<Text>();
			this.TPText = GameObject.Find("CameraTablet(Clone)/MiscPage/Canvas/TPText").GetComponent<Text>();
			this.TPRotText = GameObject.Find("CameraTablet(Clone)/MiscPage/Canvas/TPRotText").GetComponent<Text>();
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/MiscButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/FPVButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/FovUP"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/FovDown"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/FlipCamButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/NearClipUp"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/NearClipDown"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/FPButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/ControlsButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/TPVButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/SmoothingDownButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MainPage/SmoothingUpButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/BackButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/GreenScreenButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/MinDistDownButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/MinDistUpButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/SpeedDownButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/SpeedUpButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/SpeedDownButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/TPModeDownButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/TPModeUpButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/TPRotButton"));
			this.Buttons.Add(GameObject.Find("CameraTablet(Clone)/MiscPage/TPRotButton1"));
			foreach (GameObject gameObject in this.Buttons)
			{
				gameObject.AddComponent<YzGButton>();
			}
			this.CMVirtualCamera.enabled = false;
			this.ThirdPersonCameraGO.transform.SetParent(this.CameraTablet.transform, true);
			this.CameraTablet.transform.position = new Vector3(-65f, 12f, -82f);
			this.ThirdPersonCameraGO.transform.position = this.TabletCamera.transform.position;
			this.ThirdPersonCameraGO.transform.rotation = this.TabletCamera.transform.rotation;
			this.CameraTablet.transform.Rotate(0f, 180f, 0f);
			this.ColorScreenText = GameObject.Find("CameraTablet(Clone)/MiscPage/Canvas/ColorScreenText").GetComponent<Text>();
			this.ColorButtons.Add(GameObject.Find("ColorScreen(Clone)/Stuff/RedButton"));
			this.ColorButtons.Add(GameObject.Find("ColorScreen(Clone)/Stuff/GreenButton"));
			this.ColorButtons.Add(GameObject.Find("ColorScreen(Clone)/Stuff/BlueButton"));
			foreach (GameObject gameObject2 in this.ColorButtons)
			{
				gameObject2.AddComponent<YzGButton>();
			}
			this.ScreenMats.Add(GameObject.Find("ColorScreen(Clone)/Screen1").GetComponent<MeshRenderer>().material);
			this.ScreenMats.Add(GameObject.Find("ColorScreen(Clone)/Screen2").GetComponent<MeshRenderer>().material);
			this.ScreenMats.Add(GameObject.Find("ColorScreen(Clone)/Screen3").GetComponent<MeshRenderer>().material);
			this.meshRenderers.Add(GameObject.Find("CameraTablet(Clone)/FakeCamera").GetComponent<MeshRenderer>());
			this.meshRenderers.Add(GameObject.Find("CameraTablet(Clone)/Tablet").GetComponent<MeshRenderer>());
			this.meshRenderers.Add(GameObject.Find("CameraTablet(Clone)/Handle").GetComponent<MeshRenderer>());
			this.meshRenderers.Add(GameObject.Find("CameraTablet(Clone)/Handle2").GetComponent<MeshRenderer>());
			this.ColorScreenGO.transform.position = new Vector3(-54.3f, 16.21f, -122.96f);
			this.ColorScreenGO.transform.Rotate(0f, 30f, 0f);
			this.ColorScreenGO.SetActive(false);
			this.MiscPage.SetActive(false);
			this.ThirdPersonCamera.nearClipPlane = 0.1f;
			this.TabletCamera.nearClipPlane = 0.1f;
			this.FakeWebCam.transform.Rotate(-180f, 180f, 0f);
			this.init = true;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002884 File Offset: 0x00000A84
		private void LateUpdate()
		{
			if (this.init)
			{
				if (this.fpv)
				{
					if (this.MainPage.active)
					{
						foreach (MeshRenderer meshRenderer in this.meshRenderers)
						{
							meshRenderer.enabled = false;
						}
						this.MainPage.active = false;
					}
					float num = Mathf.Clamp01(this.smoothing * 9f);
					float num2 = Mathf.Clamp01((Mathf.Lerp(0.11f, 0.05f, Mathf.Pow(num, 2f)) - 0.008740158f) / 0.010488189f);
					float num3 = Mathf.Lerp(0.12f, 0.05f, num2);
					this.CameraTablet.transform.position = ((GTPlayer.Instance.LeftHand.wasColliding || GTPlayer.Instance.RightHand.wasColliding) ? Vector3.SmoothDamp(this.CameraTablet.transform.position, this.CameraFollower.transform.position, ref this.velocity, num3 * (Time.deltaTime * 26.75f)) : this.CameraFollower.transform.position);
					this.CameraTablet.transform.rotation = Quaternion.Lerp(this.CameraTablet.transform.rotation, this.CameraFollower.transform.rotation, this.smoothing);
				}
				if (InputManager.instance.LeftPrimaryButton && this.CameraTablet.transform.parent == null)
				{
					this.fp = false;
					this.fpv = false;
					this.tpv = false;
					if (!this.MainPage.active)
					{
						foreach (GameObject gameObject in this.Buttons)
						{
							gameObject.SetActive(true);
						}
						foreach (MeshRenderer meshRenderer2 in this.meshRenderers)
						{
							meshRenderer2.enabled = true;
							this.CameraTablet.transform.Rotate(0f, -180f, 0f);
						}
						this.MainPage.active = true;
					}
					this.CameraTablet.transform.position = GTPlayer.Instance.headCollider.transform.position + GTPlayer.Instance.headCollider.transform.forward;
				}
				if (this.fp)
				{
					this.CameraTablet.transform.LookAt(2f * this.CameraTablet.transform.position - this.CameraFollower.transform.position);
					if (!this.flipped)
					{
						this.flipped = true;
						this.ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
						this.TabletCameraGO.transform.Rotate(0f, 180f, 0f);
						this.FakeWebCam.transform.Rotate(-180f, 180f, 0f);
					}
					this.dist = Vector3.Distance(this.CameraFollower.transform.position, this.CameraTablet.transform.position);
					if (this.dist > this.minDist)
					{
						this.CameraTablet.transform.position = Vector3.Lerp(this.CameraTablet.transform.position, this.CameraFollower.transform.position, this.fpspeed);
					}
				}
				if (this.tpv)
				{
					if (this.MainPage.active)
					{
						foreach (MeshRenderer meshRenderer3 in this.meshRenderers)
						{
							meshRenderer3.enabled = false;
						}
						this.MainPage.active = false;
					}
					if (this.TPVMode == CameraController.TPVModes.BACK)
					{
						if (this.followheadrot)
						{
							this.targetPosition = this.CameraFollower.transform.TransformPoint(new Vector3(0.3f, 0.1f, -1.5f));
						}
						else
						{
							this.targetPosition = this.TPVBodyFollower.transform.TransformPoint(new Vector3(0.3f, 0.1f, -1.5f));
						}
						this.CameraTablet.transform.position = Vector3.SmoothDamp(this.CameraTablet.transform.position, this.targetPosition, ref this.velocity, 0.1f);
						this.CameraTablet.transform.LookAt(this.CameraFollower.transform.position);
					}
					else if (this.TPVMode == CameraController.TPVModes.FRONT)
					{
						if (this.followheadrot)
						{
							this.targetPosition = this.CameraFollower.transform.TransformPoint(new Vector3(0.1f, 0.3f, 2.5f));
						}
						else
						{
							this.targetPosition = this.TPVBodyFollower.transform.TransformPoint(new Vector3(0.1f, 0.3f, 2.5f));
						}
						this.CameraTablet.transform.position = Vector3.SmoothDamp(this.CameraTablet.transform.position, this.targetPosition, ref this.velocity, 0.1f);
						this.CameraTablet.transform.LookAt(2f * this.CameraTablet.transform.position - this.CameraFollower.transform.position);
					}
					if (InputManager.instance.LeftPrimaryButton)
					{
						this.CameraTablet.transform.position = GTPlayer.Instance.headCollider.transform.position + GTPlayer.Instance.headCollider.transform.forward;
						foreach (MeshRenderer meshRenderer4 in this.meshRenderers)
						{
							meshRenderer4.enabled = true;
						}
						this.CameraTablet.transform.parent = null;
						this.tpv = false;
					}
				}
				this.fov = Camera.main.fieldOfView;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002F28 File Offset: 0x00001128
		private GameObject LoadBundle(string goname, string resourcename)
		{
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcename);
			AssetBundle assetBundle = AssetBundle.LoadFromStream(manifestResourceStream);
			GameObject result = Object.Instantiate<GameObject>(assetBundle.LoadAsset<GameObject>(goname));
			assetBundle.Unload(false);
			manifestResourceStream.Close();
			return result;
		}

		// Token: 0x04000001 RID: 1
		public static CameraController Instance;

		// Token: 0x04000002 RID: 2
		public GameObject CameraTablet;

		// Token: 0x04000003 RID: 3
		public GameObject FirstPersonCameraGO;

		// Token: 0x04000004 RID: 4
		public GameObject ThirdPersonCameraGO;

		// Token: 0x04000005 RID: 5
		public GameObject CMVirtualCameraGO;

		// Token: 0x04000006 RID: 6
		public GameObject LeftHandGO;

		// Token: 0x04000007 RID: 7
		public GameObject RightHandGO;

		// Token: 0x04000008 RID: 8
		public GameObject FakeWebCam;

		// Token: 0x04000009 RID: 9
		public GameObject TabletCameraGO;

		// Token: 0x0400000A RID: 10
		public GameObject MainPage;

		// Token: 0x0400000B RID: 11
		public GameObject MiscPage;

		// Token: 0x0400000C RID: 12
		public GameObject LeftGrabCol;

		// Token: 0x0400000D RID: 13
		public GameObject RightGrabCol;

		// Token: 0x0400000E RID: 14
		public GameObject CameraFollower;

		// Token: 0x0400000F RID: 15
		public GameObject TPVBodyFollower;

		// Token: 0x04000010 RID: 16
		public GameObject ColorScreenGO;

		// Token: 0x04000011 RID: 17
		public List<GameObject> Buttons = new List<GameObject>();

		// Token: 0x04000012 RID: 18
		public List<GameObject> ColorButtons = new List<GameObject>();

		// Token: 0x04000013 RID: 19
		public List<Material> ScreenMats = new List<Material>();

		// Token: 0x04000014 RID: 20
		public List<MeshRenderer> meshRenderers = new List<MeshRenderer>();

		// Token: 0x04000015 RID: 21
		public Camera TabletCamera;

		// Token: 0x04000016 RID: 22
		public Camera FirstPersonCamera;

		// Token: 0x04000017 RID: 23
		public Camera ThirdPersonCamera;

		// Token: 0x04000018 RID: 24
		public Text FovText;

		// Token: 0x04000019 RID: 25
		public Text NearClipText;

		// Token: 0x0400001A RID: 26
		public Text ColorScreenText;

		// Token: 0x0400001B RID: 27
		public Text MinDistText;

		// Token: 0x0400001C RID: 28
		public Text SpeedText;

		// Token: 0x0400001D RID: 29
		public Text SmoothText;

		// Token: 0x0400001E RID: 30
		public Text TPText;

		// Token: 0x0400001F RID: 31
		public Text TPRotText;

		// Token: 0x04000020 RID: 32
		public bool followheadrot = true;

		// Token: 0x04000021 RID: 33
		public bool canbeused;

		// Token: 0x04000022 RID: 34
		public bool flipped;

		// Token: 0x04000023 RID: 35
		public bool tpv;

		// Token: 0x04000024 RID: 36
		public bool fpv;

		// Token: 0x04000025 RID: 37
		public bool fp;

		// Token: 0x04000026 RID: 38
		public bool openedurl;

		// Token: 0x04000027 RID: 39
		public float minDist = 2f;

		// Token: 0x04000028 RID: 40
		private float dist;

		// Token: 0x04000029 RID: 41
		public float fpspeed = 0.01f;

		// Token: 0x0400002A RID: 42
		public float smoothing = 0.16f;

		// Token: 0x0400002B RID: 43
		private Vector3 targetPosition;

		// Token: 0x0400002C RID: 44
		private Vector3 velocity = Vector3.zero;

		// Token: 0x0400002D RID: 45
		public CameraController.TPVModes TPVMode;

		// Token: 0x0400002E RID: 46
		private bool init;

		// Token: 0x0400002F RID: 47
		public float fov = 0.01f;

		// Token: 0x04000030 RID: 48
		public CinemachineVirtualCamera CMVirtualCamera;

		// Token: 0x02000003 RID: 3
		public enum TPVModes
		{
			// Token: 0x04000032 RID: 50
			BACK,
			// Token: 0x04000033 RID: 51
			FRONT
		}
	}
}
