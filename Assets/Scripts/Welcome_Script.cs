using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Welcome_Script : MonoBehaviour
{

    public  Image panel_image;
    public  byte c_r = 255;
    public  byte c_g = 255;
    public  byte c_b = 255;
    public  byte c_a = 255;

    // Start is called before the first frame update
    void Start()
    {
        panel_image = GameObject.Find("Panel").GetComponent<Image>();
        StartCoroutine(Color_Transition());
        StartCoroutine(Welcome_Scene_Transition());
    }

    public IEnumerator Welcome_Scene_Transition()
    {
        yield return new WaitForSeconds(15f);

        //!< Go to nect scene.
        SceneManager.LoadScene("Transaction_Scene", LoadSceneMode.Single);
    }

    public IEnumerator Color_Transition() 
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            c_b -= 5;
            panel_image.color = new Color32(c_r, c_g, c_b, c_a); 
        }
    }
}
