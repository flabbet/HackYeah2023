using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CenterUIController : MonoBehaviour
{
    private UIDocument document;
    //private Button animalBtn;
    private Button backBtn;
    public VisualTreeAsset alertAsset;
    
 
    public void SetDocument(VisualTreeAsset asset)
    {
        if (document != null)
        { 
            document.enabled = true;
            document.visualTreeAsset = asset;
            backBtn = document.rootVisualElement.Q<Button>("BackButton");
            backBtn.clicked += OnBackButtonClicked;
            //if(alertAsset == asset)
            //{
                //animalBtn = document.rootVisualElement.Q<Button>("AnimalBtn");
                //animalBtn.clicked += Animal_Btn_Clicked;
            //}
        }
        else
        {
            document.enabled = false;
        }
    }

    private void OnBackButtonClicked()
    {
        document.enabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        document = GetComponent<UIDocument>();

       
    }

    private void Animal_Btn_Clicked()
    {
        SceneManager.LoadScene(2);
    }
}
