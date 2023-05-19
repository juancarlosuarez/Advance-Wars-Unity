using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VanishingImageHorizontally : ICommandTask
{
    
    private RectTransform _dataSizeImage;
    private float _offSet = 5f;
    private bool _isFinishAnimation;

    public VanishingImageHorizontally(RectTransform dataSizeImage)
    {
        _dataSizeImage = dataSizeImage;
    }
    public IEnumerator Execute()
    {
        _isFinishAnimation = false;
        
        while (!_isFinishAnimation)
        {
            MakeAnimation();
            yield return null;
        }
        FinishExecute();
    }

    private void MakeAnimation()
    {
        var scaleImageX = _dataSizeImage.localScale.x - _offSet * Time.deltaTime;
        if (scaleImageX <= 0)
        {
            _isFinishAnimation = true;
            return;
        }
        _dataSizeImage.localScale = new Vector3(scaleImageX, _dataSizeImage.localScale.y);
        
    }
    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
