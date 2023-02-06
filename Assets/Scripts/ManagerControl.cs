using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ManagerControl : MonoBehaviour
{
    public GameObject planetPrefab;
    private GameObject planetParent;
    public static int planetNumber;
    private bool isLastPlanetRight;
    private float spacingTimeBetweenPlanets = 10f;
    private float leftTimeUntilNewPlanet;
    private float curPlanetRotateSpeed;
    private float curPlanetGravityScale;

    public GameObject seedUIPrefab;
    private GameObject seedUIParent;
    private List<Transform> seedUIList;
    private Vector2 seedUIStartPoint = new Vector2(-10f,-5f);
    private float seedUISpacing = 0.8f;

    public RootTreeControl rootTreeControl;
    public GroundControl groundControl;
    public BackgroundControl backgroundControl;
    public PauseButtonControl pauseButtonControl;
    public static bool isPlaying;
    void Start()
    {
        planetParent = new GameObject("PlanetParent");
        planetParent.SetActive(true);
        seedUIParent = new GameObject("SeedUIParent");
        seedUIParent.SetActive(true);
        seedUIList = new List<Transform>();
        RestartGame();
        PauseOrResumeGame();
    }
    void FixedUpdate()
    {
        leftTimeUntilNewPlanet -= Time.deltaTime;
        if (leftTimeUntilNewPlanet <= 0)
        {
            GeneratePlanet();
            leftTimeUntilNewPlanet = spacingTimeBetweenPlanets;
        }
        UpdateSeedUI();
        UpdateDifficulty();
    }
    public void PauseOrResumeGame()
    {
        isPlaying = !isPlaying;
        if (isPlaying)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
        pauseButtonControl.isPausing = !isPlaying;
        pauseButtonControl.UpdateButtonImage();
    }
    public void RestartGame()
    {
        PlanetControl planetControl = planetPrefab.GetComponent<PlanetControl>();
        if(planetControl != null)
        {
            curPlanetRotateSpeed = planetControl.rotateSpeed;
            curPlanetGravityScale = planetControl.gameObject.GetComponent<Rigidbody2D>().gravityScale;
        }
        for (int i = 0; i < planetParent.transform.childCount; i++)
        {
            Destroy(planetParent.transform.GetChild(i).gameObject);
        }
        planetNumber = 0;
        leftTimeUntilNewPlanet = 0;
        isLastPlanetRight = true;
        Vector2 firstPos = new Vector2(5f, 3f);
        GameObject firstPlanet = Instantiate(planetPrefab, firstPos, Quaternion.identity);
        firstPlanet.transform.SetParent(planetParent.transform);
        firstPlanet.transform.localScale = Vector3.one;

        RootTreeControl.seedLeftNumber = 6;
        for (int i = 0; i < seedUIParent.transform.childCount; i++)
        {
            Destroy(seedUIParent.transform.GetChild(i).gameObject);
        }
        seedUIList.Clear();
        for(int i = 0; i < RootTreeControl.seedLeftNumber; i++)
        {
            Vector2 pos = seedUIStartPoint + seedUISpacing * i * Vector2.right;
            GameObject tmpSeedUI = Instantiate(seedUIPrefab, pos, Quaternion.identity);
            tmpSeedUI.transform.SetParent(seedUIParent.transform);
            seedUIList.Add(tmpSeedUI.transform);
        }

        rootTreeControl.Initiate();
        groundControl.Initiate();
        backgroundControl.Initiate();

        if(!isPlaying)
        {
            Time.timeScale = 1f;
            isPlaying = true;
        }
        pauseButtonControl.isPausing = false;
        pauseButtonControl.UpdateButtonImage();
        curPlanetRotateSpeed = 0.1f;
        curPlanetGravityScale = 0.05f;
        spacingTimeBetweenPlanets = 10f;
    }
    public void GeneratePlanet()
    {
        planetNumber++;
        float x;
        float scale = UnityEngine.Random.Range(0.8f, 1.4f);
        if (isLastPlanetRight)
        {
            x = UnityEngine.Random.Range(-9f, -4f);
        }
        else
        {
            x = UnityEngine.Random.Range(4f, 9f);
        }
        Vector2 pos = new Vector2(x, 7.5f);
        GameObject tmpPlanet = Instantiate(planetPrefab, pos, Quaternion.identity);
        tmpPlanet.transform.SetParent(planetParent.transform);
        tmpPlanet.transform.localScale = Vector3.one * scale;
        tmpPlanet.GetComponent<PlanetControl>().rotateSpeed = curPlanetRotateSpeed;
        tmpPlanet.GetComponent<Rigidbody2D>().gravityScale = curPlanetGravityScale;
        isLastPlanetRight = !isLastPlanetRight;
    }
    public void UpdateSeedUI()
    {
        if(seedUIList.Count == RootTreeControl.seedLeftNumber)
        {
            return;
        }
        else if(seedUIList.Count > RootTreeControl.seedLeftNumber)
        {
            for (int i = seedUIList.Count - 1; i >= RootTreeControl.seedLeftNumber; i--)
            {
                Destroy(seedUIList[i].gameObject);
                seedUIList.RemoveAt(i);
            }
        }
        else
        {
            for(int i = seedUIList.Count; i < RootTreeControl.seedLeftNumber; i++)
            {
                Vector2 pos = seedUIStartPoint + seedUISpacing * i * Vector2.right;
                GameObject tmpSeedUI = Instantiate(seedUIPrefab, pos, Quaternion.identity);
                tmpSeedUI.transform.SetParent(seedUIParent.transform);
                seedUIList.Add(tmpSeedUI.transform);
            }
        }
    }
    public void UpdateDifficulty()
    {
        curPlanetRotateSpeed = 0.1f + 0.06f * (GroundControl.saplingNumber / 2);
        curPlanetGravityScale = 0.05f + 0.02f * (GroundControl.saplingNumber / 2);
        spacingTimeBetweenPlanets = 10f / (curPlanetGravityScale / 0.05f);
    }
}
