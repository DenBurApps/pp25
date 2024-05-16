using Firebase.Extensions;
using Firebase.RemoteConfig;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.SceneManagement;

public class SaveDataInstaller : MonoBehaviour
{
    [SerializeField] private bool _fromTheBeginning;
    [SerializeField] private ConfigData _allConfigData;
    private bool _showTerms = true;

    private void Start()
    {
        InstallBindings();
        Device.RequestStoreReview();
    }

    private void InstallBindings()
    {
        BindFileNames();
        BindRegistration();
        BindQuizz();
        StartLoading();
    }

    private void StartLoading()
    {
        string HtmlText = GetHtmlFromUri("http://google.com");

        if (HtmlText != "")
        {
            LoadFirebaseConfig();
        }

        else
        {
            LoadScene();
        }
    }

    public void LoadFirebaseConfig()
    {
        CheckRemoteConfigValues();
    }


    public Task CheckRemoteConfigValues()
    {
        Debug.Log("Fetching data...");
        Task fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }

    private void FetchComplete(Task fetchTask)
    {
        if (!fetchTask.IsCompleted)
        {
            Debug.LogError("Retrieval hasn't finished.");
            LoadScene();
            return;
        }

        var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
        var info = remoteConfig.Info;
        if (info.LastFetchStatus != LastFetchStatus.Success)
        {
            Debug.LogError($"{nameof(FetchComplete)} was unsuccessful\n{nameof(info.LastFetchStatus)}: {info.LastFetchStatus}");
            LoadScene();
            return;
        }

        // Fetch successful. Parameter values must be activated to use.
        remoteConfig.ActivateAsync()
          .ContinueWithOnMainThread(
            task => {
                Debug.Log($"Remote data loaded and ready for use. Last fetch time {info.FetchTime}.");

                foreach (var item in remoteConfig.AllValues)
                {
                    switch (item.Key)
                    {
                        case "Url":
                            {
                                _allConfigData.Url = item.Value.StringValue;
                                break;
                            }
                        case "ShowAgree":
                            {
                                _allConfigData.ShowAgree = item.Value.BooleanValue;
                                break;
                            }
                    }
                }

                _showTerms = _allConfigData.ShowAgree;
                Debug.Log(_showTerms + "/" + _allConfigData.ShowAgree);
                var reg = SaveSystem.LoadData<RegistrationSaveData>();
                reg.Link = _allConfigData.Url;
                SaveSystem.SaveData(reg);
                LoadScene();
            });
        
    }

    private void LoadScene()
    {
        if (_showTerms)
        {
            if (PlayerPrefs.HasKey("Onboarding"))
            {
                SceneManager.LoadScene("MainScene");
            }
            else
            {
                SceneManager.LoadScene("Onboarding");
            }
        }
        else
        {
            SceneManager.LoadScene("Privacy");
        }
        
    }

    private void BindRegistration()
    {
        {
            var reg = SaveSystem.LoadData<RegistrationSaveData>();

#if UNITY_EDITOR
            if (_fromTheBeginning)
            {
                reg = null;
            }
#endif

            if (reg == null)
            {
                reg = new RegistrationSaveData("", false);
                SaveSystem.SaveData(reg);
            }

        }
    }

    private void BindQuizz()
    {
        {
            var quizz = SaveSystem.LoadData<QuizzSaveData>();

#if UNITY_EDITOR
            if (_fromTheBeginning)
            {
                quizz = null;
            }
#endif

            if (quizz == null)
            {
                quizz = new QuizzSaveData(new List<float> {0, 0, 0, 0});
                SaveSystem.SaveData(quizz);
            }

        }
    }


    private void BindFileNames()
    {
        FileNamesContainer.Add(typeof(RegistrationSaveData), FileNames.RegData);
        FileNamesContainer.Add(typeof(QuizzSaveData), FileNames.QuizzData);
    }

    public string GetHtmlFromUri(string resource)
    {
        string html = string.Empty;
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(resource);
        try
        {
            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                bool isSuccess = (int)resp.StatusCode < 299 && (int)resp.StatusCode >= 200;
                if (isSuccess)
                {
                    using (StreamReader reader = new StreamReader(resp.GetResponseStream()))
                    {
                        //We are limiting the array to 80 so we don't have
                        //to parse the entire html document feel free to 
                        //adjust (probably stay under 300)
                        char[] cs = new char[80];
                        reader.Read(cs, 0, cs.Length);
                        foreach (char ch in cs)
                        {
                            html += ch;
                        }
                    }
                }
            }
        }
        catch
        {
            return "";
        }
        return html;
    }

}

[Serializable]
public class ConfigData
{
    public string Url;
    public bool ShowAgree;
}