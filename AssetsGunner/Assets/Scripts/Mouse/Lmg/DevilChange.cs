using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilChange : MonoBehaviour
{
	[SerializeField] private AudioSource changeSound;
	[SerializeField] private Camera camShake;
	private LmgShoot lmg;
	private ShootDevilLmg devil;
	private bool change = false;
	private bool begin = false;
	private bool invoked = false;
	private bool changeDone = false;
	private Color ogColor;
	private Color variant = new Color(0.239f, 0.082f, 0.082f, 0.196f);
	private bool changer = true;
	private Vector3 ogCameraPos;
	private Vector3[] camPosVariants = new Vector3[4];
	private int shakeCount = 0;
	private bool inThere = false;
	private void Awake() 
	{
		lmg = GetComponent<LmgShoot>();
		devil = GetComponent<ShootDevilLmg>();
	}
	private void Start()
	{
		ogColor = lmg.groundAlpha.color;
		ogColor.a = 0.196f;
		ogCameraPos = camShake.transform.position;
		camPosVariants[0] = new Vector3(ogCameraPos.x - 0.016f, ogCameraPos.y - 0.016f, ogCameraPos.z);
		camPosVariants[1] = new Vector3(ogCameraPos.x + 0.016f, ogCameraPos.y - 0.016f, ogCameraPos.z);
		camPosVariants[2] = new Vector3(ogCameraPos.x - 0.016f, ogCameraPos.y + 0.016f, ogCameraPos.z);
		camPosVariants[3] = new Vector3(ogCameraPos.x + 0.016f, ogCameraPos.y + 0.016f, ogCameraPos.z);
	}
	private void Update()
	{
		if (changeDone)
		{
			if (!lmg.build)
			{
				CancelInvoke();
				ogColor.a = 0.196f;
				variant.a = 0.196f;
				lmg.groundAlpha.color = new Color(ogColor.r, ogColor.g, ogColor.b, 0.0f);
				changeDone = false;
			}
			else
				NormalChanger();
			return ;
		}
		if (lmg.build && !change)
		{
			begin = true;
			change = true;
			inThere = false;
		}
		else if (!lmg.build && !inThere)
		{
			devil.acive = false;
			devil.rapid.Stop();
			devil.start = false;
			change = false;
			shakeCount = 0;
			ogColor.a = 0.196f;
			variant.a = 0.196f;
			changer = true;
			invoked = false;
			camShake.transform.position = ogCameraPos;
			lmg.groundAlpha.color = new Color(ogColor.r, ogColor.g, ogColor.b, 0.0f);
			GotKill.active = false;
			if (Time.timeScale != 1.0f)
				Time.timeScale = 1.0f;
			changeDone = false;
			CancelInvoke();
			changeSound.Stop();
			inThere = true;
		}
		if (begin)
			BeginTransformation();
		if (change)
			ChangeHappening();
	}

	private void FixedUpdate()
	{
		if (!lmg.build)
			return ;
		if (shakeCount % 3 == 0)
			ShakeCamera();
		shakeCount++;
	}
	private void ShakeCamera()
	{
		if (camShake.transform.position != ogCameraPos)
		{
			camShake.transform.position = ogCameraPos;
			return ;
		}
		int fPos = Random.Range(0, 4);
		camShake.transform.position = camPosVariants[fPos];
	}
	private void NormalChanger()
	{
		if (changer)
		{
			changer = false;
			Invoke(nameof(ChangeColorVariants), 0.075f);
		}
	}
	private void ChangeColorVariants()
	{
		if ((int)Random.Range(0, 2) == 1)
			lmg.groundAlpha.color = variant;
		else
			lmg.groundAlpha.color = ogColor;
		changer = true;
		float unit = 0.274f / 550.0f;
		if (devil.shotsHit < 550)
		{
			variant.a = 0.196f + (unit * devil.shotsHit);
			ogColor.a = 0.196f + (unit * devil.shotsHit);
		}
		else
		{
			variant.a = 0.196f + 0.274f;
			ogColor.a = 0.196f + 0.274f;
		}
	}
	private void ChangeHappening()
	{
		if (!invoked)
		{
			invoked = true;
			Invoke(nameof(ChangeColor), 0.075f);
			if (Time.timeScale >= 1.0f)
			{
				invoked = false;
				lmg.groundAlpha.color = ogColor;
				changeDone = true;
				CancelInvoke();
			}
		}
		if (Time.timeScale < 1.0f)
			Time.timeScale += 0.08f * Time.unscaledDeltaTime;
		else if (Time.timeScale != 1.5f)
			Time.timeScale = 1.0f;
		if (devil.rapid.volume < 0.2f)
			devil.rapid.volume += 0.1f * Time.unscaledDeltaTime;
		else if (devil.rapid.volume != 0.2f)
			devil.rapid.volume = 0.2f;
	}
	private void ChangeColor()
	{
		int col = Random.Range(0, 3);
		if (col == 0)
			lmg.groundAlpha.color = ogColor;
		else if (col == 1)
			lmg.groundAlpha.color = new Color(1.0f, 1.0f, 1.0f, ogColor.a);
		else
			lmg.groundAlpha.color = new Color(0.0f, 0.0f, 0.0f, ogColor.a);
		invoked = false;
	}
	private void BeginTransformation()
	{
		devil.PlayAudio();
		devil.acive = true;
		devil.start = true;
		GotKill.active = true;
		devil.rapid.volume = 0.02f;
		begin = false;
		Time.timeScale = 0.4f;
		changeSound.Play();
	}
}
