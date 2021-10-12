using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PumpScript : MonoBehaviour
{
    public bool Player1;
    public bool Player2;

    public GameObject handle;
    public GameObject balloon;
    public Transform balloonTarget;
    public Transform lookAtTarget;

    public float maxSize;
    public float minSize;
    public float growFactor;

    public bool pumpingDown;

    public float pumpingPower;
    public float maxPower;
    public float minPower;


    private Vector3 _direction;
    private Quaternion _lookRotation;

    public Material red;
    public Material green;

    public GameObject explosion;
    public GameManager gameManager;

    AudioSource pumpSound;

    public TextMeshProUGUI rateText;
    public TextMeshProUGUI minPowerText;

    private void Start()
    {
        pumpSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!gameManager.gameComplete)
        {

            if (pumpingDown)
            {
                if (balloon)
                {
                    balloon.transform.localScale += new Vector3(1, 0, 0) * Time.deltaTime * (pumpingPower / 3);
                    balloon.transform.position = Vector3.MoveTowards(balloon.transform.position, balloonTarget.position, Time.deltaTime * (pumpingPower * 5));

                    _direction = (lookAtTarget.position - balloon.transform.position).normalized;
                    _lookRotation = Quaternion.LookRotation(_direction);
                    balloon.transform.rotation = Quaternion.Slerp(balloon.transform.rotation, _lookRotation, Time.deltaTime * pumpingPower);

                    //if too big, then win
                    if (balloon.transform.localScale.x >= 1.3f)
                    {
                        gameManager.gameComplete = true;
                        Instantiate(explosion, balloon.transform.position, Quaternion.identity);
                        if (Player1)
                        {
                            StartCoroutine(gameManager.winning(1));
                        }
                        if (Player2)
                        {
                            StartCoroutine(gameManager.winning(2));
                        }

                        Destroy(balloon);
                    }
                }
            }

            if (pumpingPower >= maxPower)
            {
                GetComponent<MeshRenderer>().material = green;
            }
            else
            {
                GetComponent<MeshRenderer>().material = red;
            }

            //show pumping power
            if (Input.GetKeyDown(KeyCode.DownArrow) && Player1 || Input.GetKeyDown(KeyCode.S) && Player2)
            {
                pumpSound.Play();
            }

            //if release down, pumping is false
            if (Input.GetKeyUp(KeyCode.DownArrow) && Player1 || Input.GetKeyUp(KeyCode.S) && Player2)
            {
                pumpingDown = false;
            }

            if (Input.GetKey(KeyCode.DownArrow) && Player1 || Input.GetKey(KeyCode.S) && Player2)
            {
                pumpingDown = true;

                if (minSize < transform.localScale.y)
                {
                    handle.transform.position += new Vector3(0, 1, 0) * Time.deltaTime * -growFactor;
                    transform.localScale += new Vector3(-0.2f, 1, 0) * Time.deltaTime * -growFactor; 
                }

                if (pumpingPower > minPower)
                {
                    pumpingPower -= 0.1f;
                }
                else
                {
                    pumpingPower = minPower - 0.001f;
                }
            }





            //if press up, pumping is false
            if (Input.GetKey(KeyCode.UpArrow) && Player1 || Input.GetKey(KeyCode.W) && Player2)
            {
                pumpingDown = false;

                if (maxSize > transform.localScale.y)
                {
                    handle.transform.position += new Vector3(0, 1, 0) * Time.deltaTime * growFactor;
                    transform.localScale += new Vector3(-0.2f, 1, 0) * Time.deltaTime * growFactor;
                }

                if (pumpingPower < maxPower)
                {
                    pumpingPower += 0.05f;
                }
            }

            if (rateText)
            {
                rateText.text = pumpingPower.ToString();
            }
            if (minPowerText)
            {
                minPowerText.text = minPower.ToString();
            }
        }
    }



}


/*if (transform.localScale.y !> minSize)
{
    balloon.transform.localScale += new Vector3(1, 0, 0) * Time.deltaTime * (transform.localScale.y / 3);
    balloon.transform.position = Vector3.MoveTowards(balloon.transform.position, balloonTarget.position, Time.deltaTime * (transform.localScale.y * 5));

    _direction = (lookAtTarget.position - balloon.transform.position).normalized;
    _lookRotation = Quaternion.LookRotation(_direction);
    balloon.transform.rotation = Quaternion.Slerp(balloon.transform.rotation, _lookRotation, Time.deltaTime * transform.localScale.y);
}*/