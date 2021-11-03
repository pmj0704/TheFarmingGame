using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private User user = null;
    public User currentUser { get { return user; } }
    private string SAVE_PATH;
    private readonly string SAVE_FILENAME = "/SaveFile.txt";

    [SerializeField]
    private GameObject QuitBt;

    void Awake()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
        }
        LoadFromJson();

        InvokeRepeating("Save", 0f, 60f);
    }
    private void Save()
    {
        SaveToJson();
    }
    private void LoadFromJson()
    {
        string json = "";
        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            user = JsonUtility.FromJson<User>(json);
        }
        else
        {
            SaveToJson();
            LoadFromJson();
        }
    }

    public void Quit()
    {
        SaveToJson();
        Debug.Log("Quit");
        Application.Quit();
    }

    private void SaveToJson()
    {
        SAVE_PATH = Application.dataPath + "/Save";
        if (user == null) return;
        string json = JsonUtility.ToJson(user, true);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }
    private void OnApplicationQuit()
    {
        SaveToJson();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            QuitBt.SetActive(true);
            QuitBt.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(QuitCor());
        }
    }
    private IEnumerator QuitCor()
    {
        yield return new WaitForSeconds(0.3f);
        QuitBt.SetActive(false);
    }
}
