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
        if (isStunned)
        {
            anim.SetBool("isStunned", true);
        }
        else
        {
            anim.SetBool("isStunned", false);
            if (IsRunning)
            {
                anim.SetBool(runInvoker, true);
            }
            else
            {
                anim.SetBool(runInvoker, false);
            }
        }
        
    }

    
}
