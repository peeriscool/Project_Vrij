using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArms : MonoBehaviour
{

    public Transform targetTransform;
    public Transform rightTarget;
    public LayerMask mouseAimMask;

    public Transform rightHand;
    public Transform lefttHand;

    private Animator animator;
    private Camera mainCamera;


    bool activate = false;
    bool leftActive = false;
    bool rightActive = false;




    void Start()
    {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    
    void Update()
    {
        //check if clicked
        if (Input.GetMouseButtonDown(0))
        {
            activate = true;
        }
        if (Input.GetMouseButtonUp(0) && activate == true)
          {
            activate = false;
         }

        Rect bounds = new Rect (0, 0, Screen.width/2, Screen.height);
        if (activate == true){
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

        //tracks the mouse & sets the target for the arms
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
            {
                if (hit.transform != null && bounds.Contains(Input.mousePosition))
                {
                    targetTransform.position = hit.point;
                    leftActive = true;
                    rightActive = false;
                } else if (hit.transform != null && !bounds.Contains(Input.mousePosition))
                {
                    rightTarget.position = hit.point;
                    rightActive = true;
                    leftActive = false;
                }
                
            }
            
        }

    }


   

    private void OnAnimatorIK()
    {   

        if (leftActive == true){

            //move right arm at target
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);

            // Look at target 
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(targetTransform.position);

            //keeps right arm at target
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, rightTarget.position);

        } else if (rightActive == true){
            

            //move left arm at taget
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, rightTarget.position);

            // Look at target 
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(rightTarget.position);

            //keeps right arm at target
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);

        }
        
    }

}
