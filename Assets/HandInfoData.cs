using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public enum HandInfoFrequency 
{
    None , 
    Once , 
    Repeat 
}
public class HandDebugSkeleton : MonoBehaviour
{
    [SerializeField]
    private OVRHand hand ; 

    [SerializeField]
    private OVRSkeleton handSkeleton ; 

    [SerializeField]
    private HandInfoFrequency handInfoFrequency =HandInfoFrequency.Once ; 

    private bool handInfoDisplayed = false ; 
    
    public TextMeshProUGUI MessageTextDistance_Q ;
    public TextMeshProUGUI MessageTextDistance_W;
    public TextMeshProUGUI MessageTextDistance_E ;
    public TextMeshProUGUI MessageTextDistance_R;
    public TextMeshProUGUI MessageTextDistance_T ;
    public TextMeshProUGUI MessageTextDistance_Y ;
    
    

    public GameObject Q ; 
    public GameObject W ; 
    public GameObject E;
    public GameObject R ;
    public GameObject T ;
    public GameObject Y ;


   



    private void Awake()
    {
        if ( !hand ) hand = GetComponent<OVRHand>();
        if ( !handSkeleton ) handSkeleton = GetComponent<OVRSkeleton>();
    }

    
    private void Update()
    {
        if ( !hand ) hand = GetComponent<OVRHand>();
        if ( !handSkeleton ) handSkeleton = GetComponent<OVRSkeleton>();
        if (hand.IsTracked )
        {
            foreach ( var bone in handSkeleton.Bones)
            {
                if ( bone.Id == OVRSkeleton.BoneId.Hand_IndexTip)
                {

                    // MessageTextIndex.text = " index : " + bone.Transform.position.ToString() ; 
                    float distance_Q = Vector3.Distance( Q.transform.position , bone.Transform.position) ;
                    float distance_W = Vector3.Distance( W.transform.position , bone.Transform.position) ;
                    float distance_E = Vector3.Distance( E.transform.position , bone.Transform.position) ; 
                    float distance_R = Vector3.Distance( R.transform.position , bone.Transform.position) ; 
                    float distance_T = Vector3.Distance( T.transform.position , bone.Transform.position) ;  
                    float distance_Y = Vector3.Distance( Y.transform.position , bone.Transform.position) ; 
                    

                    if ( distance_Q < 0.023)
                    {
                        MessageTextDistance_Q.text = " Q " ;
                    }
                    if ( distance_W < 0.023)
                    {
                        MessageTextDistance_W.text = " W " ;
                    }
                    if ( distance_E < 0.023)
                    {
                        MessageTextDistance_E.text = " E " ; 
                    }
                    if ( distance_R < 0.023)
                    {
                        MessageTextDistance_R.text = " R " ; 
                    }
                    if ( distance_T < 0.023)
                    {
                        MessageTextDistance_T.text = " T " ; 
                    }
                    if ( distance_Y < 0.023)
                    {
                        MessageTextDistance_Y.text = " Y " ; 
                    }
                }
                
            }
        }
    }
   
}

