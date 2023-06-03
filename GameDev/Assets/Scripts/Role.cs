using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : MonoBehaviour
{
    public Route currentRoute;
    public DoTask task;
    int routePosition;
    public int steps;
    public bool isMoving;
    public string nodeType;

    // only use update for input!
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            // the max point will be 7-1
            steps = Random.Range(1, 7);
            Debug.Log("Dice Rolled " + steps);
            if (routePosition + steps < currentRoute.childNodeList.Count)
            {
                StartCoroutine(Move());
            }
            else
            {
                Debug.Log("Rolled number is too high.");
            }
        }
    }

    // coroutine: doing everything next or in an extra thread without touching update loop which is running 60 times a second (too mucg costing)
    IEnumerator Move()
    {
        if (isMoving)
        {
            // paused and resumed coroutine
            yield break;
        }

        isMoving = true;

        while (steps > 0)
        {
            Vector3 nextPos = currentRoute.childNodeList[routePosition + 1].position;
            // return true: (position != goal): still moving -> wait for it to reach goal
            while (MoveToNextNode(nextPos))
            {
                yield return null;
            }
            yield return new WaitForSeconds(0.1f);
            steps--;
            routePosition++;
        }
        isMoving = false;
        nodeType = currentRoute.childNodeList[routePosition].tag;
        switch (nodeType)
        {
            case "event":
                task.RandomEvent();
                break;
            case "fight":
                task.FightMonster();
                break;
            case "shop":
                task.Shop();
                break;
        }
    }

    bool MoveToNextNode(Vector3 goal)
    {
        return goal != (transform.position = Vector3.MoveTowards(transform.position, goal, 2f * Time.deltaTime));
    }
}