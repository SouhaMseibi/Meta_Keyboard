/*
 * Copyright (c) 2023 Black Whale Studio. All rights reserved.
 *
 * This software is the intellectual property of Black Whale Studio. Direct use, copying, or distribution of this code in its original or only slightly modified form is strictly prohibited. Significant modifications or derivations are required for any use.
 *
 * If this code is intended to be used in a commercial setting, you must contact Black Whale Studio for explicit permission.
 *
 * For the full licensing terms and conditions, visit:
 * https://blackwhale.dev/
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT ANY WARRANTIES OR CONDITIONS.
 *
 * For questions or to join our community, please visit our Discord: https://discord.gg/55gtTryfWw
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro; 
namespace Keyboard
{
    public class Key : MonoBehaviour
    {
        [SerializeField] protected KeyChannel keyChannel;
        [SerializeField] protected KeyboardManager keyboard;
        [SerializeField] protected OVRHand hand ; 
        // [SerializeField] protected OVRHand handLeft ;
        [SerializeField] protected OVRSkeleton handSkeleton ; 
        [SerializeField] public TextMeshProUGUI MessageText ;

        protected Button button;
        protected float MaxtriggerDistance = 0.0219f ; 
        protected float MintriggerDistance = 0.0211f ;
        protected bool isClicked = false ; 
        protected bool Clicked = false ; 
        protected bool IsClicking = false ; 
        protected float ClickTimeDelay = 1f ;
        protected float lastClickTime =0f;
        public static bool isButtonPressed = false ;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnPress);
            keyboard.onKeyboardModeChanged.AddListener(UpdateKey);
            keyChannel.onFirstKeyPress.AddListener(UpdateKey);
            keyChannel.OnKeyColorsChanged += ChangeKeyColors; 
            keyChannel.OnKeysStateChange += ChangeKeyState;
        }

        
        protected virtual void OnDestroy()
        {
            button.onClick.RemoveListener(OnPress);
            keyboard.onKeyboardModeChanged.RemoveListener(UpdateKey);
            keyChannel.onFirstKeyPress.RemoveListener(UpdateKey);
            keyChannel.OnKeyColorsChanged -= ChangeKeyColors;
            keyChannel.OnKeysStateChange -= ChangeKeyState;
        }

        protected virtual void OnPress()
        {

            keyboard.DeactivateShift();
        }

        protected virtual void UpdateKey()
        {
            // empty method for override in child classes
        }
        
        protected void ChangeKeyColors(Color normalColor, Color highlightedColor, Color pressedColor, Color selectedColor)
        {
            ColorBlock cb = button.colors;
            cb.normalColor = normalColor;
            cb.highlightedColor = highlightedColor;
            cb.pressedColor = pressedColor;
            cb.selectedColor = selectedColor;
            button.colors = cb;
        }

        protected void ChangeKeyState(bool enabled)
        {
            button.interactable = enabled;
        }


        public List<Vector3> FingerTipsPos ( OVRHand hand , OVRSkeleton handSkeleton )
        {
            List<Vector3> FingersPosRight = new List<Vector3>() ; 
            OVRSkeleton.SkeletonType type = handSkeleton.GetSkeletonType () ; 
            if ( type == OVRSkeleton.SkeletonType.HandRight)
            {
                foreach ( var bone in handSkeleton.Bones)
                {
                    if ( bone.Id == OVRSkeleton.BoneId.Hand_ThumbTip   )
                    {
                        FingersPosRight.Add(bone.Transform.position);
                    }
                    if ( bone.Id == OVRSkeleton.BoneId.Hand_IndexTip  )
                    {
                        FingersPosRight.Add(bone.Transform.position);
                    }
                    if ( bone.Id == OVRSkeleton.BoneId.Hand_MiddleTip )
                    {
                        FingersPosRight.Add(bone.Transform.position);
                    }
                    if ( bone.Id == OVRSkeleton.BoneId.Hand_RingTip)
                    {
                        FingersPosRight.Add(bone.Transform.position);
                    }
                    if ( bone.Id == OVRSkeleton.BoneId.Hand_PinkyTip )
                    {
                        FingersPosRight.Add(bone.Transform.position);
                    }
                }
            }
            // else 
            // {
            //     foreach ( var bone in handSkeleton.Bones)
            //     {
            //         if ( bone.Id == OVRSkeleton.BoneId.Hand_ThumbTip && CheckDistance ( bone.Transform.position ,button.transform.position  ))
            //         {
            //             FingersPosLeft.Add(bone.Transform.position);
            //         }
            //         if ( bone.Id == OVRSkeleton.BoneId.Hand_IndexTip && CheckDistance ( bone.Transform.position ,button.transform.position  ) )
            //         {
            //             FingersPosLeft.Add(bone.Transform.position);
            //         }
            //         if ( bone.Id == OVRSkeleton.BoneId.Hand_MiddleTip && CheckDistance ( bone.Transform.position ,button.transform.position  ))
            //         {
            //             FingersPosLeft.Add(bone.Transform.position);
            //         }
            //         if ( bone.Id == OVRSkeleton.BoneId.Hand_RingTip && CheckDistance ( bone.Transform.position ,button.transform.position  ))
            //         {
            //             FingersPosLeft.Add(bone.Transform.position);
            //         }
            //         if ( bone.Id == OVRSkeleton.BoneId.Hand_PinkyTip && CheckDistance ( bone.Transform.position ,button.transform.position  ))
            //         {
            //             FingersPosLeft.Add(bone.Transform.position);
            //         }
            //     }
            // }
            
            
            return FingersPosRight ; // , FingersPosLeft ; 
        }

        public bool CheckDistance ( Vector3 buttonPos , Vector3 FingerPos ) 
        {
            if (( MintriggerDistance <= Vector3.Distance( buttonPos , FingerPos )) &&  (Vector3.Distance( buttonPos , FingerPos )<= MaxtriggerDistance ) )
            {    return true ; }
            
            return false ;  
        }
        // The coordinate of the Finger in action !!
        public Vector3 HandleInput (List<Vector3> FingersPosRight  )
        {
            if ( FingersPosRight.Count > 1)
            { 
                return  FingersPosRight[0]; // FingersPosLeft[0] ;
            }
            else 
            {   return FingersPosRight[0] ;
                // Vector3 LeftPos = LeftPos ; 
                
            }
        }

        
        public bool  CalculateDistance ( List<Vector3> FingersPosRight , bool clicked  )
        {
            foreach ( var finger in FingersPosRight )
            { 
                float distance = Vector3.Distance( button.transform.position , finger ) ; 
                Vector3 direction = Vector3.forward ;
                Vector3 normalizedDirection = direction.normalized;                                 // ensures that the calculations reflect changes along the direction accurately, without being influenced by the original length of the direction vector.
                float fingerProjection = Vector3.Dot(finger, normalizedDirection);                  // project the position vectors of GameObjects onto the normalized direction vector.
                float boneProjection = Vector3.Dot(button.transform.position, normalizedDirection);
                float distanceAlongDirection = Mathf.Abs(fingerProjection - boneProjection);
                if (( MintriggerDistance <= Vector3.Distance( button.transform.position , finger )) &&  (Vector3.Distance( button.transform.position , finger )<= MaxtriggerDistance ) ) 
                // if ( ( distanceAlongDirection <= 0.005 )&&(distanceAlongDirection >= 0.006))
                {
                    Clicked =  true ; 
                }
            }
            return Clicked ; 
        }

        public IEnumerator ResetClick()
        {
            yield return new WaitForSeconds(ClickTimeDelay);
            
        }
        
        protected void Update ( )         // Original
        { 
            if ( !hand ) hand = GetComponent<OVRHand>();
            if ( !handSkeleton ) handSkeleton = GetComponent<OVRSkeleton>();
            if (hand.IsTracked  ) 
            {
                List <Vector3> FingersPosRight ; 

                FingersPosRight = FingerTipsPos ( hand , handSkeleton);  
                OVRSkeleton.SkeletonType type = handSkeleton.GetSkeletonType () ; 
                MessageText.text = "HandType : " + type.ToString() ; 
                if ( CalculateDistance ( FingersPosRight , Clicked ) && !isClicked)
                {
                    keyChannel.OnKeyPressed += keyboard.KeyPress;
                    button.onClick.Invoke() ;
                    // chnage the update in the Letterkey 
                    // add flag to the OnPress 
                    // time delay 
                    // button.interactable = true ; // with collider ? 
                    // IsButtonPressed = false , so another button can be clicked 
                    isClicked =true;
                }
                else if ( CalculateDistance ( FingersPosRight , Clicked ) && isClicked)
                {
                    keyChannel.OnKeyPressed -= keyboard.KeyPress;
                    if ( (Time.time - lastClickTime) < ClickTimeDelay)
                    {
                        
                    }
                    else 
                    {
                        isClicked = false ; 
                        
                    }
                    lastClickTime = Time.time ; 
                }
                else                    // if distance > Threshold
                {
                    keyChannel.OnKeyPressed -= keyboard.KeyPress;
                    isClicked = false ;           
                }
                Clicked=false ;
                

            
        }
        
}
}
}










/*
 * Copyright (c) 2023 Black Whale Studio. All rights reserved.
 *
 * This software is the intellectual property of Black Whale Studio. Direct use, copying, or distribution of this code in its original or only slightly modified form is strictly prohibited. Significant modifications or derivations are required for any use.
 *
 * If this code is intended to be used in a commercial setting, you must contact Black Whale Studio for explicit permission.
 *
 * For the full licensing terms and conditions, visit:
 * https://blackwhale.dev/
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT ANY WARRANTIES OR CONDITIONS.
 *
 * For questions or to join our community, please visit our Discord: https://discord.gg/55gtTryfWw
 */

// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections.Generic;
// using System.Collections;
// using TMPro; 

// namespace Keyboard
// {
//     public class Key : MonoBehaviour
//     {
//         [SerializeField] protected KeyChannel keyChannel;
//         [SerializeField] protected KeyboardManager keyboard;
//         [SerializeField] protected OVRHand handRight ; 
//         [SerializeField] protected OVRHand handLeft ;
//         [SerializeField] protected OVRSkeleton handSkeletonRight  ; 
//         [SerializeField] protected OVRSkeleton handSkeletonLeft  ; 
//         [SerializeField] public TextMeshProUGUI MessageText ;
//         protected Button button;
//         protected float MaxtriggerDistance = 0.0219f ; 
//         protected float MintriggerDistance = 0.0211f ;
    
        
//         protected float ClickTimeDelay = 1f ;
//         protected float lastClickTime =0f;
//         public static bool IsClicking = false;
//         // private bool canPressKey = true; // Initial state allowing key press
//         // private float debounceTime = 0.2f; // Cooldown time in seconds
        

//         protected virtual void Awake()
//         {
//             button = GetComponent<Button>();
//             button.onClick.AddListener(OnPress);
//             keyboard.onKeyboardModeChanged.AddListener(UpdateKey);
//             keyChannel.onFirstKeyPress.AddListener(UpdateKey);
//             keyChannel.OnKeyColorsChanged += ChangeKeyColors; 
//             keyChannel.OnKeysStateChange += ChangeKeyState;
//         }

