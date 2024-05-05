using System.IO;
using EasySave.Json;
using SaveDatas;
using UnityEngine;

public class DBManager
{
    private const string LOCALPATH = "";
    private const string UserDataFileName = "UserData";
    private const string LobbyDataFileName = "LobbyData";
    private const string InGameDataFileName = "LobbyData";
    
    public static void SaveUserData(UserData userData)
    {
        EasyToJson.ToJson(userData, UserDataFileName, true);
    }

    public static UserData LoadUserData()
    {
        UserData data = EasyToJson.FromJson<UserData>(UserDataFileName);
        if (data == null)
        {
            return new UserData();
        }
        return data;
    }

    public static void SaveLobbyData(LobbyData lobbyData)
    {
        EasyToJson.ToJson(lobbyData, LobbyDataFileName, true);
    }

    public static LobbyData LoadLobbyData()
    {
        LobbyData data = EasyToJson.FromJson<LobbyData>(LobbyDataFileName);
        if (data == null)
        {
            return new LobbyData();
        }
        return data;
    }

    public static void SaveInGameData(InGameData inGameData)
    {
        EasyToJson.ToJson(inGameData, InGameDataFileName);
    }

    public static InGameData LoadInGameData()
    {
        InGameData data = EasyToJson.FromJson<InGameData>(InGameDataFileName);
        if (data == null)
        {
            return new InGameData();
        }
        return data;
    }
}
