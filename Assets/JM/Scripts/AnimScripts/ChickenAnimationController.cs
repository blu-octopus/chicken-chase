using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class ChickenAnimationController : MonoBehaviour
{
    #region Fields

    // ReadOnly fields for debugging purposes
    [ReadOnly]
    [SerializeField] float viewElapsed;
    [ReadOnly]
    [SerializeField] float viewTimeEnd;
    [ReadOnly]
    [SerializeField] int selectedIndex;

    [Header("Animation Fields")]
    [Tooltip("Array of animations labeled as strings")]
    [SerializeField] private string[] paramArray;

    [Tooltip("The string used to invoke the running animation")]
    [SerializeField] private string runInvoker;

    [Tooltip("The string used to invoke a random idle animation")]
    [SerializeField] private string idleInvoker;

    [Tooltip(
        "Average length of time until " +
        "random idle animation selection begins"
    )]
    [SerializeField] private float loopEnd;

    #endregion

    #region Members

    public Animator anim;
    private int _idleSelect;
    public float TimeElapsed { get; private set; }
    public float TimeEnd { get; private set; }

    // Toggle IsRunning with movement script
    public bool IsRunning { get; set; } 

    #endregion

    /// Gather required components
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    /// Initialize timer variables
    private void Start()
    {
        _idleSelect= 0;
        ResetTimer();
    }
    
    void Update()
    {
        // Incremement timer
        TimeElapsed += Time.deltaTime;

        // If object is running use run anim
        if (IsRunning) 
        {
            // Reset timer as a precaution for potential bugs
            ResetTimer();
            
            ChickenStartRun();            
        }
        // If object is not running use idle anim
        else
        {
            ChickenStopRun();
        }

        // If timer reaches end, trigger idle anim switch
        if (TimeElapsed >= TimeEnd)
        {
            SelectIdleAnimation();
            anim.SetBool("CanSwitch", true);

            // Reset timer before selecting next idle animation
            ResetTimer();
        }
        // Else, do not trigger idle switch
        else
        {
            anim.SetBool("CanSwitch", false);
        }

        // See serialize fields
        viewElapsed = TimeElapsed;
        viewTimeEnd = TimeEnd;
    }

    /// <summary>
    /// Chooses a random idle animation listed in paramArray
    /// The select integer is then sent to the Animator component
    /// Animator chooses animation based on select index
    /// The idleInvoker is just used to track the set of animations
    /// </summary>
    public void SelectIdleAnimation()
    {
        // Choose an idle animation other than main Idle
        _idleSelect = Random.Range(1, paramArray.Length);
        selectedIndex = _idleSelect;

        // Execute animation
        anim.SetInteger(idleInvoker, _idleSelect);
    }

    // Invokes the run anim
    public void ChickenStartRun()
    {
        anim.SetBool(runInvoker, true);
    }

    // Stops the run anim
    public void ChickenStopRun()
    {
        anim.SetBool(runInvoker, false);
    }

    // Resets IdleSelect variable in animator back to 0 (main Idle anim)
    public void ResetIdleAnimation()
    {
        anim.SetInteger(idleInvoker, 0);
    }

    // timer reset
    private void ResetTimer()
    {
        // Calculates a random number between loopEnd - 1 and loopEnd + 1
        // all inclusive. Designed to create a bit of variance in the timing
        // of animations

        TimeEnd = Random.Range(loopEnd - 1f, loopEnd + 1f);
        TimeElapsed = 0;
    }
}
