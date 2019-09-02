//// GameMaster.cs
//// Sceneの遷移をコントロールする。


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    public static int game_scene;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void mode_select_1()
    {
        game_scene = 1;
        SceneManager.LoadScene("GameRbar");
    }


    public void mode_select_2()
    {
        game_scene = 2;
        SceneManager.LoadScene("GameProx");
    }
}
