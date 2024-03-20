using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameObject actor;
    Animator Anim;
    Command KeyQ, KeyW, KeyE, KeyS;
    List<Command> oldCommands = new List<Command>();

    Coroutine ReplayMovesCoroutine;
    bool shouldStartReplay;
    bool isReplaying;

    // Start is called before the first frame update
    void Start()
    {
        KeyQ = new PerformJump();
        KeyW = new PerformPunch();
        KeyE = new PerformKick();
        KeyS = new PerformWalk();
        Anim = actor.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isReplaying)
        {
            HandleInputs();
        }
        else
        {
            StartReplay();
        }
    }

    void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            KeyQ.Execute(Anim);
            oldCommands.Add(KeyQ);
            Camera.main.GetComponent<CameraFollow360>().player = null;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            KeyW.Execute(Anim);
            oldCommands.Add(KeyW);
            Camera.main.GetComponent<CameraFollow360>().player = null;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            KeyE.Execute(Anim);
            oldCommands.Add(KeyE);
            Camera.main.GetComponent<CameraFollow360>().player = null;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            KeyS.Execute(Anim);
            oldCommands.Add(KeyS);
            Camera.main.GetComponent<CameraFollow360>().player = actor.transform;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            shouldStartReplay = true;
            isReplaying = true;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            UndoLastCommand();
        }
    }

    void StartReplay()
    {
        if(shouldStartReplay == true && oldCommands.Count > 0)
        {
            shouldStartReplay = false;
            if(ReplayMovesCoroutine != null)
            {
                StopCoroutine(ReplayMovesCoroutine);
            }
            ReplayMovesCoroutine = StartCoroutine(ReplayMoves());
        }
    }

    IEnumerator ReplayMoves()
    {
        isReplaying = true;
        for(int i = 0; i < oldCommands.Count; i++)
        {
            oldCommands[i].Execute(Anim);
            yield return new WaitForSeconds(2f);
        }
        isReplaying = false;
    }

    void UndoLastCommand()
    {
        Command c = oldCommands[oldCommands.Count - 1];
        c.Execute(Anim);
        oldCommands.RemoveAt(oldCommands.Count-1);
    }
}
