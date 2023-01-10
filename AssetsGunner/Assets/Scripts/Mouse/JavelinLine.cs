using UnityEngine;
using System.Collections.Generic;

public class JavelinLine : MonoBehaviour
{
	[SerializeField] private MousePosition click;
	[SerializeField] private GameObject ground;
	private LineRenderer line;
	[HideInInspector] public bool drawing {get ; private set ;}
	[HideInInspector] public float material = 100.0f;
	private List<Vector2> points = new List<Vector2>();
	private int mod = 0;
	[SerializeField] private int modCount;
	[SerializeField] private float maxDist = 1.0f;
	[SerializeField] private float materialDecrease;
	private bool endIt = false;
	private bool visited = false;
	private int currColor = 0;
	private bool javelinFlying = false;
	private float materialUsed = 0.0f;
	[SerializeField] private new AudioSource audio;
	[SerializeField] private GameObject javelinPrefab;
	private void Awake()
	{
		line = GetComponent<LineRenderer>();
	}
	void Start()
	{
		StartCoroutine(WaitCoroutine());
	}
	private void Update()
	{
		if (click.click && !drawing && material != 0.0f && !endIt && !javelinFlying)
		{
			ground.SetActive(true);
			InvokeRepeating(nameof(DrawingProgress), 0.0f, 0.02f);
			Time.timeScale = 0.3f;
			drawing = true;
		}
		if ((click.holdClick == false && drawing) || material <= 0.0f || (endIt && !visited))
		{
			ground.SetActive(false);
			javelinFlying = true;
			Time.timeScale = 1.0f;
			if (endIt)
				Denied();
			else
			{
				if (points.Count > 1)
				{
					GameObject javelin = Instantiate(javelinPrefab, new Vector3(points[0].x, points[0].y, 1.0f), Quaternion.identity);
					JavelinTravel travel = javelin.GetComponent<JavelinTravel>();
					travel.points = new List<Vector2>(points);
					travel.destroyed += ResetFlying;
				}
				else
					javelinFlying = false;
				points.Clear();
				line.positionCount = 0;
			}
			materialUsed = 0.0f;
			mod = 0;
			drawing = false;
		}
	}
	private IEnumerator<WaitForSecondsRealtime> WaitCoroutine()
	{
		while(true)
		{
			yield return new WaitForSecondsRealtime(0.02f);
				DrawingProgress();
		}
	}
	private void DrawingProgress()
	{
		if (drawing && (mod == 0 || mod % modCount == 0))
		{
			material -= (materialDecrease / 12.0f);
			if (points.Count != 0)
			{
				if (NotFarEngough())
					return ;
			}
			Vector2 point = DrawLine();
			GetAngle(point);
			if (endIt)
				return ;
			points.Add(point);
			ManageLine();
			material -= materialDecrease;
			materialUsed += materialDecrease;
		}
	}
	private bool NotFarEngough()
	{
		Vector2 dist = new Vector2(click.mousePosition.x, click.mousePosition.y) - points[points.Count - 1];
		if (dist.magnitude < 0.2f)
			return (true);
		return (false);
	}
	private void Denied()
	{
		audio.Play();
		material += materialUsed;
		visited = true;
		InvokeRepeating(nameof(ChangeColor), 0.0f, 0.075f);
		Invoke(nameof(ResetEndit), 0.4f);
	}
	private void ChangeColor()
	{
		if (currColor == 0)
		{
			line.endColor = new Color(1.0f, 0.0f, 0.0f, 0.48f);
			currColor = 1;
		}
		else
		{
			line.endColor = new Color(0.0f, 0.0f, 0.0f, 0.0f);
			currColor = 0;
		}
	}
	private void ResetEndit()
	{
		CancelInvoke();
		javelinFlying = false;
		line.endColor = new Color(0.160f, 0.180f, 0.2f, 0.48f);
		endIt = false;
		visited = false;
		points.Clear();
		line.positionCount = 0;
	}
	private void GetAngle(Vector2 point)
	{
		if (points.Count <= 1)
			return ;
		Vector2 og = points[points.Count - 1] - points[points.Count - 2];
		Vector2 uus = point - points[points.Count - 1];
		if (Vector2.Angle(og, uus) >= 150.0f)
			endIt = true;
	}
	private void ManageLine()
	{
		line.positionCount = points.Count;
		for (int i = 0; i < points.Count; i++)
			line.SetPosition(i, points[i]);
	}
	private Vector2 DrawLine()
	{
		if (points.Count == 0)
			return (new Vector2(click.mousePosition.x, click.mousePosition.y));
		Vector2 ret = new Vector2(click.mousePosition.x, click.mousePosition.y);
		Vector2 compare = ret - points[points.Count - 1];
		if (compare.magnitude > maxDist)
			return (points[points.Count - 1] + (compare.normalized * maxDist));
		return (ret);
	}
	private void ResetFlying()
	{
		javelinFlying = false;
	}
}
