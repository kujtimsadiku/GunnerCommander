using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
	[SerializeField] private Camera cam;
	private Quaternion savedRotation;
	private Vector3 camOriginalRot;
	private Vector3 camOriginalPos;
	public float InvokeRepeatRate, InvokeTime;
	public float xShakeStart, xShakeEnd;
	public float yShakeStart, yShakeEnd;
	public float zAngleStart, zAngleEnd;
	public bool setValuesNegativeAlso;
	public int shakeAmm;
	//Clamped range is from 0-1;
	public float clampedRange;
	private int i = 0;
	private void Awake() {
		camOriginalRot = Camera.main.transform.eulerAngles;
		camOriginalPos = Camera.main.transform.position;
		savedRotation = Camera.main.transform.rotation;
		InvokeRepeating(nameof(ShakeCamera), InvokeTime, InvokeRepeatRate);
	}
	private void ShakeCamera()
	{
		int temp = i % 2;
		Quaternion target = Targets(temp);
		if ((i % 2) == 0)
			cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, target, clampedRange);
		else if ((i % 2) != 0)
			cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, target, clampedRange);
		i++;
		if (i > shakeAmm)
		{
			Camera.main.transform.eulerAngles = camOriginalRot;
			Camera.main.transform.rotation = savedRotation;
			Camera.main.transform.position = camOriginalPos;
			CancelInvoke();
		}
		Quaternion Targets(int i)
		{
			Quaternion target;
			if (setValuesNegativeAlso && i != 0)
			{
				target = Quaternion.Euler(cam.transform.rotation.x,
											cam.transform.rotation.y,
												cam.transform.rotation.z - Random.Range(-zAngleEnd, -zAngleStart));
				if ((xShakeEnd - xShakeStart) == 0f && (xShakeEnd - xShakeStart) == 0f)
					return (target);
				Camera.main.transform.position = new Vector3(Random.Range(-xShakeEnd, -xShakeStart),
																Random.Range(-yShakeEnd, -yShakeStart),
																	cam.transform.position.z);
			}
			else
			{
				target = Quaternion.Euler(cam.transform.rotation.x,
											cam.transform.rotation.y,
												cam.transform.rotation.z - Random.Range(zAngleStart, zAngleEnd));
				if ((xShakeEnd - xShakeStart) == 0f && (xShakeEnd - xShakeStart) == 0f)
					return (target);
				Camera.main.transform.position = new Vector3(Random.Range(xShakeStart, xShakeEnd),
																Random.Range(yShakeStart, yShakeEnd),
																	cam.transform.position.z);
			}
			return (target);
		}
	}
}
