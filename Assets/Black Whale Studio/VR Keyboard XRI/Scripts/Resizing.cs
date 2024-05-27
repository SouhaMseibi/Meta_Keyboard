using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resizing : MonoBehaviour
{
    [SerializeField] public Transform canvasTransform;
    private float initialDistance;
    private Vector3 initialScale;
    private bool isResizing = false;
    [SerializeField] protected OVRHand hand ;
    [SerializeField] protected OVRSkeleton handSkeleton ;
    private Vector3 indexPos ; 
    private Vector3 thumbPos ; 
    void Update()
    {
        if ( !hand ) hand = GetComponent<OVRHand>();
        if ( !handSkeleton ) handSkeleton = GetComponent<OVRSkeleton>();
        if (hand.IsTracked  ) 
            {
            
            if (hand.IsTracked  ) 
                { 
                    if (hand.GetFingerIsPinching(OVRHand.HandFinger.Index) )
                    {
                        StartResizing();
                        if (isResizing)
                        {
                            ResizeObject();
                        }
                    }
                    else 
                    {
                        StopResizing();
                    }
                    }
            }              
            
    }

    void StartResizing()
    {
        
        foreach ( var bone in handSkeleton.Bones)
        {
            if ( bone.Id == OVRSkeleton.BoneId.Hand_ThumbTip)
            {
                thumbPos = bone.Transform.position;
            }
            if ( bone.Id == OVRSkeleton.BoneId.Hand_IndexTip)
            {
                indexPos = bone.Transform.position;
            }
        }  
         
        initialDistance = Vector3.Distance(indexPos, thumbPos);
        initialScale = transform.localScale;
        isResizing = true;
    }
    

    void StopResizing()
    {
        isResizing = false;
    }

    void ResizeObject()
    {
        foreach ( var bone in handSkeleton.Bones)
        {
            if ( bone.Id == OVRSkeleton.BoneId.Hand_ThumbTip)
            {
                thumbPos = bone.Transform.position;
            }
            if ( bone.Id == OVRSkeleton.BoneId.Hand_IndexTip)
            {
                indexPos = bone.Transform.position;
            }
        }  
            
        float currentDistance = Vector3.Distance(indexPos, thumbPos);
        float scaleFactor = currentDistance / initialDistance;
        canvasTransform.localScale = initialScale * scaleFactor;
    }
    }




