using System.Collections.Generic;
using EasySave.Json;
using UnityEngine;

public class FileItemManager : MonoBehaviour
{
    [SerializeField] private GameObject _fileItemPrefab;
    [SerializeField] private Transform _fileItemParent;

    private List<FileItem> _fileItemList = new List<FileItem>();

    private void Start()
    {
    }
}
