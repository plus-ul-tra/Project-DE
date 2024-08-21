using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainButtonAction : MonoBehaviour
{
    public void ButtonAction()
    {
        // 이름 별로 동작 추가.
        switch (this.gameObject.name)
        {
            case "StartGameButton":
                SceneManager.LoadScene("Scene Test");
                Debug.Log("Clicked!");
                break;
            case "Multi Play Button":

                break;
            case "Setting Button":

                break;
        }
    }
}
