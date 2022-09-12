using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipController : MonoBehaviour
{
    public static TooltipController instance;
    private GameObject tooltipPanel;
    private Text tipContainer;

    void Start(){

        instance = this;

        tooltipPanel = GameObject.Find("Player/Main Camera/Canvas/TooltipPanel");
        tipContainer = tooltipPanel.GetComponentInChildren<Text>();
        hideToolip();
    }

    public void showTooltip(string tip){
        tipContainer.text = tip;
        tooltipPanel.SetActive(true);
    }

    public void hideToolip(){
        tipContainer.text = "";
        tooltipPanel.SetActive(false);
    }
    
}
