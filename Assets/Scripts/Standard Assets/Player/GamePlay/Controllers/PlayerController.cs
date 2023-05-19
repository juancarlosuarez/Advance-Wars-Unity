using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //public UIController uIController;
    public delegate void Controller();

    private static Controller ControllersMap;
    public static event Controller ControllersGameplayEvent;
    public static event Controller ControllersMenuGameplayEvent;
    public static event Controller ControllersEditorMapEvent;

    public static event Action OpenEditorLevel; 

    public static PlayerController sharedInstance;

    private StickGameplay stickGamePlay;



    public static event Action<int> EXIT;
    [Header("ScriptableObjects")]
    [SerializeField] IntReference globalIDMenus;

    private SOMenuManager uiControllerComponent;

    public static event Action PressButtonCross;
    public static event Action PressButtonCircle;
    public static event Action PressButtonSquare;
    public static event Action PressButtonTriangle;

    public static event Action<Vector2> PressButtonArrows;
    public static event Action StopPressButtonArrows;

    public static event Action<int> PressButtonDownArrow;
    public static event Action<int> PressButtonUpArrow;
    public static event Action PressButtonRightArrow;
    public static event Action PressButtonLeftArrow;
    
    public static event Action PressButtonR1;
    public static event Action PressButtonL1;

    public static event Action PressButtonStart;
    public static event Action PressButtonSelect;

    public static event Action ResetEventsNoMonobehavior;

    public PlayerCtrl playerController;

    private bool active = true;
    [SerializeField] DataPermanentForMenus dataPermanentForMenus;
    private ControllerStates currentController;
    private ControllerStates controllerBeforeStop;
    private bool isControllerStop;
    private enum ControllerStates
    {
        UIController, GamePlayController, StopController, EditMapController, IntroductionController
    }
    private void Awake()
    {
        uiControllerComponent = Resources.Load<SOMenuManager>("Menu/MenuController");
        sharedInstance = this;
        stickGamePlay = GetComponent<StickGameplay>();
        isControllerStop = false;
        playerController = new PlayerCtrl();
        
        currentController = ControllerStates.StopController;
    }

    public void Update()
    {
        if (!Application.isFocused && !isControllerStop)
        {
            print("stopControls");
            controllerBeforeStop = currentController;
            isControllerStop = true;
            StopControls();
        }
        if (Application.isFocused && isControllerStop)
        {
            print("resumeControls");
            isControllerStop = false;
            ResumeControls();
        }

    }

    private void OnApplicationQuit() => ResetEventsNoMonobehavior?.Invoke();
    public void ControllerEditorMap()
    {
        ControllersEditorMapEvent();
    }

    private void XEditorMap()
    {
        
    }

    private void OEditorMap()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            StateGamePlay.GetInstance().ChangeState(GameState.GamePlayActionable);
        }
    }

    private void OnDisable()
    {
        
        ControllersEditorMapEvent -= XEditorMap;
        ControllersEditorMapEvent -= OEditorMap;
    }
    private void OnEnable()
    {
        ControllersEditorMapEvent += XEditorMap;
        ControllersEditorMapEvent += OEditorMap;
    }

    private void ResumeControls()
    {
        switch (controllerBeforeStop)
        {
            case ControllerStates.IntroductionController: ChangeControlToIntroduction();
                break;
            case ControllerStates.StopController: StopControls();
                break;
            case ControllerStates.EditMapController: ChangeControlToEditMap();
                break;
            case ControllerStates.GamePlayController: ChangeControlToGamePlay();
                break;
            case ControllerStates.UIController: ChangeControlToUI();
                break;
        }

        currentController = controllerBeforeStop;
    }
    public void ChangeControlToGamePlay()
    {
        if (currentController == ControllerStates.GamePlayController) return;
        currentController = ControllerStates.GamePlayController;
        playerController.Disable();
        playerController.GamePlay.Enable();
    }

    public void ChangeControlToUI()
    {
        if (currentController == ControllerStates.UIController) return;
        
        currentController = ControllerStates.UIController;
        playerController.Disable();
        playerController.UI.Enable();
    }

    public void ChangeControlToEditMap()
    {
        if (currentController == ControllerStates.EditMapController) return;

        currentController = ControllerStates.EditMapController;
        playerController.Disable();
        playerController.EditingMap.Enable();
    }

    public void ChangeControlToIntroduction()
    {
        if (currentController == ControllerStates.IntroductionController) return;

        currentController = ControllerStates.IntroductionController;
        playerController.Disable();
        playerController.Introduction.Enable();
    }
    public void StopControls()
    {
        if (currentController == ControllerStates.StopController) return;

        currentController = ControllerStates.StopController;
        playerController.Disable();
        playerController.NoControls.Enable();
    }
}
