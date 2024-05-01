using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FileItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _fileNameText;
    
    private DraggableUI _draggableUi;
    private string _fileName;
    
    private void Awake()
    {
        _draggableUi = GetComponent<DraggableUI>();
        _draggableUi.ParentTransform = transform.parent as RectTransform;
    }
    
    public void SetFileName(string fileName)
    {
        _fileName = fileName;
        _fileNameText.text = fileName;
    }
}
