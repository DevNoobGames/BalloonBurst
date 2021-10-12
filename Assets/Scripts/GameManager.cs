using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject mainCam;

    public GameObject player1CamPos;
    public GameObject player2CamPos;

    public GameObject[] objectsToRemove;

    public float winner = 0;

    public float speed = 5;
    public GameObject FinishText;
    public GameObject WinnerText;

    public bool gameComplete = false;

    public AudioSource backgroundAudio;
    public AudioSource winnerAudio;

    public IEnumerator winning(float winnera)
    {

        /*foreach (GameObject obj in objectsToRemove)
        {
            if (obj)
            {
                obj.SetActive(false);
            }
        }*/
        backgroundAudio.Stop();

        yield return new WaitForSeconds(1);
        winner = winnera;

        yield return new WaitForSeconds(0.5f);
        FinishText.SetActive(true);
        winnerAudio.Play();
        yield return new WaitForSeconds(2.5f);
        FinishText.SetActive(false);

        if (winner == 1)
        {
            WinnerText.GetComponent<TextMeshProUGUI>().text = "PUTIN  WINS!";
        }
        if (winner == 2)
        {
            WinnerText.GetComponent<TextMeshProUGUI>().text = "DEVNOOB  WINS!";
        }
        WinnerText.SetActive(true);

        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("SampleScene");
    }

    private void Update()
    {
        if (winner == 1)
        {
            mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, player1CamPos.transform.position, Time.deltaTime * speed);
            
            Vector3 to = new Vector3(17.96f, 13.67f, 0);
            mainCam.transform.eulerAngles = Vector3.Lerp(mainCam.transform.rotation.eulerAngles, to, Time.deltaTime);

        }
        if (winner == 2)
        {
            mainCam.transform.position = Vector3.MoveTowards(mainCam.transform.position, player2CamPos.transform.position, Time.deltaTime * speed);
            
            Vector3 to = new Vector3(17.96f, 13.67f, 0);
            mainCam.transform.eulerAngles = Vector3.Lerp(mainCam.transform.rotation.eulerAngles, to, Time.deltaTime);
        }
    }
}
