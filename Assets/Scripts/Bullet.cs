using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bullet : MonoBehaviourPun
{
    public float speed = 10f;
    private Rigidbody2D rb;
    private PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(speed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Character>().Damage();
        pv.RPC("NetworkDestroy", RpcTarget.All);

    }
    [PunRPC]
    void NetworkDestroy()
    {
        Destroy(this.gameObject);
    }

}

