using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] popUps;
    private int popUpIndex;
    [SerializeField] private PlayerInput playerInput;
    private bool forwardMovement = true;
    private bool rotationMovement = false;

    private bool tutorialFinished = false;

    private void DisplayPopUp(int i)
    {
        if(i > popUps.Length - 1)
        {
            return;
        }

        foreach (GameObject go in popUps)
        {
            go.SetActive(false);
            if (popUps[i] == go)
            {
                popUps[i].SetActive(true);
                return;
            }
        }
    }

    private void Start()
    {
        popUpIndex = 0;
        DisplayPopUp(popUpIndex);
    }

    private void Update()
    {
        if(forwardMovement)
        {
            if (Mathf.Abs(playerInput.GetMovementVector().y) > 0)
            {
                IEnumerator coroutine = PopUpDelay(() => {
                    rotationMovement = true;
                });
                StartCoroutine(coroutine);

                // Next Event Sub
                forwardMovement = false;
            }
        }
        if (rotationMovement)
        {
            if (Mathf.Abs(playerInput.GetMovementVector().x) > 0)
            {
                IEnumerator coroutine = PopUpDelay(() => {
                    playerInput.OnShootPressed += PlayerInput_OnShootPressed;
                });
                StartCoroutine(coroutine);

                // Next Event Sub
                rotationMovement = false;
            }
        }
    }

    private void PlayerInput_OnShootPressed()
    {
        playerInput.OnShootPressed -= PlayerInput_OnShootPressed;
        IEnumerator coroutine = PopUpDelay(() =>
        {
            playerInput.OnDashStarted += PlayerInput_OnDashStarted;
        });
        StartCoroutine(coroutine);
    }

    private void PlayerInput_OnDashStarted()
    {
        playerInput.OnDashStarted -= PlayerInput_OnDashStarted;
        IEnumerator coroutine = PopUpDelay(() =>
        {
            IEnumerator coroutine = PopUpDelay(() =>
            {
                //Last Call - Deactivate gameObject and last pop up
                popUps[popUpIndex-1].SetActive(false);
                tutorialFinished = true;
                //this.enabled = false;
            });
            StartCoroutine(coroutine);
        });
        StartCoroutine(coroutine);
    }

    private IEnumerator PopUpDelay(Action callBack)
    {
        if(popUpIndex < popUps.Length-1)
        {
            popUps[popUpIndex].GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        yield return new WaitForSeconds(3f);
        DisplayPopUp(++popUpIndex);
        callBack();
    }

    public bool IsTutorialFinished()
    {
        return tutorialFinished;
    }

    private void OnDisable()
    {
        //StopAllCoroutines();
        playerInput.OnShootPressed -= PlayerInput_OnShootPressed;
        playerInput.OnDashStarted -= PlayerInput_OnDashStarted;
    }
}
