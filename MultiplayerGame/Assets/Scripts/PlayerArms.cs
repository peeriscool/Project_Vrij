using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArms : MonoBehaviour
{

    public Transform targetTransform;
    public LayerMask mouseAimMask;

    public Transform rightHand;
    public Transform lefttHand;

    private Animator animator;
    private Camera mainCamera;


    bool activate = false;




    void Start()
    {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            activate = true;
        }
        if (Input.GetMouseButtonUp(0) && activate == true)
          {
            activate = false;
         }

        if (activate == true){
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, mouseAimMask))
            {
                if (hit.transform != null)
                {
                    targetTransform.position = hit.point;
                }
                
            }
            
        }

    }


   

    private void OnAnimatorIK()
    {   
        Rect bounds = new Rect (0, 0, Screen.width/2, Screen.height);
        if (activate == true && bounds.Contains(Input.mousePosition)){

            //move right arm at target
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
            animator.SetIKPosition(AvatarIKGoal.RightHand, targetTransform.position);

            // Look at target 
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(targetTransform.position);

        } else {
            

            //move left arm at taget
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, targetTransform.position);

            // Look at target 
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(targetTransform.position);

        }
        
    }

}
