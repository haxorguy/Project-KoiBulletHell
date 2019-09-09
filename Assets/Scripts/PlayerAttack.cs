using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;
    public float timer = 1.5f;

    private enum Form { Sword, Bow };
    Form form = Form.Sword;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //Melee
        if (form == Form.Sword && Input.GetKey(KeyCode.X))
        {
            if (playerMovement.grounded)
            {
                //First Attack

                //charge attack
                while (timer >= 0.0f)
                    timer -= Time.deltaTime;
                if (timer <= 0.0f && Input.GetKeyUp(KeyCode.X))
                {

                }

                //if not, check if button is pressed again within time
                //same check as last for 3 hits
            }
            if(!playerMovement.grounded)
            {
                //Single air attack

            }
            
        }
        //Ranged
        else if (form == Form.Bow && Input.GetKey(KeyCode.X) && playerMovement.grounded)
        {
            //First Attack

            //Charge attack
            while (timer >= 0.0f)
                timer -= Time.deltaTime;
            if(timer <= 0.0f && Input.GetKeyUp(KeyCode.X))
            {
                //charged shot
            }


            //If not, then end


            //Aim when grounded
            if (Input.GetKeyDown(KeyCode.A) && playerMovement.grounded)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow)) // NW
                    {
                        Debug.DrawLine(transform.position, new Vector3(transform.position.x - 100, transform.position.y + 100, transform.position.z));
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow)) // NE
                    {
                        Debug.DrawLine(transform.position, new Vector3(transform.position.x + 100, transform.position.y + 100, transform.position.z));
                    }
                    else //N
                        Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y + 100, transform.position.z));
                }
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
            }
        }



        //Change form
        if(Input.GetKeyDown(KeyCode.C))
        {
            changeForm();
        }

        void changeForm()
        {
            if (form == Form.Sword)
            {
                form = Form.Bow;
                //animation
                //cooldown
            }
            else
            {
                form = Form.Sword;
                //animation
                //cooldown
            }
        }
    }
}
