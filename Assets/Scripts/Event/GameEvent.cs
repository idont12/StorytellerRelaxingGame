using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Storyteller/GameEvent", fileName = "GameEvent")]
public class GameEvent : ScriptableObject
{
    private List<Action> actions;

    private void OnEnable()
    {
        actions = new List<Action>();
    }

    public void Raise()
    {
        for (int i = actions.Count - 1; i >= 0; i--)
        {
            actions[i].Invoke();
        }
    }

    public void RegisterListener(Action action)
    {
        if (actions.Contains(action))
        {
            Debug.LogWarning($"listener {action} is already registered");
            return;
        }
        actions.Add(action);
    }
    
    public void UnregisterListener(Action action)
    {
        if (actions.Contains(action))
        {
            actions.Remove(action);
        }
        else
        {
            Debug.LogWarning($"listener {action} is not registered and can't be removed");
        }
    }
}