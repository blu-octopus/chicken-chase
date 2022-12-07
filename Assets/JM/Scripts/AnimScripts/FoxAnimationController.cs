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

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
