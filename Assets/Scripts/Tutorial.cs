using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public static Tutorial Instance { get; private set; }
    public MeshRenderer background1;
    public MeshRenderer background2;
    public float speed1;
    public float speed2;
    public Image tut1;
    public Image tut2;
    public Image tut3;
    public Transform spawnLocation;

    public GameObject star;
    public GameObject demon;
    public GameObject nightmare;

    public GameObject[] livesImages;
    public TMP_Text clockText;
    public TMP_Text scoreText;

    public GameObject tutEndMenu;

    private Sequence sequence;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }

        tutEndMenu.SetActive(false);
        clockText.SetText("Time: 00:00");
        scoreText.SetText("Score: 0");
        tut1.gameObject.SetActive(true);
        tut2.gameObject.SetActive(false);
        tut3.gameObject.SetActive(false);
        StartTut();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void StartTut()
    {
        sequence = DOTween.Sequence();
        sequence.Append(tut1.DOFade(0, 3).OnComplete(SpawnStar)).SetDelay(5);
        sequence.AppendInterval(15);
        sequence.Append(tut2.DOFade(0, 3).OnComplete(SpawnNightmare));
        sequence.AppendInterval(15);
        sequence.Append(tut3.DOFade(0, 3).OnComplete(SpawnDemon));
    }

    private void SpawnStar()
    {
        StartCoroutine(SpawnObjects(star, tut2.gameObject));
    }

    private void SpawnDemon()
    {
        StartCoroutine(SpawnObjects(demon, null));
    }

    private void SpawnNightmare()
    {
        StartCoroutine(SpawnObjects(nightmare, tut3.gameObject));
    }

    IEnumerator SpawnObjects(GameObject prefab, GameObject nextTut)
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 spawn = spawnLocation.position;
            GameObject obj = Instantiate(prefab, spawn, Quaternion.identity);
            obj.transform.DOMoveX(-15f, 5f);
            yield return new WaitForSeconds(3);
            Destroy(obj);
        }
        if (nextTut != null)
        {
            nextTut.SetActive(true);
        }
        else tutEndMenu.SetActive(true);
    }

    public void ReloadTutotial()
    {
        DOTween.Clear(true);
        SceneManager.LoadScene("Tutorial");
    }

    public void ReturnHome()
    {
        DOTween.Clear(true);
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        background1.material.mainTextureOffset += Vector2.right * speed1 * Time.deltaTime;
        background2.material.mainTextureOffset += Vector2.right * speed2 * Time.deltaTime;
    }
}
