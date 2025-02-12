using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private List<Task> _tasks = new();

    [Space]

    [SerializeField] private TMP_Text _titleView;

    private static string _taskNumber = "1";

    private void Start()
    {
        foreach (var task in _tasks)
        {
            task.UI.SetActive(task.Number == _taskNumber);

            if (task.Number == _taskNumber)
                _titleView.text = task.Name;
        }
    }

    public void OpenTask(string number)
    {
        _taskNumber = number;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

[Serializable]
public class Task
{
    public string Number;
    public string Name;
    public GameObject UI;
}
