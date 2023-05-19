using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class CommandQueue : MonoBehaviour
{
    public static CommandQueue GetInstance => _sharedInstance;

    private static CommandQueue _sharedInstance;
    private Queue<ICommandParent> _commandsToExecute;
    private Queue<bool> _currentCommandIsTask;
    private bool _isRunningCommand;
    
    private void Awake()
    {
        _sharedInstance = this;
        _commandsToExecute = new Queue<ICommandParent>();
        _isRunningCommand = false;
        _currentCommandIsTask = new Queue<bool>();
    }
    public void AddCommand(ICommandParent commandToExecute, bool isCommandTask)
    {
        _currentCommandIsTask.Enqueue(isCommandTask);
        _commandsToExecute.Enqueue(commandToExecute);
    }

    public void ExecuteCommandImmediately(ICommandParent commandToExecute, bool isCommandTask)
    {
        if (isCommandTask)
        {
            var commandTask = commandToExecute as ICommandTask;
            commandTask.Execute();
        }
        else
        {
            var command = commandToExecute as ICommand;
            command.Execute();
        }
    }
    private void Update()
    {
        RunNextCommand();
    }
    private void RunNextCommand()
    {
        if (_isRunningCommand || _commandsToExecute.Count == 0) return;
        
        _isRunningCommand = true;
            
        var isCommandTask = _currentCommandIsTask.Dequeue();
        
        if (isCommandTask) ExecuteCommandTask();
        else ExecuteCommandRegular();
    }
    public void CurrentCommandFinish() => _isRunningCommand = false;
    private void ExecuteCommandRegular()
    {
        var currentCommand = _commandsToExecute.Dequeue() as ICommand;
        currentCommand.Execute();
    }
    private void ExecuteCommandTask()
    {
        var currentCommand = _commandsToExecute.Dequeue() as ICommandTask;

        StartCoroutine(currentCommand.Execute());
    }
}

public interface ICommand : ICommandParent
{
    void Execute();
    void FinishExecute();
}
public interface ICommandTask : ICommandParent
{
    IEnumerator Execute();
    void FinishExecute();
}
public interface ICommandParent{}