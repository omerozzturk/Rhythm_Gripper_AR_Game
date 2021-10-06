using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class Note_Process_Manager : MonoBehaviour
{
    public bool raycast_detected_b = false;
    private Ray ray;

    public  GameObject  cross_hair_gameobject;
    public  AudioSource audio_source;
    private Vector2     sc_coord_vec2;
    public Text asf_text;


    private void Start()
    {
        StartCoroutine(Update_Loop());
    }

    // Update is called once per frame
    private IEnumerator Update_Loop()
    {
        while (true)
        {
            cross_hair_gameobject = GameObject.Find("Cross_Hair");
            ray = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>().camera.ScreenPointToRay(cross_hair_gameobject.transform.position);
            sc_coord_vec2 = GameObject.Find("AR Session Origin").GetComponent<ARSessionOrigin>().camera.WorldToScreenPoint(gameObject.transform.position);
            audio_source = gameObject.GetComponent<AudioSource>();
            asf_text = GameObject.Find("AR_Type_Text").GetComponent<Text>();

            if (Rhythm_Gripper_Manager.current_AR_orientation_type == AR_Scene_Displacement_Types.AR_SCENE_TYPE_GROUND)
            {
                gameObject.transform.Translate(Vector3.up * Time.deltaTime * 4f);
            }
            else
            {
                gameObject.transform.Translate(Vector3.back * Time.deltaTime * 4f);
            }


            float dist = Vector2.Distance(cross_hair_gameobject.transform.position, sc_coord_vec2);

            if (dist <= 120f || Physics.Raycast(ray) == true )
            {
                raycast_detected_b = true;
            }
            else
            {
                raycast_detected_b = false;
            }

            //!< Run it 100 Hz
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void Source_Play()
    {
        if (audio_source.isPlaying == false)
        {
            audio_source.Play();
        }
    }
}
