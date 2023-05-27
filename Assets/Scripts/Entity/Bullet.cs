using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    [SerializeField] 
    private float speed = 10f;
    public float direction = 1;

    private Rigidbody2D rb;
    private PhotonView pv;

    private float playerDamage;

    public void SetPlayerDamage(float damage) { playerDamage = damage; }
    public float GetPlayerDamage() { return playerDamage; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(direction * speed, 0);
    }

    [PunRPC]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player")) {
            collision.gameObject.GetComponent<Character>().Damage(playerDamage);
            pv.RPC("NetworkDestroy", RpcTarget.All);
        }
    }

    [PunRPC]
    public void NetworkDestroy() 
    {
        Destroy(this.gameObject);
    }
}
