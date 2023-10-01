using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class IntroController : MonoBehaviour
{
    private UIDocument document;
    private Button btnStart;
    


    // Start is called before the first frame update
    void Start()
    {
        document = GetComponent<UIDocument>();
        btnStart = document.rootVisualElement.Q<Button>("StartButton");
        btnStart.clicked += BtnStart_clicked;


    }

    private void BtnStart_clicked()
    {
        SceneManager.LoadScene(1);
        
    }
}
