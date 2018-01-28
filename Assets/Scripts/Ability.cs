using UnityEngine;

public class Ability : MonoBehaviour
{
	public float damage;
	public float spawnRotationDeviation;
	public float speed;
	public bool dieOnHit;
	/// <summary>
	/// Set to a negative number to live forever.
	/// </summary>
	public float timeToLive = -1;
	public GameObject deathEffect;

	private float _spawnTime;
	private bool _isDead;

    private void Start()
    {
		var eulerAngles = transform.eulerAngles;
		eulerAngles.y += Random.Range(-spawnRotationDeviation, spawnRotationDeviation);
		transform.eulerAngles = eulerAngles;

		var body = GetComponent<Rigidbody>();
		body.AddForce(transform.forward * speed);
		_isDead = false;
		_spawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time > _spawnTime + timeToLive && timeToLive > 0 && !_isDead)
		{
			_isDead = true;
			Destroy(gameObject);
			if (deathEffect != null)
			{
				Instantiate(deathEffect, transform.position, transform.rotation);
			}
		}
    }

    private void OnTriggerEnter(Collider other)
    {
		ProcessHit();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ProcessHit();
    }

	private void ProcessHit()
	{
		// TODO: deal damage to the damage receiver.
		Debug.Log("Hit");
		if (dieOnHit)
		{
			Destroy(gameObject);
		}
	}
}
