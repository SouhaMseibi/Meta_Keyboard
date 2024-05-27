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

using TMPro;
using UnityEngine;

namespace Keyboard
{
    public class LetterKey : Key
    {
        [SerializeField] private string character;
        private TextMeshProUGUI buttonText;
        
       
        
        protected override void Awake()
        {
            base.Awake();
            buttonText = GetComponentInChildren<TextMeshProUGUI>();
            InitializeKey();
        }

        private void InitializeKey() => buttonText.text = keyboard.autoCapsAtStart ? character.ToUpper() : character;

        protected override void OnPress()
        {
            base.OnPress();
            if (!isButtonPressed)
            {
                isButtonPressed = true ; 
                keyChannel.RaiseKeyPressedEvent(character); // ajouter ici   
                isButtonPressed = false ; 
            }
               
        }
        

        protected override void UpdateKey()
        {
            if(keyboard.IsShiftActive() || keyboard.IsCapsLockActive())
            {
                buttonText.text = character.ToUpper();
            }
            else
            {
                buttonText.text = character.ToLower();
            }
        }
    }
}



// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections.Generic;
// using System.Collections;

// using TMPro;
// using UnityEngine;

// namespace Keyboard
// {
//     public class LetterKey : Key
//     {
//         [SerializeField] private string character;
//         private TextMeshProUGUI buttonText;
//         private bool Clicked = false ;


//         protected override void Awake()
//         {
//             base.Awake();
//             handRight = GetComponent<OVRHand>();
//             handSkeletonRight = GetComponent<OVRSkeleton>();
//             buttonText = GetComponentInChildren<TextMeshProUGUI>();
//             InitializeKey();
//         }

//         private void InitializeKey() => buttonText.text = keyboard.autoCapsAtStart ? character.ToUpper() : character;

//         protected override void OnPress()
//         {
//             base.OnPress();
//             if ( !IsClicking )
//             {
//                 IsClicking =true ;                              // Flag To block other buttons from being clicked 
//                 keyChannel.RaiseKeyPressedEvent(character);     // Insertion of the character in the Input Filed 
//                 IsClicking = false ;                            // Unblock so Other buttons scan be pressed 
                 
//             }
            
//         }
//         private void ResetKeyPress()
//         {
//             IsClicking = false; // Allow key press again
//         }

//         protected override void UpdateKey()
//         {
//             if(keyboard.IsShiftActive() || keyboard.IsCapsLockActive())
//             {
//                 buttonText.text = character.ToUpper();
//             }
//             else
//             {
//                 buttonText.text = character.ToLower();
//             }
//         }
//         public void Update()
//         {
//             if ( !handRight ) handRight = GetComponent<OVRHand>();
//             if ( !handSkeletonRight ) handSkeletonRight = GetComponent<OVRSkeleton>();

//             if (handRight.IsTracked  ) 
//             {
//                 Vector3 RightPos = HandleInput ( FingerTipsPos ( handRight , handSkeletonRight) ) ;  
//                 if ( CheckDistance( RightPos , button.transform.position )  &&  (Clicked==false ) )
//                 {
//                     button.onClick.Invoke() ;
//                     Clicked = true ;
//                 }
//                 if ( CheckDistance( RightPos , button.transform.position )  && (Clicked==true ) )
//                 {
//                     base.ResetClick() ;
//                     Clicked = false ; 
//                 }

//             }
//             // if (handLeft.IsTracked  ) 
//             // {
//             //     Vector3 RightPos = HandleInput ( FingerTipsPos ( handRight , handSkeletonRight) ) ;  
//             //     if ( CheckDistance( RightPos , button.transform.position )  &&  (Clicked==false ) )
//             //     {
//             //         button.onClick.Invoke() ;
//             //         Clicked = true ;
//             //     }
//             //     if ( CheckDistance( RightPos , button.transform.position )  && (Clicked==true ) )
//             //     {
//             //         ResetClick() ;
//             //         Clicked = false ; 
//             //     }

//             // }
//     }
// }
// }