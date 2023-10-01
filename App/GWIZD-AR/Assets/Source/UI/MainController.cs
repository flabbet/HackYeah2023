using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainController : MonoBehaviour
{
    private UIDocument document;

   
    public CenterUIController centerUIController;

    private VisualElement[] buttons;
    public VisualTreeAsset alertDocument;
    public VisualTreeAsset profileDocument;
    public VisualTreeAsset notificationDocument;


    // Start is called before the first frame update
    void Start()
    {
        document = GetComponent<UIDocument>();

        buttons = new VisualElement[4];
        buttons[0] = document.rootVisualElement.Q<VisualElement>("MapBtn");
        buttons[1] = document.rootVisualElement.Q<VisualElement>("AlertBtn");
        buttons[2] = document.rootVisualElement.Q<VisualElement>("ProfileBtn");
        buttons[3] = document.rootVisualElement.Q<VisualElement>("NotificationBtn");


        buttons[0].RegisterCallback<ClickEvent>(MapBtnOnClick);
        buttons[1].RegisterCallback<ClickEvent>(AlertBtnOnClick);
        buttons[2].RegisterCallback<ClickEvent>(ProfileBtnOnClick);
        buttons[3].RegisterCallback<ClickEvent>(NotificationBtnOnClick);

    }

    private void NotificationBtnOnClick(ClickEvent evt)
    {
        centerUIController.SetDocument(notificationDocument);

    }

    private void ProfileBtnOnClick(ClickEvent evt)
    {
        centerUIController.SetDocument(profileDocument);
    }

    private void AlertBtnOnClick(ClickEvent evt)
    {
        centerUIController.SetDocument(alertDocument);
    }

    private void MapBtnOnClick(ClickEvent evt)
    {
        centerUIController.SetDocument(null);
    }
}
