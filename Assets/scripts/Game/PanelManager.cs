using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour {

    public GameObject adventurersPanel;
    public GameObject questsPanel;
    public GameObject questsBoardPanel;
    public GameObject recruitmentPanel;

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadAdventurersPanel()
    {
        if (questsPanel.activeInHierarchy)
        {
            questsPanel.SetActive(false);
        }
        if (questsBoardPanel.activeInHierarchy)
        {
            questsBoardPanel.SetActive(false);
        }
        if (recruitmentPanel.activeInHierarchy)
        {
            recruitmentPanel.SetActive(false);
        }
        adventurersPanel.SetActive(!adventurersPanel.activeInHierarchy);
    }

    public void LoadQuestsPanel()
    {
        if (adventurersPanel.activeInHierarchy)
        {
            adventurersPanel.SetActive(false);
        }
        if (questsBoardPanel.activeInHierarchy)
        {
            questsBoardPanel.SetActive(false);
        }
        if (recruitmentPanel.activeInHierarchy)
        {
            recruitmentPanel.SetActive(false);
        }
        questsPanel.SetActive(!questsPanel.activeInHierarchy);
    }

    public void LoadQuestsBoardPanel()
    {
        if (adventurersPanel.activeInHierarchy)
        {
            adventurersPanel.SetActive(false);
        }
        if (questsPanel.activeInHierarchy)
        {
            questsPanel.SetActive(false);
        }
        if (recruitmentPanel.activeInHierarchy)
        {
            recruitmentPanel.SetActive(false);
        }
        questsBoardPanel.SetActive(!questsBoardPanel.activeInHierarchy);
    }

    public void LoadRecruitmentPanel()
    {
        if (adventurersPanel.activeInHierarchy)
        {
            adventurersPanel.SetActive(false);
        }
        if (questsPanel.activeInHierarchy)
        {
            questsPanel.SetActive(false);
        }
        if (questsBoardPanel.activeInHierarchy)
        {
            questsBoardPanel.SetActive(false);
        }
        recruitmentPanel.SetActive(!recruitmentPanel.activeInHierarchy);
    }
}
