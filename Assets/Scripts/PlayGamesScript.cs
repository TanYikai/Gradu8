using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PlayGamesScript : MonoBehaviour
{
    
    public static PlayGamesScript Instance { get; private set; }

    const string SAVE_NAME = "Tutorialz";
    bool isSaving;
    bool isCloudDataLoaded = false;
    public Text signInButtonText;
    //public Text authStatus;

    // Use this for initialization
    void Start()
    {
        if (Instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        //Protects SaveManager from Being Destroyed when changing Scene
        else
        {
            GameObject.DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        //setting default value, if the game is played for the first time
        if (!PlayerPrefs.HasKey(SAVE_NAME))
            PlayerPrefs.SetString(SAVE_NAME, string.Empty);
        //tells us if it's the first time that this game has been launched after install - 0 = no, 1 = yes 
        if (!PlayerPrefs.HasKey("IsFirstTime"))
            PlayerPrefs.SetInt("IsFirstTime", 1);

        //LoadLocal(); //we want to load local data first because loading from cloud can take quite a while, if user progresses while using local data, it will all
        //sync in our comparating loop in StringToGameData(string, string)

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames().Build();
        // Enable debugging output (recommended)
        PlayGamesPlatform.DebugLogEnabled = true;

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        SignIn();
    }
/*
    public void NewGame()
    {
        DeleteSavedGame(SAVE_NAME);
        PlayerPrefs.SetString(SAVE_NAME, string.Empty);
        SaveManager.Instance.offlineShown = true;
        SaveManager.Instance.newGame = true;
        StartCoroutine(SaveManager.Instance.toCity());
    }


    public void Load()
    {
        LoadData();
        StartCoroutine(SaveManager.Instance.toCity());
    }
    */
    public void SignIn()
    {
        if (!PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // Sign in with Play Game Services, showing the consent dialog
            // by setting the second parameter to isSilent=false.
            PlayGamesPlatform.Instance.Authenticate(SignInCallback, false);
        }
        else
        {
            // Sign out of play games
            PlayGamesPlatform.Instance.SignOut();

            // Reset UI
            signInButtonText.text = "SIGN IN";
            //authStatus.text = "";
        }
    }
    public void SignInCallback(bool success)
    {
        if (success)
        {
            Debug.Log("(Gradu8) Signed in!");

            // Change sign-in button text
            signInButtonText.text = "SIGN OUT";

            // Show the user's name
            //authStatus.text = "Signed in as: " + Social.localUser.userName;
            //LoadData();

        }
        else
        {
            Debug.Log("(Gradu8) Sign-in failed...");

            // Show failure message
            signInButtonText.text = "SIGN IN";
            //authStatus.text = "Sign-in failed";
        }
    }

    public void ShowLeaderboards()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("Cannot show leaderboard: not authenticated");
        }
    }
    #region Saved Games
    /*
    //making a string out of game data (highscores...)
    string GameDataToString()
    {
        SaveManager.updateSave();
        Debug.Log("(Hangry) GAMEDATA TO STRING : " + JsonUtil.CollectionToJsonString(SaveManager.SAVE, "myKey"));
        return JsonUtil.CollectionToJsonString(SaveManager.SAVE, "myKey");
    }

    //this overload is used when user is connected to the internet
    //parsing string to game data (stored in CloudVariables), also deciding if we should use local or cloud save
    void StringToGameData(string cloudData, string localData)
    {
        Debug.Log("(Hangry) compare" + cloudData + " & " + localData);

        if (cloudData == string.Empty)
        {
            Debug.Log("(Hangry)" + localData + " chosen instead of " + cloudData);
            StringToGameData(localData);
            isCloudDataLoaded = true;
            return;
        }
        int[] cloudArray = JsonUtil.JsonStringToArray(cloudData, "myKey", str => int.Parse(str));

        if (localData == string.Empty)
        {
            Debug.Log("(Hangry)" + cloudData + " chosen instead of " + localData);
            printLoop(cloudArray);
            SaveManager.SAVE = cloudArray;
            SaveManager.loadSave();
            PlayerPrefs.SetString(SAVE_NAME, cloudData);
            isCloudDataLoaded = true;
            return;
        }
        int[] localArray = JsonUtil.JsonStringToArray(localData, "myKey", str => int.Parse(str));

        //if it's the first time that game has been launched after installing it and successfuly logging into Google Play Games
        if (PlayerPrefs.GetInt("IsFirstTime") == 1)
        {
            Debug.Log("(Hangry) isFirstTime");
            //set playerpref to be 0 (false)
            PlayerPrefs.SetInt("IsFirstTime", 0);
            for (int i = 0; i < cloudArray.Length; i++)
                if (cloudArray[i] > localArray[i]) //cloud save is more up to date
                {
                    Debug.Log("(Hangry) cloudData" + "'s " + cloudArray[i] + " >" + "localData's " + localArray[i]);
                    //set local save to be equal to the cloud save
                    PlayerPrefs.SetString(SAVE_NAME, cloudData);
                }
        }
        //if it's not the first time, start comparing
        else
        {
            for (int i = 0; i < cloudArray.Length; i++)
                //comparing integers, if one int has higher score in it than the other, we update it
                if (localArray[i] > cloudArray[i])
                {
                    Debug.Log("(Hangry) localData" + "'s " + localArray[i] + " >" + "cloudData's " + cloudArray[i]);
                    //update the cloud save, first set CloudVariables to be equal to localSave
                    SaveManager.SAVE = localArray;
                    SaveManager.loadSave();
                    isCloudDataLoaded = true;
                    //saving the updated CloudVariables to the cloud
                    SaveData();
                    return;
                }
        }
        //if the code above doesn't trigger return and the code below executes,
        //cloud save and local save are identical, so we can load either one
        printLoop(cloudArray);
        Debug.Log("(Hangry) chosen");
        SaveManager.SAVE = cloudArray;
        SaveManager.loadSave();
        isCloudDataLoaded = true;
    }

    //this overload is used when there's no internet connection - loading only local data
    void StringToGameData(string localData)
    {
        if (localData != string.Empty)
        {
            SaveManager.SAVE = JsonUtil.JsonStringToArray(localData, "myKey",
                                                                        str => int.Parse(str));
            SaveManager.loadSave();
        }

        Debug.Log("(Hangry) StringTOGameDATA LOCAL");
    }

    //used for loading data from the cloud or locally
    public void LoadData()
    {
        Debug.Log("(Hangry)LoadData()! ");
        //basically if we're connected to the internet, do everything on the cloud
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            isSaving = false;

            PlayGamesPlatform.Instance.SavedGame.OpenWithManualConflictResolution(SAVE_NAME,
                DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
            //SaveManager.loadSave();
            Debug.Log("(Hangry) Data Loaded from Cloud");

        }
        //this will basically only run in Unity Editor, as on device,
        //localUser will be authenticated even if he's not connected to the internet (if the player is using GPG)
        else
        {
            LoadLocal();
            SaveManager.loadSave();
            Debug.Log("(Hangry) Impossbl!!! , Data Loaded from local");


        }
    }

    private void LoadLocal()
    {
        StringToGameData(PlayerPrefs.GetString(SAVE_NAME));
        Debug.Log("(Hangry) Data Loaded from local");

    }

    //used for saving data to the cloud or locally
    public void SaveData()
    {
        //if we're still running on local data (cloud data has not been loaded yet), we also want to save only locally
        if (!isCloudDataLoaded)
        {
            SaveLocal();
            Debug.Log("(Hangry) Data Saved to local");
            return;
        }
        //same as in LoadData
        if (Social.localUser.authenticated)
        {
            isSaving = true;
            PlayGamesPlatform.Instance.SavedGame.OpenWithManualConflictResolution(SAVE_NAME,
                DataSource.ReadCacheOrNetwork, true, ResolveConflict, OnSavedGameOpened);
            Debug.Log("(Hangry) Data Saved to cloud!");

        }
        else
        {

            SaveLocal();
            Debug.Log("(Hangry) Data Saved to local!");

        }
    }

    private void SaveLocal()
    {
        PlayerPrefs.SetString(SAVE_NAME, GameDataToString());
        Debug.Log("(Hangry)SaveLocal()!");

    }

    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData,
        ISavedGameMetadata unmerged, byte[] unmergedData)
    {
        if (originalData == null)
        {
            resolver.ChooseMetadata(unmerged);
            Debug.Log("(Hangry)unmerged data chosen due to null!");

        }
        else if (unmergedData == null)
        {
            resolver.ChooseMetadata(original);
            Debug.Log("(Hangry)original data chosen due to null!");

        }
        else
        {
            //decoding byte data into string
            string originalStr = Encoding.UTF8.GetString(originalData);
            string unmergedStr = Encoding.UTF8.GetString(unmergedData);

            //parsing
            int[] originalArray = JsonUtil.JsonStringToArray(originalStr, "myKey", str => int.Parse(str));
            int[] unmergedArray = JsonUtil.JsonStringToArray(unmergedStr, "myKey", str => int.Parse(str));

            for (int i = 0; i < originalArray.Length; i++)
            {
                //if original score is greater than unmerged
                if (originalArray[i] > unmergedArray[i])
                {
                    Debug.Log("(Hangry)original data chosen, better!");
                    resolver.ChooseMetadata(original);
                    return;
                }
                //else (unmerged score is greater than original)
                else if (unmergedArray[i] > originalArray[i])
                {
                    Debug.Log("(Hangry)unmerged data chosen, vbettter!");

                    resolver.ChooseMetadata(unmerged);
                    return;
                }
            }
            //if return doesn't get called, original and unmerged are identical
            //we can keep either one
            Debug.Log("(Hangry)original data chosen, same!");

            resolver.ChooseMetadata(original);
        }
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        Debug.Log("(Hangry) OnSavedGameOpened!");

        //if we are connected to the internet
        if (status == SavedGameRequestStatus.Success)
        {
            //if we're LOADING game data
            if (!isSaving)
            {
                Debug.Log("(Hangry) OnSavedGameOpened loading cloud game!");
                LoadGame(game);
                Debug.Log("(Hangry) OnSavedGameOpened loaded cloud game!");

            }
            //if we're SAVING game data
            else
            {
                SaveGame(game);
                Debug.Log("(Hangry) OnSavedGameOpened saved cloud game!");

            }
        }
        //if we couldn't successfully connect to the cloud, runs while on device,
        //the same code that is in else statements in LoadData() and SaveData()
        else
        {
            if (!isSaving)
            {
                LoadLocal();
                Debug.Log("(Hangry) OnSavedGameOpened loaded local game!");
            }
            else
            {
                SaveLocal();
                Debug.Log("(Hangry) OnSavedGameOpened saved local game!");
            }
        }
    }

    private void LoadGame(ISavedGameMetadata game)
    {
        Debug.Log("(Hangry) LoadGame!");
        PlayGamesPlatform.Instance.SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
        Debug.Log("(Hangry) LoadedGame!");

    }
    //Writing Data
    private void SaveGame(ISavedGameMetadata game)
    {
        Debug.Log("(Hangry) SaveGame!");
        string stringToSave = GameDataToString();
        //saving also locally (can also call SaveLocal() instead)
        PlayerPrefs.SetString(SAVE_NAME, stringToSave);
        Debug.Log("(Hangry) SaveGameLocal! >" + stringToSave);


        //encoding to byte array
        byte[] dataToSave = Encoding.UTF8.GetBytes(stringToSave);
        //updating metadata with new description
        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
        Debug.Log("(Hangry) uploading! ");
        Debug.Log("(Hangry)" + System.BitConverter.ToString(dataToSave));
        printLoop(dataToSave);
        //uploading data to the cloud
        PlayGamesPlatform.Instance.SavedGame.CommitUpdate(game, update, dataToSave,
            OnSavedGameDataWritten);
        Debug.Log("(Hangry) uploaded!");

    }

    //callback for ReadBinaryData
    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    {
        //if reading of the data was successful
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("(Hangry) save read success! read : ");
            Debug.Log("(Hangry)" + System.BitConverter.ToString(savedData));
            printLoop(savedData);
            string cloudDataString;
            //if we've never played the game before, savedData will have length of 0
            if (savedData.Length == 0)
                //in such case, we want to assign default value to our string
                cloudDataString = string.Empty;
            //otherwise take the byte[] of data and encode it to string
            else
                cloudDataString = Encoding.UTF8.GetString(savedData);

            //getting local data (if we've never played before on this device, localData is already
            //string.Empty, so there's no need for checking as with cloudDataString)
            string localDataString = PlayerPrefs.GetString(SAVE_NAME);
            Debug.Log("(Hangry) local read !");

            //this method will compare cloud and local data
            StringToGameData(cloudDataString, localDataString);
        }
    }

    //callback for CommitUpdate
    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
            Debug.Log("(Hangry) Data writting success !");
        else
            Debug.Log("(Hangry) Data writting fail =( !");
        //LoadData();
    }
    */
    #endregion /Saved Games
    /*
void DeleteSavedGame(string filename)
{
    ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
    savedGameClient.OpenWithManualConflictResolution(
        filename,
        DataSource.ReadCacheOrNetwork, true,
        ResolveConflict,
        OnDeleteSavedGame);
}

public void OnDeleteSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game)
{
    ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
    if (status == SavedGameRequestStatus.Success)
    {
        Debug.Log("(Hangry) Deleting Game");
        // delete the game.
        isCloudDataLoaded = true;
        savedGameClient.Delete(game);
    }
    else
    {
        Debug.Log("(Hangry) Cannot open Game to  delete");
        // handle error
    }
}

void printLoop<T>(T arr) where T : IList
{
    for (int i = 0; i < arr.Count; i++)
        Debug.Log("(Hangry)" + i + ":" + arr[i].ToString());
}
*/
}
