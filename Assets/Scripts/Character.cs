using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Character : MonoBehaviourPun, IPunObservable
{

    [SerializeField]
    private float speed = 600;

    [SerializeField]
    private float jumpforce = 800f;

    private Rigidbody2D rb;
    private float desireMovementAxis = 0f;

    private PhotonView pv;
    private Vector3 enemyPos = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pv = GetComponent<PhotonView>();

        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 20;
    }

    void Update()
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
        rb.velocity = new Vector2(desireMovementAxis * Time.deltaTime * speed, rb.velocity.y);
    }

    private void CheckInputs()
    {
        desireMovementAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && Mathf.Approximately(rb.velocity.y, 0f))
        {
            rb.AddForce(new Vector2(0f, jumpforce));
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            PiumPium();
        }

    }

    private void SmoothReplicate()
    {
        transform.position = Vector3.Lerp(transform.position, enemyPos, Time.deltaTime * 20);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) 
        {
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
            enemyPos = (Vector3) stream.ReceiveNext();
        }
    }

    public void PiumPium()
    {
        PhotonNetwork.Instantiate("Bullet", transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);
    }

    public void Damage()
    {
        pv.RPC("NetworkDamage", RpcTarget.All);
    }

    [PunRPC]
    private void NetworkDamage()
    {
        Destroy(this.gameObject);
    }
}
