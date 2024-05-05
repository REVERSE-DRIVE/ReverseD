using System.IO;
using EasySave.Json;
using SaveDatas;
using UnityEngine;

public class DBManager
{
    private const string LOCALPATH = "";
    private const string userDataFileName = "UserData";
    
    public static void SaveUserData(UserData userData)
    {
        EasyToJson.ToJson(userData, userDataFileName, true);
    }

    public static UserData LoadUserData()
    {
        UserData data = EasyToJson.FromJson<UserData>(userDataFileName);
        return data;
    }
}
