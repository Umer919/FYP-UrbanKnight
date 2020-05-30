using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public static GameHandler _Instance;
    public _HealthManager clsHealthManager;
    public _UiHandler clsUiHandler;
    public _dataType clsDataTypes;
    public Transform[] SpawnPoints;
    public Material[] EnvironmentSkybox;
    public GameObject first;
    public GameObject second;
    public GameObject third;
    public GameObject fourth;
    public GameObject fifth;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        _Instance = this;
        FncCheckLevel();
        clsHealthManager._HealthBar.value = clsHealthManager.Health / 100.0f;
        
    }

    private void Start()
    {
        int EnvironmentNo = PlayerPrefs.GetInt(Prefs.LevelID);

        RenderSettings.skybox = EnvironmentSkybox[EnvironmentNo];
        //PlayerPrefs.DeleteAll();
        FncRandom();
        clsDataTypes.isMonsterComing =  false;
    }

    public void FncRandom()
    {
        clsUiHandler.txt_TotalEnemy.text = "Total : " + clsDataTypes._CurrentLevelZombie;
        clsUiHandler.txt_RemaningEnemy.text = "Remaning : " + clsDataTypes.RenamingEnemy;
        
        if (clsDataTypes.ZombieSpawn < clsDataTypes._CurrentLevelZombie)
        {
            clsDataTypes.Random = Random.Range(0, SpawnPoints.Length);
            var _ZoombieToSpawn = Random.Range(0, clsDataTypes.Zombie.Length);
            float dis = Vector3.Distance(SpawnPoints[clsDataTypes.Random].position, clsDataTypes._Player.position);
            if (dis > 10)
            {
                if(PlayerPrefs.GetInt(Prefs.LevelID) ==4)
                {
                      if( clsDataTypes.isMonsterComing ==  true)
                    {
                        Instantiate(clsDataTypes._Monster, SpawnPoints[clsDataTypes.Random].position, Quaternion.identity);
                         clsDataTypes.ZombieSpawn++;
                                                
                    }

                    if(clsDataTypes.isMonsterComing == false)
                    {
                         Instantiate(clsDataTypes.Zombie[_ZoombieToSpawn], SpawnPoints[clsDataTypes.Random].position, Quaternion.identity);
                         clsDataTypes.ZombieSpawn++;
                        
                         if(clsDataTypes.ZombieSpawn == 3)
                         {
                              clsDataTypes.isMonsterComing = true;
                         }
                    }

                  

                    // clsDataTypes.ZombieSpawn++;
                }
                else
                {
                    Instantiate(clsDataTypes.Zombie[_ZoombieToSpawn], SpawnPoints[clsDataTypes.Random].position, Quaternion.identity);
                    clsDataTypes.ZombieSpawn++;
                }

            }
            else
            {
                clsDataTypes.Random = UnityEngine.Random.Range(0, SpawnPoints.Length);
            }
        }
        if (clsDataTypes._CurrentLevelZombie == clsDataTypes.killZombie)
        {
            Invoke("FncComplete", 3f);
        }
    }

    public void FncHealthHandler(float damage)
    {
        clsHealthManager.Health -= damage;
        clsHealthManager._HealthBar.value = clsHealthManager.Health / 100.0f;
        clsDataTypes._Player.GetComponent<Animator>().SetTrigger("Damage");
        if (clsHealthManager.Health <= 0)
        {
            clsUiHandler.pnl_Failed.SetActive(true);
        }
    }

    public void FncButtonHandler(Button btn)
    {
        switch (btn.name)
        {
            case "Btn_paused":
                Time.timeScale = 0.0f;
                clsUiHandler.pnl_paused.SetActive(true);
                break;
            case "Btn_MainMenu":
                Time.timeScale = 1.0f;
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
                break;
            case "Btn_Resume":
                Time.timeScale = 1.0f;
                clsUiHandler.pnl_paused.SetActive(false);
                break;
            case "Btn_Restart":
                Time.timeScale = 1.0f;
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
                break;
            case "Btn_Next":
                Time.timeScale = 1.0f;
                if (PlayerPrefs.GetInt(Prefs.LevelID) < 5)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
                }
               else
                {
                    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
                }
                break;
        }
    }

    void FncCheckLevel()
    {
        clsDataTypes.CurrentLevel = PlayerPrefs.GetInt(Prefs.LevelID);
        if (clsDataTypes.CurrentLevel == 0)
        {
            first.SetActive(true);
            second.SetActive(false);
            third.SetActive(false);
            fourth.SetActive(false);
            fifth.SetActive(false);
            clsDataTypes._CurrentLevelZombie = 3;
            clsDataTypes.RenamingEnemy = clsDataTypes._CurrentLevelZombie;
        }
        if (clsDataTypes.CurrentLevel == 1)
        {
            first.SetActive(false);
            second.SetActive(true);
            third.SetActive(false);
            fourth.SetActive(false);
            fifth.SetActive(false);
            clsDataTypes._CurrentLevelZombie = 4;
            clsDataTypes.RenamingEnemy = clsDataTypes._CurrentLevelZombie;
        }
        if (clsDataTypes.CurrentLevel == 2)
        {
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(true);
            fourth.SetActive(false);
            fifth.SetActive(false);
            clsDataTypes._CurrentLevelZombie = 5;
            clsDataTypes.RenamingEnemy = clsDataTypes._CurrentLevelZombie;
        }
        if (clsDataTypes.CurrentLevel == 3)
        {
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            fourth.SetActive(true);
            fifth.SetActive(false);
            clsDataTypes._CurrentLevelZombie = 6;
            clsDataTypes.RenamingEnemy = clsDataTypes._CurrentLevelZombie;
        }
        if (clsDataTypes.CurrentLevel == 4)
        {
            first.SetActive(false);
            second.SetActive(false);
            third.SetActive(false);
            fourth.SetActive(false);
            fifth.SetActive(true);
            clsDataTypes._CurrentLevelZombie =4;
            clsDataTypes.RenamingEnemy = clsDataTypes._CurrentLevelZombie;
        }
        int level = clsDataTypes.CurrentLevel + 1;
        clsUiHandler.txt_LevelNumber.text = "LEVEL : " + level;
    }

    void FncComplete()
    {
        clsUiHandler.pnl_Complete.SetActive(true);
        if (PlayerPrefs.GetInt(Prefs.LevelID) < 4)
        {
            PlayerPrefs.SetInt(Prefs.LevelID, PlayerPrefs.GetInt(Prefs.LevelID) + 1);
            FncCheckLevel();
        }

    }
}
[System.Serializable]
public class _HealthManager
{
    public Slider _HealthBar;
    public float Health;

}
[System.Serializable]
public class _UiHandler
{
    public GameObject pnl_Failed, pnl_Complete, pnl_paused;
    public Text txt_LevelNumber,txt_TotalEnemy,txt_RemaningEnemy;

}
[System.Serializable]
public class _dataType
{
    public int CurrentLevel, _CurrentLevelZombie, ZombieSpawn, Random, killZombie,RenamingEnemy;
    public GameObject[] Zombie;
    public bool isMonsterComing;
    public GameObject _Monster;
    public Transform _Player;

}
public class Prefs
{
    public const string LevelID = "LevelID";
}
