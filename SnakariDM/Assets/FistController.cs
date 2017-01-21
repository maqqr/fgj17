using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistController : MonoBehaviour {

    private Facing side;
    private Rigidbody2D fistBody;
    private FixedJoint2D joint;
    [SerializeField]
    private float punchSpeed = 20f;
    [SerializeField]
    private float punchAnimationTime = 0.15f;

    [SerializeField]
    private float punchForce = 20f;
    [SerializeField]
    private Vector2 punchStartModifier = new Vector2();

    private Vector2 startingPos;

    private float punchTime = 0f;
    private bool punched = false;
    private Rigidbody2D toLatchTo;
    private Vector2 diff;
    private Pool powPool;
    private GameController gameController;

    // Use this for initialization
    void Awake () {
        fistBody = GetComponent<Rigidbody2D>();
        joint = GetComponent<FixedJoint2D>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        startingPos = transform.localPosition;
	}

    private void Start()
    {
        powPool = GameObject.FindObjectOfType<Pool>();
    }

    public void Register(Rigidbody2D player)
    {
        this.toLatchTo = player;

        diff = fistBody.position - player.position;
    }


    public void ThrowAPunch(Facing facing)
    {
        if (punched) return;

        fistBody.position += punchStartModifier;
        Vector2 punch = facing == Facing.Left ? new Vector2(-punchSpeed, 0) : new Vector2(punchSpeed, 0);
        joint.enabled = false;
        fistBody.velocity = Vector2.zero;
        fistBody.AddForce(punch, ForceMode2D.Impulse);
        punched = true;
        //Rotate the fist
    }


    // Update is called once per frame
    void Update () {
        if(punched)
            punchTime += Time.deltaTime;
        if(punchTime >= punchAnimationTime)
        {
            punchTime = 0;
            punched = false;
            fistBody.position = toLatchTo.position + diff;
            fistBody.velocity = Vector2.zero;
            fistBody.rotation = 0;
            fistBody.angularVelocity = 0;
            joint.enabled = true;
        }
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(punched && "Player" == collision.gameObject.tag)
        {
            Vector2 dir = fistBody.position - collision.rigidbody.position;

            collision.rigidbody.AddForce(dir.normalized * punchForce * -1);
            collision.gameObject.GetComponent<PlayerController>().TakeHit();
            GameObject pow = powPool.Get();
            PovDispenser povDispenser = pow.AddComponent<PovDispenser>();
            povDispenser.PovPool = this.powPool;
            pow.transform.position = fistBody.position +  Random.insideUnitCircle * 0.75f;

            gameController.PlayRandomSound(gameController.punchSounds);
            gameController.PlayRandomSound(gameController.hurtSounds);
        }
    }

}
