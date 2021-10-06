using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARKit;
using UnityEngine.XR.ARSubsystems;

public class Plane_Detection_Manager : MonoBehaviour
{
    /**GLOBAL GAME OBJECTS**/
    public GameObject warning_panel;
    public GameObject focus_panel;
    public GameObject cross_hair;
    public GameObject scene_background_gameobject;
    public GameObject firework_effect_gameobject;

    /***************************************************/

    /**** GLOBAL AR OBJECTS****/
    private List<ARRaycastHit>          AR_raycast_hit_list      = new List<ARRaycastHit>();       //!< List all AR camera hits.
    private ARRaycastManager            AR_raycast_manager       = null;                           //!< This is our RAYCAST manager.
    private bool                        intersection_detected_b  = false;                          //!< TRUE, if plane and cross.
    private bool                        touch_detected_b         = false;                          //!< TRUE, if tapped the screen.
    private Touch                       screen_touch;                                              //!< Touch screen
    private Pose                        plane_pose;
    private Projection_Manager          projection_manager;
    private Sound_Rhythm_Manager        sound_rhythm_manager;
    private ARSessionOrigin             AR_session_origin;
    private ARPlaneManager              AR_plane_manager;
    private Signal_Processing_Manager   signal_processing_manager;
    private Score_Manager               score_manager;
    /********************************************************************************************************************/

    /*Global update Coroutines*/
    private IEnumerator AR_scene_preparation_enumarator         ;
    private bool        scene_preperation_is_active_b     = true;

    private IEnumerator AR_song_rhythm_process_enumarator        ;
    private bool        song_rhythm_process_is_active_b   = false;

    /*******************************************************************/
    private void Start()
    {
        AR_raycast_manager          = gameObject.GetComponent<ARRaycastManager>();
        projection_manager          = gameObject.GetComponent<Projection_Manager>();
        sound_rhythm_manager        = gameObject.GetComponent<Sound_Rhythm_Manager>();
        AR_session_origin           = gameObject.GetComponent<ARSessionOrigin>();
        AR_plane_manager            = gameObject.GetComponent<ARPlaneManager>();
        signal_processing_manager   = gameObject.GetComponent<Signal_Processing_Manager>();
        score_manager               = gameObject.GetComponent<Score_Manager>();
        warning_panel               = GameObject.Find("Left_Side_Panel");
        focus_panel                 = GameObject.Find("Focus");
        cross_hair                  = GameObject.Find("Cross_Hair");

        //!< Start preparetion enumarator
        AR_scene_preparation_enumarator = AR_Scene_Pre_Preperation_Step_Loop();
        StartCoroutine(AR_scene_preparation_enumarator);

        //!< Only assign rhythm process enumarator, we dont start it.
        AR_song_rhythm_process_enumarator = AR_Song_Rhythm_Process_Loop();
    }

    private IEnumerator AR_Scene_Pre_Preperation_Step_Loop() 
    {
        while (scene_preperation_is_active_b == true) 
        {
            AR_Scene_Pre_Preperation_Step();

            //!< Run this 100 Hz.
            yield return new WaitForSeconds(0.01f);
        }    
    }

    private void AR_Scene_Pre_Preperation_Step()
    {
        //!< Control plane and cross hair intersection 
        intersection_detected_b = AR_raycast_manager.Raycast(cross_hair.transform.position, AR_raycast_hit_list, TrackableType.PlaneWithinPolygon);

        if (intersection_detected_b == true)
        {
            focus_panel.SetActive(true);
        }
        else 
        {
            focus_panel.SetActive(false);
        }

        if (Input.touchCount > 0)
        {
            screen_touch = Input.GetTouch(0);

            if (screen_touch.phase == TouchPhase.Ended)
            {
                touch_detected_b = true;
            }
        }
        else 
        {
            touch_detected_b = false;
        }


        if (touch_detected_b == true && intersection_detected_b == true) 
        {
            //!< Get the location of the current Plane pos.
            plane_pose = AR_raycast_hit_list[0].pose;

            //!< Ready to go.
            Start_AR_Scene_Model(AR_raycast_hit_list[0].trackableId);
        }
    }

    private void Start_AR_Scene_Model(TrackableId detected_trackable_id) 
    {
        //!< Close plane detection
        projection_manager.Close_Projection();

        //!< Close warning panel
        warning_panel.SetActive(false);

        //!< Close focus panel
        focus_panel.SetActive(false);

        //!< Play the music.
        sound_rhythm_manager.Play_The_Music();

        //!< Stop coroutine flag
        scene_preperation_is_active_b = false;

        //!< Place the scene object.
        Place_The_Base_Model_Scene();

        //!< Close detected planes except hitted plane, if any
        if (AR_plane_manager.trackables.count > 1)
        {
            var trackable_planes = AR_plane_manager.trackables;
            foreach (var trackable_plane in trackable_planes)
            {
                if (trackable_plane.trackableId != detected_trackable_id)
                {
                    trackable_plane.gameObject.SetActive(false);
                }
            }
        }

        signal_processing_manager.Generate_Rhythms(plane_pose.position, plane_pose.rotation);

        //!< Start the coroutine
        song_rhythm_process_is_active_b = true;
        StartCoroutine(AR_song_rhythm_process_enumarator);
    }

    private void Place_The_Base_Model_Scene() 
    {
        //!< We create a object that is related to scene background.
        Instantiate(scene_background_gameobject, plane_pose.position, plane_pose.rotation);
    }

    private IEnumerator AR_Song_Rhythm_Process_Loop() 
    {
        while (song_rhythm_process_is_active_b == true) 
        {
            if (Input.touchCount > 0)
            {
                screen_touch = Input.GetTouch(0);

                if (screen_touch.phase == TouchPhase.Ended)
                {
                    touch_detected_b = true;
                }
            }
            else
            {
                touch_detected_b = false;
            }

            List<GameObject> rhythm_list = GameObject.FindGameObjectsWithTag("Note_Rhythm").ToList();

            bool hit_obj = false;
            foreach (var rhythm in rhythm_list)
            {
                if (rhythm.GetComponent<Note_Process_Manager>().raycast_detected_b == true)
                {
                    hit_obj = true;
                    if (touch_detected_b == true)
                    {
                        rhythm.GetComponent<Note_Process_Manager>().Source_Play();
                        GameObject effect_obj = Instantiate(firework_effect_gameobject, rhythm.transform.position, rhythm.transform.rotation);
                        Destroy_Timer_Manager dest_timer = effect_obj.AddComponent<Destroy_Timer_Manager>();
                        dest_timer.Destroy_Timer_Init();
                        Destroy(rhythm,2);
                        rhythm.transform.Translate(Vector3.up * 100);
                        score_manager.Score_Update();
                    }
                }
            }

            if (hit_obj == true)
            {
                focus_panel.SetActive(true);
            }
            else
            {
                focus_panel.SetActive(false);
            }
            //!< Run it 1000 Hz
            yield return new WaitForSeconds(0.001f);
        }
    }
}
