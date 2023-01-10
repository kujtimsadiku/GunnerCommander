using UnityEngine;

public class TrooperMoement : MonoBehaviour
{
	public float speed;
	public int unit;
	private LaneUnits removeFromThis;
	[HideInInspector] public int ownLane;
	[HideInInspector] public LaneUnitClass ownLink;
	[HideInInspector] public int index;
	private Rigidbody2D rb;
	[HideInInspector] public float direction;
	[HideInInspector] public float stop = 1.0f;
	public BoxCollider2D hitBox;
	[HideInInspector] public bool shooting = false;
	[SerializeField] private Vector2 attackBoxSize;
	public float distanceToEnemy;
	[HideInInspector] public float moveSpeed;
	public Vector2 ownBoxArea;
	[HideInInspector] public bool idle;
	[HideInInspector] public bool shoot;
	[HideInInspector] public bool projectile = false;
	private bool justStarted = true;
	[HideInInspector] public GameObject targetEnemy;
	[SerializeField] private Vector3 positionOffset;
	[HideInInspector] public bool endLine = false;
	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		direction = 1;
		moveSpeed = speed;
	}
	public void Init()
	{
		if (gameObject.layer == LayerMask.NameToLayer("Allied"))
			removeFromThis = GameObject.FindWithTag("LaneManager").GetComponent<LaneUnits>();
		else
			removeFromThis = GameObject.FindWithTag("EnemyLaneManager").GetComponent<LaneUnits>();
	}
	private void FixedUpdate()
	{
		collidingWithAlliedTroopers();
		collidingWithTheEnemy();
		CanAttack();
		if (projectile)
		{
			rb.velocity = Vector2.zero;
			stop = 0.0f;
			return ;
		}
		rb.velocity = Vector2.right * direction * moveSpeed * stop;
		if (stop == 0.0f && !shooting)
			idle = true;
		else
			idle = false;
	}
	private void collidingWithAlliedTroopers()
	{
		bool visited = false;
		TrooperMoement troop;
		Collider2D[] collisions = Physics2D.OverlapBoxAll(transform.position + positionOffset, ownBoxArea, 0.0f);
		foreach (Collider2D other in collisions)
		{
			if (other.transform.gameObject.layer == gameObject.layer && other != hitBox)
			{
				troop = other.transform.gameObject.GetComponent<TrooperMoement>();
				if (justStarted && troop.index < index)
				{
					stop = 0.0f;
					return ;
				}
				if (!justStarted && (((direction == 1.0f) && other.transform.position.x > transform.position.x) || (direction == (-1.0f) && other.transform.position.x < transform.position.x)) && troop != null)
				{
					if (troop.stop == 0.0f)
					{
						stop = 0.0f;
						return ;
					}
					visited = true;
					moveSpeed = troop.moveSpeed;
					if (moveSpeed > speed)
						moveSpeed = speed;
				}
			}
		}
		if (!visited)
		{
			moveSpeed = speed;
			justStarted = false;
		}
		if (!shooting)
		{
			stop = 1.0f;
			justStarted = false;
		}
	}
	private void collidingWithTheEnemy()
	{
		int layerIndex = LayerMask.NameToLayer("Enemy");
		Collider2D[] collisions = Physics2D.OverlapCircleAll(new Vector2(transform.position.x + distanceToEnemy * direction + positionOffset.x, transform.position.y + positionOffset.y), 0.2f);
		if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
			layerIndex = LayerMask.NameToLayer("Allied");
		foreach (Collider2D other in collisions)
		{
			if (other.transform.gameObject.layer == layerIndex && other != hitBox)
			{
				stop = 0.0f;
				return ;
			}
		}
	}
	private void CanAttack()
	{
		if (stop != 0.0f)
		{
			shoot = false;
			return ;
		}
		float len = 5000.0f;
		float temp;
		GameObject target = null;
		int layerIndex = LayerMask.NameToLayer("Enemy");
		if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
			layerIndex = LayerMask.NameToLayer("Allied");
		Collider2D[] other = Physics2D.OverlapBoxAll(transform.position + positionOffset, attackBoxSize, 0.0f);
		foreach (Collider2D enem in other)
		{
			if (enem.transform.gameObject.layer == layerIndex)
			{
				shoot = true;
				if ((temp = new Vector2(enem.transform.position.x - transform.position.x, enem.transform.position.y - transform.position.y).magnitude) < len)
				{
					target = enem.transform.gameObject;
					len = temp;
				}
			}
		}
		targetEnemy = target;
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("LaneManager") && ownLink != null)
		{
			if (other.gameObject.GetComponent<BoxCollider2D>().IsTouching(hitBox))
				ownLink.checkpoint = 1;
			else if (other.gameObject.GetComponent<CapsuleCollider2D>().IsTouching(hitBox))
				ownLink.checkpoint = 2;
		}
		if (gameObject.layer == 3)
		{
			if (other.gameObject.CompareTag("EndLine") && hitBox.IsTouching(other))
				endLine = true;
		}
		else if (gameObject.layer == 6)
		{
			if (other.gameObject.CompareTag("Fence") && hitBox.IsTouching(other))
				Destroy(transform.parent.gameObject);
		}
	}
	private void OnDestroy()
	{
		if (ownLink != null)
			removeFromThis.lanes[ownLane].Remove(ownLink);
	}
}
