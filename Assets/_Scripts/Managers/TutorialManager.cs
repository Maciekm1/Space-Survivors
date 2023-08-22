using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] popUps;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private bool completeTutorial;
    private int popUpIndex;
    private bool forwardMovement = true;
    private bool rotationMovement = false;
    private bool tutorialFinished = false;

    public event Action OnTutorialFinish;

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
        if (completeTutorial)
        {
            TutorialFinished();
        }
        else
        {
            popUpIndex = 0;
            DisplayPopUp(popUpIndex);
        }
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
                TutorialFinished();
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

    private void TutorialFinished()
    {
        tutorialFinished = true;
        OnTutorialFinish?.Invoke();
        this.enabled = false;
    }
}
