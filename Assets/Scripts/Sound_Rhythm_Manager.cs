using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound_Rhythm_Manager : MonoBehaviour
{
    public AudioSource sound_to_be_play;

    public AudioClip   never_sleep_alone_clip;
    public AudioClip   astronomia_clip;
    public AudioClip   darkside_clip;

    public Text        song_text;

    void Start()
    {
        //!< Take the text
        song_text        = GameObject.Find("Song_Text").GetComponent<Text>();
        //!< Assing a sound
        switch (Rhythm_Gripper_Manager.current_playing_song)
        {
            case Playing_Songs.SONG_UNDEFINED:
                {
                    //!< Nothing.

                    break;
                }
            case Playing_Songs.SONG_NEVER_SLEEP_ALONE:
                {
                    sound_to_be_play.clip = never_sleep_alone_clip;
                    song_text.text        = "Song: Never Sleep Alone";

                    break;
                }
            case Playing_Songs.SONG_ASTRONOMIA:
                {
                    sound_to_be_play.clip = astronomia_clip;
                    song_text.text        = "Song: Astronomia";

                    break;
                }
            case Playing_Songs.SONG_DARKSIDE:
                {
                    sound_to_be_play.clip = darkside_clip;
                    song_text.text        = "Song: Darkside";

                    break;
                }
            default:
                {
                    //!< Nothing.

                    break;
                }
        }
    }

    public void Play_The_Music() 
    {
        sound_to_be_play.Play();
        sound_to_be_play.loop = false;
    }

    public int Get_Duration_Current_Music()
    {
        return (int)sound_to_be_play.clip.length;
    }
}
