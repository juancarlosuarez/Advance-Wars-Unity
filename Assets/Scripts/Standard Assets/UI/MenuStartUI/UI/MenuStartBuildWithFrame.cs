using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class MenuStartBuildWithFrame
{

    private const float OnePixelDistance = 0.02f;

    private GameObject _finalBuild;
    private GameObject _arrowHighLight;
    private List<GameObject> _listElementsInsideFrameWork;
    
    private Vector2 _sizeReferenceElement;
    private Vector2 _sizeFirstImage;
    private Vector2 _sizeSecondImage;
    private Vector2 _positionFirstImage;
    private Vector2 _positionSecondImage;

    private List<Image> _frameworkImages;
    private List<Vector2> _sizesFrameworkImages;
    private List<Vector2> _positionsFrameworkImages;
    private List<Vector2> _positionArrowHighLight;

    private NameElementWithFramesGeneric _newMenu;
    public List<GameObject> GetListElements() => _listElementsInsideFrameWork;
    public GameObject GetArrowHighLight() => _arrowHighLight;
    public List<Vector2> GetArrowPositions() => _positionArrowHighLight;
    private int countElements;
    public List<Image> GetFrameworkPieces() => _frameworkImages;
    private ManagerBuilderElementsMenus _managerBuilders; 
    private bool _isThisMenuDinamic;
    //Well, this shit need to work correctly, the scriptable object OptionStartData and another scripts that manage how to build the elements, this scripts just
    //build the framework and put the elements inside of this framework correctly. But dont manage the logic of build the elements or what its inside of them. 
    //This system is modular, it dont care about what size is the element or what is inside. This system is open from another script, this should give
    //one object for each option possible to start menu and just active and disable for keep good performance. This is just the UI, not the controller
    //for the controller is needed another script. 
    
    //deebs cambiar el scrpit de admin menu, es donde tienes actualmente la referencia de mierda
    //public GameObject MakeBuild(OptionStartsData newData, NameMenuStart newMenu)
    public MenuStartBuildWithFrame()
    {
        _managerBuilders = new ManagerBuilderElementsMenus();
    }
    public GameObject OverWriteMenu(NameElementWithFramesGeneric menuOverwrite, DataUIMenuStart dataMenuOverwrite)
    {
        _newMenu = menuOverwrite;
        _frameworkImages = dataMenuOverwrite.frameworkMenu;
        _finalBuild = dataMenuOverwrite.referenceToMenu;
        _arrowHighLight = dataMenuOverwrite.arrowHighLight;
        _isThisMenuDinamic = true;
        
        GetElementsInsideFrameWork();
        GetValuesForFrameWorkPieces();
        ResizeComponentsFrameWork();
        RelocateElements();
        return FinalBuild();
    }
    public GameObject MakeBuild(NameElementWithFramesGeneric newMenu, bool isThisMenuDinamic)
    {
        _newMenu = newMenu;
        _isThisMenuDinamic = isThisMenuDinamic;
            
        GetElementsInsideFrameWork();
        MakeFrameWorkPieces();
        GetValuesForFrameWorkPieces();
        ResizeComponentsFrameWork();
        RelocateElements();
        return FinalBuild();
    }

    private GameObject FinalBuild()
    {
        _finalBuild.SetActive(false);
        return _finalBuild;
    } 
    #region ElementsInsideFrameWork
    private void GetElementsInsideFrameWork()
    {
        IBuilderMenuStart builderElementsStart = _managerBuilders.GetBuilderGenerics(_newMenu);
        
        _listElementsInsideFrameWork = builderElementsStart.Build(_isThisMenuDinamic);
        _sizeReferenceElement = builderElementsStart.SizeReference;
        _arrowHighLight = builderElementsStart.GetArrowHighLight();
        countElements = builderElementsStart.GetCount;
    }
    private void RelocateElements()
    {
        GameObject firstElement = _listElementsInsideFrameWork.First();
        _positionArrowHighLight = new List<Vector2>();
        for (int i = 0; i < countElements; i++)
        {
            Vector2 positionElement;
            
            float x = _positionFirstImage.x;
            //float y = _positionFirstImage.y - (_sizeFirstImage.y / 2 - _sizeReferenceElement.y / 2) + (OnePixelDistance * 3);
             float y = _positionFirstImage.y + (_sizeFirstImage.y / 2 - _sizeReferenceElement.y / 2) - (OnePixelDistance * 3);
            
             //This condiction need to be put it, for the barrack menu.
            if (i > _listElementsInsideFrameWork.Count - 1) break;
            
            if (firstElement == _listElementsInsideFrameWork[i]) positionElement = new Vector2(x, y);
            //else positionElement =  new Vector2(x, y + (_sizeReferenceElement.y * i));
            else positionElement =  new Vector2(x, y  - (_sizeReferenceElement.y * i));
            
            _listElementsInsideFrameWork[i].transform.position = positionElement;

            Vector2 arrowPosition = new Vector2(positionElement.x - _sizeFirstImage.x 
                / 2 - _arrowHighLight.GetComponent<Image>().rectTransform.sizeDelta.x / 2 - 0.06f - _positionFirstImage.x, positionElement.y - _positionFirstImage.y);
            _positionArrowHighLight.Add(arrowPosition);

        }
    }
    #endregion
    #region MakeFrameWork
    private void MakeFrameWorkPieces() => _frameworkImages = AllNewImages(_listElementsInsideFrameWork);
    private void GetValuesForFrameWorkPieces()
    {
        _sizesFrameworkImages = AllSizes();
        _positionsFrameworkImages = AllPositions();
    }
    private void ResizeComponentsFrameWork()
    {
        int count = 0;
        foreach (var componentFrameWork in _frameworkImages)
        {
            componentFrameWork.rectTransform.sizeDelta = _sizesFrameworkImages[count];
            componentFrameWork.transform.position = _positionsFrameworkImages[count];
            count++;
        }
    }
    private List<Image> AllNewImages(List<GameObject> elementsMenu)
    {
        GameObject canvas = GameObject.Find("Canvas");
        _finalBuild = new GameObject(_newMenu.ToString());
        GameObject componentsMenuMain = new GameObject("Components");
        
        componentsMenuMain.SetParent(_finalBuild);
        _finalBuild.SetParent(canvas);
        
        foreach (var c in elementsMenu) c.gameObject.SetParent(_finalBuild);

        var frameworkImages = CreateAllImageOfMenu();

        foreach (var c in frameworkImages) c.gameObject.SetParent(componentsMenuMain);
        frameworkImages[1].transform.SetSiblingIndex(0);

        return frameworkImages;
    }

    private List<Image> CreateAllImageOfMenu()
    {
        var createImage = new CreateImageFromZero(Vector2.zero, _finalBuild);

        List<Image> componentsMenu = new List<Image>();
        componentsMenu.Add(createImage.CreateImage("menuRed1", Color.red));
        componentsMenu.Add(createImage.CreateImage("menuWhite2", Color.white));
        componentsMenu.Add(createImage.CreateImage("bordVerticalRight3", Color.black));
        componentsMenu.Add(createImage.CreateImage("bordVerticalRight4", Color.black));
        componentsMenu.Add(createImage.CreateImage("bordVerticalLeft5", Color.black));
        componentsMenu.Add(createImage.CreateImage("bordHorizontalDown6", Color.black));
        componentsMenu.Add(createImage.CreateImage("bordHorizontalDown7", Color.black));
        componentsMenu.Add(createImage.CreateImage("bordHorizontalUp8", Color.black));
        return componentsMenu;
    }
    #endregion
    #region CalculatePositionForFrameWork
    private List<Vector2> AllPositions()
    {
        List<Vector2> allPosition = new List<Vector2>();

        _positionFirstImage = PositionFirstImage();
        _positionSecondImage = PositionSecondImage();

        allPosition.Add(_positionFirstImage);
        allPosition.Add(_positionSecondImage);
        allPosition.Add(PositionThirdImage());
        allPosition.Add(PositionFourthImage());
        allPosition.Add(PositionFifthImage());
        allPosition.Add(PositionSixthImage());
        allPosition.Add(PositionSeventhImage());
        allPosition.Add(PositionEighthImage());
        return allPosition;
    }
    private Vector2 PositionFirstImage() => _frameworkImages[0].transform.position;
    private Vector2 PositionSecondImage() => _frameworkImages[0].transform.position;
    private Vector2 PositionThirdImage()
    {
        float offset = _sizeSecondImage.x / 2 + (OnePixelDistance / 2);
        return new Vector2(_positionSecondImage.x + offset, _positionSecondImage.y);
    }
    private Vector2 PositionFourthImage()
    {
        float offset = _sizeSecondImage.x / 2 + (OnePixelDistance + (OnePixelDistance / 2));
        return new Vector2(_positionSecondImage.x + offset, _positionSecondImage.y);
    }
    private Vector2 PositionFifthImage()
    {
        float offset = _sizeSecondImage.x / 2 + (OnePixelDistance / 2);
        return new Vector2(_positionSecondImage.x - offset, _positionSecondImage.y);
    }
    private Vector2 PositionSixthImage()
    {
        float offset = _sizeSecondImage.y / 2 + (OnePixelDistance / 2);
        return new Vector2(_positionSecondImage.x, _positionSecondImage.y - offset );
    }
    private Vector2 PositionSeventhImage()
    {
        float offset = _sizeSecondImage.y / 2 + (OnePixelDistance + (OnePixelDistance / 2));
        return new Vector2(_positionSecondImage.x, _positionSecondImage.y - offset);
    }
    private Vector2 PositionEighthImage()
    {
        float offset = _sizeSecondImage.y / 2 + (OnePixelDistance / 2);
        return new Vector2(_positionSecondImage.x, _positionSecondImage.y + offset);
    }
    #endregion
    #region CalculateSizeForFrameWork
    private List<Vector2> AllSizes() 
    {
        List<Vector2> allSizes = new List<Vector2>();

        _sizeFirstImage = FirstImageSize(countElements);
        _sizeSecondImage = SecondImageSize();

        allSizes.Add(_sizeFirstImage);
        allSizes.Add(_sizeSecondImage);
        allSizes.Add(ThirdImageSize());
        allSizes.Add(FourthImageSIze());
        allSizes.Add(FifthImageSize());
        allSizes.Add(SixthImageSize());
        allSizes.Add(SeventhImageSize());
        allSizes.Add(EighthImageSize());
        return allSizes;
    }
    private Vector2 FirstImageSize(int numberOfElements)
    {
        float height = _sizeReferenceElement.y * numberOfElements + (OnePixelDistance * 6);
        float width = _sizeReferenceElement.x + (OnePixelDistance * 2);
        
        return new Vector2(width, height);
    }
    private Vector2 SecondImageSize() => new Vector2(_sizeFirstImage.x + (OnePixelDistance * 2), _sizeFirstImage.y + (OnePixelDistance * 2));
    private Vector2 ThirdImageSize() => new Vector2(OnePixelDistance, _sizeSecondImage.y);
    private Vector2 FourthImageSIze() => new Vector2(OnePixelDistance, _sizeSecondImage.y - (OnePixelDistance * 2));
    private Vector2 FifthImageSize() => new Vector2(OnePixelDistance, _sizeSecondImage.y);
    private Vector2 SixthImageSize() => new Vector2(_sizeSecondImage.x, OnePixelDistance);
    private Vector2 SeventhImageSize() => new Vector2(_sizeSecondImage.x - (OnePixelDistance * 2), OnePixelDistance);
    private Vector2 EighthImageSize() => new Vector2(_sizeSecondImage.x, OnePixelDistance);
    #endregion

} 
