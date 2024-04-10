using System.IO;
using System.Text;
using UnityEngine;

public static class ExcelImporter
{
    public static readonly string path = Application.dataPath + "/10_Database/Excel/";
    public static string fileName = "Stac_WeaponTable";
    public static string Weapon_Name = "";
    public static string Weapon_Damage = "";
    public static string Weapon_AttackSpeed = "";
    public static string Weapon_Type = "";
    public static string Weapon_Range = "";
    public static string Weapon_Rank = "";
    
    public static void ImportExcel(int id)
    {
        // 파일 내용을 한 줄씩 읽어오기
        string[] lines = File.ReadAllLines(Path.Combine(path, fileName + ".csv"), Encoding.UTF8);

        string[] data = lines[id].Split(',');
        Weapon_Name = data[1];
        Weapon_Damage = data[2];
        Weapon_AttackSpeed = data[3];
        Weapon_Type = data[4];
        Weapon_Range = data[5];
        Weapon_Rank = data[6];
        
        Debug.Log("무기 이름: " + Weapon_Name);
        Debug.Log("무기 데미지: " + Weapon_Damage);
        Debug.Log("무기 공격 속도: " + Weapon_AttackSpeed);
        Debug.Log("무기 타입: " + Weapon_Type);
        Debug.Log("무기 사거리: " + Weapon_Range);
        Debug.Log("무기 랭크: " + Weapon_Rank);
    }
}
