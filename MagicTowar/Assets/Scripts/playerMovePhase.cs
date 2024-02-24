using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovePhase : MonoBehaviour
{
    public float moveSpeed;
    public JoyStick jS;
    public Animator anime;
    private Vector3 moveVector;
    // Start is called before the first frame update
    void Start()
    {
       // anime = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVector.x = jS.Horizontal();
        moveVector.z = jS.Vertical();
        move();
        isMoving();
        rotation();
    }
    private void move()
    {
        transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
    }
    private void isMoving()
    {
        if (jS.Horizontal() == 0f || jS.Vertical() == 0f)
        {
            anime.SetBool("run", false);
        }
        else
        {
            anime.SetBool("run", true);
        }
    }
    private void rotation()
    {
        Vector3 dir, lookDir;
        dir = new Vector3(moveVector.x, 0, moveVector.z).normalized;
        lookDir = transform.position + dir;
        transform.LookAt(lookDir);
    }
}
