using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviourPun, IPunObservable
{
    [SerializeField] private float speed;
    [SerializeField] private float jumping;

    [SerializeField] private int lives;
    [SerializeField] private float health;
    [SerializeField] private float damage;

    [SerializeField] private float cadency;
    [SerializeField] private float timeSinceLastShot = 0;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float desiredMovementAxis = 0f;

    private PhotonView pv;
    private Vector3 enemyPosition = Vector3.zero;
    private Vector3 spawnPosition;

    [SerializeField] private GameObject bulletPrefab;

    private PlayerCanvas playerCanvas;

    //Funciones
    public float GetHealth() { return health; }

    public void SetAttributes(Race race)
    {
        health = race.health;
        damage = race.damage;
        cadency = race.cadency;
        speed = race.speed;
        jumping = race.jumping;
    }

    public void SetSpawnPosition(Vector3 position) { spawnPosition = position; }

    public PlayerCanvas GetPlayerCanvas() { return playerCanvas; }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
        playerCanvas = GetComponentInChildren<PlayerCanvas>();

        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 20;

        lives = 3;
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            CheckInputs();
        }
        else 
        {
            SmoothReplicate();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(desiredMovementAxis * Time.fixedDeltaTime * speed, rb.velocity.y);
    }

    private void CheckInputs() {
        //Mov
        desiredMovementAxis = Input.GetAxisRaw("Horizontal");
        if (desiredMovementAxis > 0)
        {
            sr.flipX = false;
        }
        else if (desiredMovementAxis < 0) {
            sr.flipX = true;
        }

        //Salto
        if (Input.GetButtonDown("Jump") && Mathf.Approximately(rb.velocity.y, 0f)) {
            rb.AddForce(new Vector2(0f, jumping));
        }

        //Disparo
        if (Input.GetKeyDown(KeyCode.E) && timeSinceLastShot >= cadency)
        {
            timeSinceLastShot = 0;
            PiumPium();
        }
        else if (timeSinceLastShot < cadency) {
            timeSinceLastShot += Time.deltaTime;
        }
    }

    private void PiumPium() 
    {
        if (sr.flipX)
        {
            GameObject bulletTmp = PhotonNetwork.Instantiate("Bullet", transform.position + new Vector3(-3f, 0, 0), Quaternion.identity);
            bulletTmp.GetComponent<Bullet>().direction = -1;
            bulletTmp.GetComponent<Bullet>().SetPlayerDamage(damage);
        }
        else
        {
            GameObject bulletTmp = PhotonNetwork.Instantiate("Bullet", transform.position + new Vector3(3f, 0, 0), Quaternion.identity);
            bulletTmp.GetComponent<Bullet>().SetPlayerDamage(damage);
        }
    }

    private void SmoothReplicate() 
    {
        transform.position = Vector3.Lerp(transform.position, enemyPosition, Time.deltaTime * 20);
    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsReading)
        {
            enemyPosition = (Vector3)stream.ReceiveNext();
            sr.flipX = (bool)stream.ReceiveNext();
        }
        else if (stream.IsWriting) 
        {
            stream.SendNext(transform.position);
            stream.SendNext(sr.flipX);
            stream.SendNext(rb.velocity.magnitude);
        }
    }

    public void Damage(float damage) 
    {
        pv.RPC("NetworkDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    private void NetworkDamage(float damage) 
    {
        health -= damage;
        playerCanvas.SetHealth(health);

        if (health <= 0) 
        {
            lives--;
            health = 100;
            playerCanvas.SetHealth(health);

            // Game Over
            if (lives <= 0) {
                Destroy(this.gameObject);
                SceneManager.LoadScene("END");
            }

            transform.position = spawnPosition;
        }
    }
}
