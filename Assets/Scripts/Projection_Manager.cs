using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class Projection_Manager : MonoBehaviour
{
    public Text              projection_text;
    public ARPlaneManager    AR_plane_manager;
    void Start()
    {
        projection_text  = GameObject.Find("AR_Type_Text").GetComponent<Text>();
        AR_plane_manager = GameObject.Find("AR Session Origin").GetComponent<ARPlaneManager>();

        switch (Rhythm_Gripper_Manager.current_AR_orientation_type)
        {
            case AR_Scene_Displacement_Types.AR_SCENE_TYPE_UNDEFINED:
                {
                    projection_text.text = "UNDEFINED";

                    break; 
                }            
            case AR_Scene_Displacement_Types.AR_SCENE_TYPE_WALL:
                {
                    projection_text.text = "Vertical Projection";

                    Prepare_Vertical_Projection();

                    break;
                }
            case AR_Scene_Displacement_Types.AR_SCENE_TYPE_GROUND:
                {
                    projection_text.text = "Horizontal Projection";

                    Prepare_Horizontal_Projection();

                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private void Prepare_Horizontal_Projection() 
    {
        //!< We are waiting for a user to choose proper point
        AR_plane_manager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.Horizontal;
    }

    private void Prepare_Vertical_Projection() 
    {
        //!< We are waiting for a user to choose proper point
        AR_plane_manager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.Vertical;
    }

    public void Close_Projection() 
    {
        AR_plane_manager.requestedDetectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.None;
    }
}
