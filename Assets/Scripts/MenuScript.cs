using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MenuScript : MonoBehaviour
{
    public PumpScript[] pumps;
    public GameObject[] toDeactivate;

    public GameObject startText;
    public GameObject counterText;
    public TextMeshProUGUI counterTexTT;

    public AudioSource counterAudio;

    public void StartTheGame()
    {
        /*foreach (PumpScript pump in pumps)
        {
            pump.enabled = true;
        }
        foreach (GameObject deac in toDeactivate)
        {
            deac.SetActive(false);
        }*/
        StartCoroutine(Starter());
    }

    public IEnumerator Starter()
    {

        foreach (GameObject deac in toDeactivate)
        {
            deac.SetActive(false);
        }

        yield return new WaitForSeconds(0.3f);
        counterText.SetActive(true);
        counterTexTT.text = "3";
        counterAudio.Play();
        yield return new WaitForSeconds(1);

        counterTexTT.text = "2";
        counterAudio.Play();
        yield return new WaitForSeconds(1);

        counterTexTT.text = "1";
        counterAudio.Play();
        yield return new WaitForSeconds(1);

        counterText.SetActive(false);
        startText.SetActive(true);
        foreach (PumpScript pump in pumps)
        {
            pump.enabled = true;
        }

        yield return new WaitForSeconds(2);
        startText.SetActive(false);
    }
}
