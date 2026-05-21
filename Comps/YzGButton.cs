using System;
using System.Collections.Generic;
using UnityEngine;

namespace YizziCamModV2.Comps
{
	// Token: 0x0200000C RID: 12
	internal class YzGButton : MonoBehaviour
	{
		// Token: 0x0600001F RID: 31 RVA: 0x0000210D File Offset: 0x0000030D
		private void Start()
		{
			base.gameObject.layer = 18;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002132 File Offset: 0x00000332
		private void OnEnable()
		{
			base.Invoke("ButtonTimer", 1f);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002144 File Offset: 0x00000344
		private void OnDisable()
		{
			CameraController.Instance.canbeused = false;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002151 File Offset: 0x00000351
		private void ButtonTimer()
		{
			if (!base.enabled)
			{
				CameraController.Instance.canbeused = false;
			}
			CameraController.Instance.canbeused = true;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000420C File Offset: 0x0000240C
		private void OnTriggerEnter(Collider col)
		{
			if (CameraController.Instance.canbeused && (col.name == "RightHandTriggerCollider" | col.name == "LeftHandTriggerCollider"))
			{
				CameraController.Instance.canbeused = false;
				base.Invoke("ButtonTimer", 1f);
				string name = base.name;
				if (name != null)
				{
					switch (name.Length)
					{
					case 5:
						if (!(name == "FovUP"))
						{
							return;
						}
						CameraController.Instance.TabletCamera.fieldOfView += 5f;
						if (CameraController.Instance.TabletCamera.fieldOfView > 130f)
						{
							CameraController.Instance.TabletCamera.fieldOfView = 20f;
							CameraController.Instance.ThirdPersonCamera.fieldOfView = 20f;
						}
						CameraController.Instance.ThirdPersonCamera.fieldOfView = CameraController.Instance.TabletCamera.fieldOfView;
						CameraController.Instance.FovText.text = CameraController.Instance.TabletCamera.fieldOfView.ToString();
						CameraController.Instance.canbeused = true;
						return;
					case 6:
					case 18:
						return;
					case 7:
						if (!(name == "FovDown"))
						{
							return;
						}
						CameraController.Instance.TabletCamera.fieldOfView -= 5f;
						if (CameraController.Instance.TabletCamera.fieldOfView < 20f)
						{
							CameraController.Instance.TabletCamera.fieldOfView = 130f;
							CameraController.Instance.ThirdPersonCamera.fieldOfView = 130f;
						}
						CameraController.Instance.ThirdPersonCamera.fieldOfView = CameraController.Instance.TabletCamera.fieldOfView;
						CameraController.Instance.FovText.text = CameraController.Instance.TabletCamera.fieldOfView.ToString();
						CameraController.Instance.canbeused = true;
						return;
					case 8:
						if (!(name == "FPButton"))
						{
							return;
						}
						CameraController.Instance.fp = !CameraController.Instance.fp;
						return;
					case 9:
					{
						char c = name.get_Chars(0);
						if (c != 'F')
						{
							if (c != 'R')
							{
								if (c != 'T')
								{
									return;
								}
								if (!(name == "TPVButton"))
								{
									return;
								}
								if (CameraController.Instance.TPVMode == CameraController.TPVModes.BACK)
								{
									if (CameraController.Instance.flipped)
									{
										CameraController.Instance.flipped = false;
										CameraController.Instance.ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
										CameraController.Instance.TabletCameraGO.transform.Rotate(0f, 180f, 0f);
										CameraController.Instance.FakeWebCam.transform.Rotate(-180f, 180f, 0f);
									}
								}
								else if (CameraController.Instance.TPVMode == CameraController.TPVModes.FRONT && !CameraController.Instance.flipped)
								{
									CameraController.Instance.flipped = true;
									CameraController.Instance.ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
									CameraController.Instance.TabletCameraGO.transform.Rotate(0f, 180f, 0f);
									CameraController.Instance.FakeWebCam.transform.Rotate(-180f, 180f, 0f);
								}
								CameraController.Instance.fp = false;
								CameraController.Instance.fpv = false;
								CameraController.Instance.tpv = true;
								return;
							}
							else
							{
								if (!(name == "RedButton"))
								{
									return;
								}
								using (List<Material>.Enumerator enumerator = CameraController.Instance.ScreenMats.GetEnumerator())
								{
									while (enumerator.MoveNext())
									{
										Material material = enumerator.Current;
										material.color = Color.red;
									}
									return;
								}
							}
						}
						else
						{
							if (!(name == "FPVButton"))
							{
								return;
							}
							if (CameraController.Instance.flipped)
							{
								CameraController.Instance.flipped = false;
								CameraController.Instance.ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
								CameraController.Instance.TabletCameraGO.transform.Rotate(0f, 180f, 0f);
								CameraController.Instance.FakeWebCam.transform.Rotate(-180f, 180f, 0f);
							}
							CameraController.Instance.fp = false;
							CameraController.Instance.fpv = true;
							return;
						}
						break;
					}
					case 10:
					{
						char c = name.get_Chars(1);
						if (c <= 'e')
						{
							if (c != 'a')
							{
								if (c != 'e')
								{
									return;
								}
								if (!(name == "NearClipUp"))
								{
									return;
								}
								CameraController.Instance.TabletCamera.nearClipPlane += 0.01f;
								if ((double)CameraController.Instance.TabletCamera.nearClipPlane > 1.0)
								{
									CameraController.Instance.TabletCamera.nearClipPlane = 0.01f;
									CameraController.Instance.ThirdPersonCamera.nearClipPlane = 0.01f;
								}
								CameraController.Instance.ThirdPersonCamera.nearClipPlane = CameraController.Instance.TabletCamera.nearClipPlane;
								CameraController.Instance.NearClipText.text = CameraController.Instance.TabletCamera.nearClipPlane.ToString();
								CameraController.Instance.canbeused = true;
								return;
							}
							else
							{
								if (!(name == "BackButton"))
								{
									return;
								}
								CameraController.Instance.MainPage.SetActive(true);
								CameraController.Instance.MiscPage.SetActive(false);
								return;
							}
						}
						else if (c != 'i')
						{
							if (c != 'l')
							{
								return;
							}
							if (!(name == "BlueButton"))
							{
								return;
							}
							goto IL_CF9;
						}
						else
						{
							if (!(name == "MiscButton"))
							{
								return;
							}
							CameraController.Instance.MainPage.SetActive(false);
							CameraController.Instance.MiscPage.SetActive(true);
							return;
						}
						break;
					}
					case 11:
					{
						char c = name.get_Chars(0);
						if (c != 'G')
						{
							if (c != 'T')
							{
								return;
							}
							if (!(name == "TPRotButton"))
							{
								return;
							}
							CameraController.Instance.followheadrot = !CameraController.Instance.followheadrot;
							CameraController.Instance.TPRotText.text = CameraController.Instance.followheadrot.ToString().ToUpper();
							return;
						}
						else if (!(name == "GreenButton"))
						{
							return;
						}
						break;
					}
					case 12:
					{
						char c = name.get_Chars(0);
						if (c != 'N')
						{
							if (c != 'T')
							{
								return;
							}
							if (!(name == "TPRotButton1"))
							{
								return;
							}
							CameraController.Instance.followheadrot = !CameraController.Instance.followheadrot;
							CameraController.Instance.TPRotText.text = CameraController.Instance.followheadrot.ToString().ToUpper();
							return;
						}
						else
						{
							if (!(name == "NearClipDown"))
							{
								return;
							}
							CameraController.Instance.TabletCamera.nearClipPlane -= 0.01f;
							if ((double)CameraController.Instance.TabletCamera.nearClipPlane < 0.01)
							{
								CameraController.Instance.TabletCamera.nearClipPlane = 1f;
								CameraController.Instance.ThirdPersonCamera.nearClipPlane = 1f;
							}
							CameraController.Instance.ThirdPersonCamera.nearClipPlane = CameraController.Instance.TabletCamera.nearClipPlane;
							CameraController.Instance.NearClipText.text = CameraController.Instance.TabletCamera.nearClipPlane.ToString();
							CameraController.Instance.canbeused = true;
							return;
						}
						break;
					}
					case 13:
					{
						char c = name.get_Chars(0);
						if (c != 'F')
						{
							if (c != 'S')
							{
								return;
							}
							if (!(name == "SpeedUpButton"))
							{
								return;
							}
							CameraController.Instance.fpspeed += 0.01f;
							if ((double)CameraController.Instance.fpspeed > 0.1)
							{
								CameraController.Instance.fpspeed = 0.1f;
							}
							CameraController.Instance.SpeedText.text = CameraController.Instance.fpspeed.ToString();
							CameraController.Instance.canbeused = true;
							return;
						}
						else
						{
							if (!(name == "FlipCamButton"))
							{
								return;
							}
							CameraController.Instance.flipped = !CameraController.Instance.flipped;
							CameraController.Instance.ThirdPersonCameraGO.transform.Rotate(0f, 180f, 0f);
							CameraController.Instance.TabletCameraGO.transform.Rotate(0f, 180f, 0f);
							CameraController.Instance.FakeWebCam.transform.Rotate(-180f, 180f, 0f);
							return;
						}
						break;
					}
					case 14:
					{
						char c = name.get_Chars(0);
						if (c != 'C')
						{
							if (c != 'T')
							{
								return;
							}
							if (!(name == "TPModeUpButton"))
							{
								return;
							}
							if (CameraController.Instance.TPVMode == CameraController.TPVModes.BACK)
							{
								CameraController.Instance.TPVMode = CameraController.TPVModes.FRONT;
							}
							else
							{
								CameraController.Instance.TPVMode = CameraController.TPVModes.BACK;
							}
							CameraController.Instance.TPText.text = CameraController.Instance.TPVMode.ToString();
							return;
						}
						else
						{
							if (!(name == "ControlsButton"))
							{
								return;
							}
							if (!CameraController.Instance.openedurl)
							{
								Application.OpenURL("https://github.com/Yizzii/YizziCamModV2#controls");
								CameraController.Instance.openedurl = true;
								return;
							}
							return;
						}
						break;
					}
					case 15:
					{
						char c = name.get_Chars(0);
						if (c != 'M')
						{
							if (c != 'S')
							{
								return;
							}
							if (!(name == "SpeedDownButton"))
							{
								return;
							}
							CameraController.Instance.fpspeed -= 0.01f;
							if ((double)CameraController.Instance.fpspeed < 0.01)
							{
								CameraController.Instance.fpspeed = 0.01f;
							}
							CameraController.Instance.SpeedText.text = CameraController.Instance.fpspeed.ToString();
							CameraController.Instance.canbeused = true;
							return;
						}
						else
						{
							if (!(name == "MinDistUpButton"))
							{
								return;
							}
							CameraController.Instance.minDist += 0.1f;
							if (CameraController.Instance.minDist > 10f)
							{
								CameraController.Instance.minDist = 10f;
							}
							CameraController.Instance.MinDistText.text = CameraController.Instance.minDist.ToString();
							CameraController.Instance.canbeused = true;
							return;
						}
						break;
					}
					case 16:
						if (!(name == "TPModeDownButton"))
						{
							return;
						}
						if (CameraController.Instance.TPVMode == CameraController.TPVModes.BACK)
						{
							CameraController.Instance.TPVMode = CameraController.TPVModes.FRONT;
						}
						else
						{
							CameraController.Instance.TPVMode = CameraController.TPVModes.BACK;
						}
						CameraController.Instance.TPText.text = CameraController.Instance.TPVMode.ToString();
						return;
					case 17:
					{
						char c = name.get_Chars(0);
						if (c != 'G')
						{
							if (c != 'M')
							{
								if (c != 'S')
								{
									return;
								}
								if (!(name == "SmoothingUpButton"))
								{
									return;
								}
								CameraController.Instance.smoothing += 0.01f;
								if (CameraController.Instance.smoothing > 0.11f)
								{
									CameraController.Instance.smoothing = 0.05f;
								}
								CameraController.Instance.SmoothText.text = CameraController.Instance.smoothing.ToString();
								CameraController.Instance.canbeused = true;
								return;
							}
							else
							{
								if (!(name == "MinDistDownButton"))
								{
									return;
								}
								CameraController.Instance.minDist -= 0.1f;
								if (CameraController.Instance.minDist < 1f)
								{
									CameraController.Instance.minDist = 1f;
								}
								CameraController.Instance.MinDistText.text = CameraController.Instance.minDist.ToString();
								CameraController.Instance.canbeused = true;
								return;
							}
						}
						else
						{
							if (!(name == "GreenScreenButton"))
							{
								return;
							}
							CameraController.Instance.ColorScreenGO.active = !CameraController.Instance.ColorScreenGO.active;
							if (CameraController.Instance.ColorScreenGO.active)
							{
								CameraController.Instance.ColorScreenText.text = "(ENABLED)";
								return;
							}
							CameraController.Instance.ColorScreenText.text = "(DISABLED)";
							return;
						}
						break;
					}
					case 19:
						if (!(name == "SmoothingDownButton"))
						{
							return;
						}
						CameraController.Instance.smoothing -= 0.01f;
						if (CameraController.Instance.smoothing < 0.05f)
						{
							CameraController.Instance.smoothing = 0.11f;
						}
						CameraController.Instance.SmoothText.text = CameraController.Instance.smoothing.ToString();
						CameraController.Instance.canbeused = true;
						return;
					default:
						return;
					}
					using (List<Material>.Enumerator enumerator = CameraController.Instance.ScreenMats.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Material material2 = enumerator.Current;
							material2.color = Color.green;
						}
						return;
					}
					IL_CF9:
					foreach (Material material3 in CameraController.Instance.ScreenMats)
					{
						material3.color = Color.blue;
					}
				}
			}
		}
	}
}
