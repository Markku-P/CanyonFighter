using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Update(){
        if (Input.anyKey)
        {
            //SceneManager.LoadScene("DesertScene01", LoadSceneMode.Additive);
            Application.LoadLevel("DesertScene01");
        }
    }
}