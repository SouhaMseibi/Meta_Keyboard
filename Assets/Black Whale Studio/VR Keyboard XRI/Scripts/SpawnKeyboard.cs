// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine..XR.Interaction.Toolkit ; 
// using UnityEngine.XR.ARFoundation ; 

// public class NewBehaviourScript : MonoBehaviour
// {
//     public XRRayInteractor rayInteractor ; 
//     public ARAnchorMnagaer anchorManager ;
    
//     // Start is called before the first frame update
//     void Start()
//     {
//         rayInteractor.selectEntered.AddListener(SapwnKeyboard);
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
//     public void async async void SapwnKeyboard ( BaseInteractionEventArgs args )
//     {
//         rayInteractor.TryCurrent3DRaycasHit( out RaycastHit hit ) ; 
//         Pose hitPose = new Pose ( hit.point , Quaternion.LookRotation(-hit.normal)  ) ;
//         var result = await anchorManager.TryAddAnchorAsync ( hitPose );
//         bool success = result.TryGetResult(out var anchor ) ; 
//     }
// }
