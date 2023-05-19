using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class BuilderFrameGeneric : MonoBehaviour
{
    
    private const float OnePixelDistance = 0.02f;
    
    
    //References to the elements
    private Vector2 _sizeReferenceElement;
    private int _countElements;
    private List<Image> _allElementsRawImages;
    //
    private Vector2 _sizeFirstImage;
    private Vector2 _sizeSecondImage;
    private Vector2 _positionFirstImage;
    private Vector2 _positionSecondImage;
    
    private List<Vector2> _sizesFrameworkImages;
    private List<Vector2> _positionsFrameworkImages;
    private List<Image> _frameworkImages;
    private List<Vector2> _positionArrowHighLight;

    private GameObject _finalBuild;
    
    //Test
    [SerializeField] int countElements;
    [SerializeField] private Vector2 sizeElements;
    [SerializeField] private bool reloadInitialData = false;

    private void OnValidate() {
        if (reloadInitialData) {
            Test();
            reloadInitialData = false;
        }
    }
    public void Test()
    {
        var parent = new GameObject("parentTest");
        var createImage = new CreateImageFromZero(Vector2.zero, parent);
        var allImages = new List<Image>();
        for (int i = 0; i < countElements; i++)
        {
            var image = createImage.CreateImage("Imagen" + i, Color.cyan);
            allImages.Add(image);
            image.rectTransform.sizeDelta = sizeElements;
        }

        Build(allImages);
        Destroy(parent.gameObject);
    }
    
    
    private GameObject Build(List<Image> allElementsRawImage)
    {
        _allElementsRawImages = allElementsRawImage;
        
        AdjustReferences();
        MakeFrameWorkPieces();
        GetValuesForFrameWorkPieces();
        ResizeComponentsFrameWork();
        RelocateElements();
        return FinalBuild();
    }

    private void AdjustReferences()
    {
        _sizeReferenceElement = _allElementsRawImages[0].rectTransform.sizeDelta;
        _countElements = _allElementsRawImages.Count;
        
    }
    #region Pieces Manager
    private void MakeFrameWorkPieces() => _frameworkImages = AllNewImages();
    private List<Image> AllNewImages()
    {
        GameObject canvas = GameObject.Find("Canvas");
        _finalBuild = new GameObject("Prueba me agoeucraogueao");
        GameObject componentsMenuMain = new GameObject("Components");
        
        componentsMenuMain.SetParent(_finalBuild);
        _finalBuild.SetParent(canvas);
        
        foreach (var c in _allElementsRawImages) c.gameObject.SetParent(_finalBuild);

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
    private void GetValuesForFrameWorkPieces()
    {
        _sizesFrameworkImages = GetAllSizes();
        _positionsFrameworkImages = GetAllPiecesPosition();
    }
    
    #region Sizes Manager
    private List<Vector2> GetAllSizes()
    {
        List<Vector2> allSizes = new List<Vector2>();
        
        _sizeFirstImage = FirstImageSize(_countElements);
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

    #region Positions Manager
    private List<Vector2> GetAllPiecesPosition()
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
    private Vector2 PositionSixthImage() //Este esta dando problemas
    {
        float offset = _sizeSecondImage.y / 2 + (OnePixelDistance / 2);
        return new Vector2(_positionSecondImage.x, _sizeSecondImage.y - offset);
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
    private void RelocateElements()
    {
        GameObject firstElement = _allElementsRawImages[0].gameObject;
        _positionArrowHighLight = new List<Vector2>();
        for (int i = 0; i < _countElements; i++)
        {
            Vector2 positionElement;
            
            float x = _positionFirstImage.x;
            //float y = _positionFirstImage.y - (_sizeFirstImage.y / 2 - _sizeReferenceElement.y / 2) + (OnePixelDistance * 3);
            float y = _positionFirstImage.y + (_sizeFirstImage.y / 2 - _sizeReferenceElement.y / 2) - (OnePixelDistance * 3);
            float sizeArrowX = .2f; 
            
            //This condiction need to be put it, for the barrack menu.
            if (i > _allElementsRawImages.Count - 1) break;
            
            if (firstElement == _allElementsRawImages[i]) positionElement = new Vector2(x, y);
            //else positionElement =  new Vector2(x, y + (_sizeReferenceElement.y * i));
            else positionElement =  new Vector2(x, y  - (_sizeReferenceElement.y * i));
            
            _allElementsRawImages[i].transform.position = positionElement;

            Vector2 arrowPosition = new Vector2(positionElement.x - _sizeFirstImage.x 
                / 2 - sizeArrowX / 2 - 0.06f - _positionFirstImage.x, positionElement.y - _positionFirstImage.y);
            _positionArrowHighLight.Add(arrowPosition);

        }
    }
    private GameObject FinalBuild()
    {
        _finalBuild.SetActive(false);
        return _finalBuild;
    } 
}
