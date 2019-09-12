using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKtest : MonoBehaviour
{
    Animator anim;
    float reach=0;
    void Start () {
    anim= GetComponent<Animator>();
    }
    void OnAnimatorIK() {
    anim.SetIKPositionWeight(AvatarIKGoal.RightHand, reach);
    anim.SetIKPosition(AvatarIKGoal.RightHand, transform.position+new Vector3(0,3,2) );
}

    // Update is called once per frame
    void Update () {
	if(  Input.GetKeyUp( KeyCode.RightArrow ) )
		reach+=0.02f;
	if(  Input.GetKeyUp( KeyCode.LeftArrow ) )
		reach-=0.02f;
}
}
