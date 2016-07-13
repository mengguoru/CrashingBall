using UnityEngine;
using System.Collections;

public class OverAnimationController : MonoBehaviour {

    Animator anim;
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(0);
        if(stateinfo.fullPathHash == Animator.StringToHash("Base Layer.over"))
        {
            Application.Quit();
        }
    }
}
