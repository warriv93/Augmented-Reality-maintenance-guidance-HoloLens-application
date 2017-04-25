using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class InstructionsManager : MonoBehaviour
{

    public List<GameObject> instructions;
    int currentInstructionIndex = 0;
    int currentInstructionIndexMarker2 = 0;


    // Use this for initialization
    void Start()
    {
        if (instructions.Count > 0)
            instructions[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            print("k key was pressed");
            NextStep();
        }


    }

    public void NextStep()
    {
        if (instructions.Count > 0)
        {
            instructions[currentInstructionIndex].SetActive(false);
            currentInstructionIndex = (currentInstructionIndex + 1) % instructions.Count;
            instructions[currentInstructionIndex].SetActive(true);
        }
    }

    public void NextStepMarker2()
    {
        if (instructions.Count > 0)
        {
            instructions[currentInstructionIndexMarker2].SetActive(false);
            currentInstructionIndexMarker2 = (currentInstructionIndexMarker2 + 1) % instructions.Count;
            instructions[currentInstructionIndexMarker2].SetActive(true);
        }
    }

}