//         protected virtual void OnDestroy()
//         {
//             button.onClick.RemoveListener(OnPress);
//             keyboard.onKeyboardModeChanged.RemoveListener(UpdateKey);
//             keyChannel.onFirstKeyPress.RemoveListener(UpdateKey);
//             keyChannel.OnKeyColorsChanged -= ChangeKeyColors;
//             keyChannel.OnKeysStateChange -= ChangeKeyState;
//         }

//         protected virtual void OnPress()
//         {
                
//             keyboard.DeactivateShift(); 
           
            
//         }
       

//         protected virtual void UpdateKey()
//         {
//             // empty method for override in child classes
//         }
        
//         protected void ChangeKeyColors(Color normalColor, Color highlightedColor, Color pressedColor, Color selectedColor)
//         {
//             ColorBlock cb = button.colors;
//             cb.normalColor = normalColor;
//             cb.highlightedColor = highlightedColor;
//             cb.pressedColor = pressedColor;
//             cb.selectedColor = selectedColor;
//             button.colors = cb;
//         }

//         protected void ChangeKeyState(bool enabled)
//         {
//             button.interactable = enabled;
//         }
//         public List<Vector3> FingerTipsPos ( OVRHand hand , OVRSkeleton handSkeleton )
//         {
//             List<Vector3> FingersPosRight = new List<Vector3>() ; 
//             OVRSkeleton.SkeletonType type = handSkeletonRight.GetSkeletonType () ; 
//             if ( type == OVRSkeleton.SkeletonType.HandRight)
//             {
//                 foreach ( var bone in handSkeletonRight.Bones)
//                 {
//                     if ( bone.Id == OVRSkeleton.BoneId.Hand_ThumbTip && CheckDistance ( bone.Transform.position ,button.transform.position  ))
//                     {
//                         FingersPosRight.Add(bone.Transform.position);
//                     }
//                     if ( bone.Id == OVRSkeleton.BoneId.Hand_IndexTip && CheckDistance ( bone.Transform.position ,button.transform.position  ) )
//                     {
//                         FingersPosRight.Add(bone.Transform.position);
//                     }
//                     if ( bone.Id == OVRSkeleton.BoneId.Hand_MiddleTip && CheckDistance ( bone.Transform.position ,button.transform.position  ))
//                     {
//                         FingersPosRight.Add(bone.Transform.position);
//                     }
//                     if ( bone.Id == OVRSkeleton.BoneId.Hand_RingTip && CheckDistance ( bone.Transform.position ,button.transform.position  ))
//                     {
//                         FingersPosRight.Add(bone.Transform.position);
//                     }
//                     if ( bone.Id == OVRSkeleton.BoneId.Hand_PinkyTip && CheckDistance ( bone.Transform.position ,button.transform.position  ))
//                     {
//                         FingersPosRight.Add(bone.Transform.position);
//                     }
//                 }
//             }
//             // else 
//             // {
//             //     foreach ( var bone in handSkeletonLeft.Bones)
//             //     {
//             //         if ( bone.Id == OVRSkeleton.BoneId.Hand_ThumbTip && CheckDistance ( bone.Transform.position ,button.transform.position  ))
//             //         {
//             //             FingersPosLeft.Add(bone.Transform.position);
//             //         }
//             //         if ( bone.Id == OVRSkeleton.BoneId.Hand_IndexTip && CheckDistance ( bone.Transform.position ,button.transform.position  ) )
//             //         {
//             //             FingersPosLeft.Add(bone.Transform.position);
//             //         }
//             //         if ( bone.Id == OVRSkeleton.BoneId.Hand_MiddleTip && CheckDistance ( bone.Transform.position ,button.transform.position  ))
//             //         {
//             //             FingersPosLeft.Add(bone.Transform.position);
//             //         }
//             //         if ( bone.Id == OVRSkeleton.BoneId.Hand_RingTip && CheckDistance ( bone.Transform.position ,button.transform.position  ))
//             //         {
//             //             FingersPosLeft.Add(bone.Transform.position);
//             //         }
//             //         if ( bone.Id == OVRSkeleton.BoneId.Hand_PinkyTip && CheckDistance ( bone.Transform.position ,button.transform.position  ))
//             //         {
//             //             FingersPosLeft.Add(bone.Transform.position);
//             //         }
//             //     }
//             // }
            
            
//             return FingersPosRight ; // , FingersPosLeft ; 
//         }

//         public bool CheckDistance ( Vector3 buttonPos , Vector3 FingerPos ) 
//         {
//             if (( MintriggerDistance <= Vector3.Distance( buttonPos , FingerPos )) &&  (Vector3.Distance( buttonPos , FingerPos )<= MaxtriggerDistance ) )
//             {    return true ; }
            
//             return false ;  
//         }
//         // The coordinates of the Finger in action !!
//         public Vector3 HandleInput (List<Vector3> FingersPosRight  )
//         {
//             if ( FingersPosRight.Count > 1)
//             { 
//                 return  FingersPosRight[0]; // FingersPosLeft[0] ;
//             }
//             else 
//             {    
//                 return FingersPosRight[0] ;  // LeftPos ;
//             }
//         }
//         public IEnumerator ResetClick()
//         {
//             yield return new WaitForSeconds(ClickTimeDelay);
            
//         }


        

//     }
// }