using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Movement : MonoBehaviour
{
    Vector3 targetPosition;
    Vector3 startPosition;

    public bool moveAble = true;
    bool moving;

    [SerializeField] float moveSpeed = 5f;

    [SerializeField] BioTechSO bioTechSO = null;
    [SerializeField] ArmamentClass armamentClass = null;

    void Start()
    {
        bioTechSO.currentMP = bioTechSO.MP;
        for (int i = 0; i < bioTechSO.position.Length; i++)
        {
            bioTechSO.currentPosition[i] = bioTechSO.position[i];
        }
    }

    void Update()
    {
        if (armamentClass != null)
        {
            if (bioTechSO.currentMP == -1 && armamentClass.weaponEnable == 1)
            {
                moveAble = false;
            }
            else if (armamentClass.weaponEnable == 0)
            {
                Movement();
            }
        }
        void Movement()
        {
            if (moving)
            {
                if (Vector3.Distance(startPosition, transform.position) > 1f)
                {
                    transform.position = targetPosition;
                    moving = false;
                    return;
                }

                transform.position += (targetPosition - startPosition) * moveSpeed * Time.deltaTime;
                return;
            }


            if (moveAble == true)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    PositionUpdate("up");
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    PositionUpdate("down");
                }
                else if (Input.GetKeyDown(KeyCode.A))
                {
                    PositionUpdate("left");
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    PositionUpdate("right");
                }
            } 
        }
        void PositionUpdate(string dir)
        {
            if (dir == "up")
            {
                for (int i = 0; i <= bioTechSO.currentPosition.Length - 1; i++)
                {
                    if ((bioTechSO.currentPosition[i] == 1) || (bioTechSO.currentPosition[i] == 2) || (bioTechSO.currentPosition[i] == 3) || (bioTechSO.currentPosition[i] == 4) || (bioTechSO.currentPosition[i] == 5))
                    {
                        Debug.Log("out of bounds up");
                        break;
                    }
                    else if (i == bioTechSO.currentPosition.Length - 1 && bioTechSO.currentMP != 0)
                    {
                        PositionUp();
                    }
                }
            }

            if (dir == "down")
            {
                for (int i = 0; i <= bioTechSO.currentPosition.Length - 1; i++)
                {
                    if ((bioTechSO.currentPosition[i] == 21) || (bioTechSO.currentPosition[i] == 22) || (bioTechSO.currentPosition[i] == 23) || (bioTechSO.currentPosition[i] == 24) || (bioTechSO.currentPosition[i] == 25))
                    {
                        Debug.Log("out of bounds down");
                        break;
                    }
                    else if (i == bioTechSO.currentPosition.Length - 1 && bioTechSO.currentMP != 0)
                    {
                        PositionDown();
                    }
                }
            }

            if (dir == "right")
            {
                for (int i = 0; i <= bioTechSO.currentPosition.Length - 1; i++)
                {
                    if ((bioTechSO.currentPosition[i] == 5) || (bioTechSO.currentPosition[i] == 10) || (bioTechSO.currentPosition[i] == 15) || (bioTechSO.currentPosition[i] == 20) || (bioTechSO.currentPosition[i] == 25))
                    {
                        Debug.Log("out of bounds right");
                        break;
                    }
                    else if (i == bioTechSO.currentPosition.Length - 1 && bioTechSO.currentMP != 0)
                    {
                        PositionRight();
                    }
                }
            }
            if (dir == "left")
            {
                for (int i = 0; i <= bioTechSO.currentPosition.Length - 1; i++)
                {
                    if ((bioTechSO.currentPosition[i] == 1) || (bioTechSO.currentPosition[i] == 6) || (bioTechSO.currentPosition[i] == 11) || (bioTechSO.currentPosition[i] == 16) || (bioTechSO.currentPosition[i] == 21))
                    {
                        Debug.Log("out of bounds left");
                        break;
                    }
                    else if (i == bioTechSO.currentPosition.Length - 1 && bioTechSO.currentMP != 0)
                    {
                        PositionLeft();
                    }
                }
            }

        }

        void PositionRight()
        {
            targetPosition = transform.position + Vector3.right;
            startPosition = transform.position;
            moving = true;

            for (int i = 0; i <= bioTechSO.currentPosition.Length - 1; i++)
            {
                bioTechSO.currentPosition[i] = bioTechSO.currentPosition[i] + 1;
                //Debug.Log("" + currentForm[i].ToString());
            }
            bioTechSO.currentMP = bioTechSO.currentMP - 1;
        }

        void PositionLeft()
        {
            targetPosition = transform.position + Vector3.left;
            startPosition = transform.position;
            moving = true;

            for (int i = 0; i <= bioTechSO.currentPosition.Length - 1; i++)
            {
                bioTechSO.currentPosition[i] = bioTechSO.currentPosition[i] - 1;
                //Debug.Log("" + currentForm[i].ToString());
            }
            bioTechSO.currentMP = bioTechSO.currentMP - 1;
        }

        void PositionUp()
        {
            targetPosition = transform.position + Vector3.forward;
            startPosition = transform.position;
            moving = true;

            for (int i = 0; i <= bioTechSO.currentPosition.Length - 1; i++)
            {
                bioTechSO.currentPosition[i] = bioTechSO.currentPosition[i] - 5;
                //Debug.Log("" + currentForm[i].ToString());
            }
            bioTechSO.currentMP = bioTechSO.currentMP - 1;
        }

        void PositionDown()
        {
            targetPosition = transform.position + Vector3.back;
            startPosition = transform.position;
            moving = true;

            for (int i = 0; i <= bioTechSO.currentPosition.Length - 1; i++)
            {
                bioTechSO.currentPosition[i] = bioTechSO.currentPosition[i] + 5;
                //Debug.Log("" + currentForm[i].ToString());
            }
            bioTechSO.currentMP = bioTechSO.currentMP - 1;
        }
    }
}
