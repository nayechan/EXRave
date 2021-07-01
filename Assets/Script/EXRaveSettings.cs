using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EXRaveSettings : MonoBehaviour
{
    public Image graphic, sound, ingame, debug;
    public GameObject gr_tab, snd_tab, game_tab, dbg_tab;

    public bool gr_video;
    public int gr_effect;
    public bool snd_bg, snd_sfx;
    public bool game_effect, game_laser, game_guideline, game_multi;
    public float game_spd;
    public int game_offset;
    public bool adv_debug;

    string lastScene;

    void Start()
    {
        lastScene = PlayerPrefs.GetString("lastScene_settings", "main");
    }
    void c_reset()
    {
        graphic.color = new Color32(51, 51, 51, 255);
        sound.color = new Color32(51, 51, 51, 255);
        ingame.color = new Color32(51, 51, 51, 255);
        debug.color = new Color32(51, 51, 51, 255);
        gr_tab.SetActive(false);
        snd_tab.SetActive(false);
        game_tab.SetActive(false);
        dbg_tab.SetActive(false);
    }

    public void ChangeTab(int tab)
    {
        switch(tab)
        {
            case 1:
                c_reset();
                graphic.color = new Color32(34, 34, 34, 255);
                gr_tab.SetActive(true);
                break;
            case 2:
                c_reset();
                sound.color = new Color32(34, 34, 34, 255);
                snd_tab.SetActive(true);
                break;
            case 3:
                c_reset();
                ingame.color = new Color32(34, 34, 34, 255);
                game_tab.SetActive(true);
                break;
            case 4:
                c_reset();
                debug.color = new Color32(34, 34, 34, 255);
                dbg_tab.SetActive(true);
                break;
            default:
                break;
        }
    }

    void SaveSettings()
    {
        /*
    public bool gr_video;
    public int gr_effect;
    public bool snd_bg, snd_sfx;
    public bool game_effect, game_laser, game_guideline, game_multi;
    public float game_spd;
    public int game_offset;
    public bool adv_debug;
    */
        SceneManager.LoadScene(lastScene);
    }

    void DiscardSettings()
    {
        SceneManager.LoadScene(lastScene);
    }

    void ResetSettings()
    {

    }
}
