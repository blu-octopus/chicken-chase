using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class FoxAnimationController : MonoBehaviour
{
    [SerializeField] string runInvoker;
    //[SerializeField] string idleInvoker;

    public Animator anim;

    public bool IsRunning { get; set; }
    public bool isStunned;
    public float stunTimer;

    private FoxMovement fm;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        fm = GetComponentInParent<FoxMovement>();
        stunTimer = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunning)
        {
            /*if (isStunned)
            {
                if (stunTimer > 0)
                {
                    stunTimer -= Time.deltaTime;
                    anim.SetBool("isStunned", true);
                }
                else
                {
                    isStunned = false;
                    stunTimer = 3f;
                    anim.SetBool("isStunned", false);
                }
            }
            else
            {
                //anim.SetBool("isStunned", false);
            }*/
            anim.SetBool(runInvoker, true);
        }
        else
        {
            
            anim.SetBool(runInvoker, false);
        }
    }

    
}
