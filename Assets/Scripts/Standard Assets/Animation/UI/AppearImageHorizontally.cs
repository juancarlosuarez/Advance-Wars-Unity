using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearImageHorizontally : ICommandTask
{
    private RectTransform _dataSizeImage;
    private float _offset = .02f;
    private bool _isFinishAnimation;

    public AppearImageHorizontally(RectTransform dataSizeImage)
    {
        _dataSizeImage = dataSizeImage;
    }
    public IEnumerator Execute()
    {
        _isFinishAnimation = false;
        _dataSizeImage.localScale = new Vector2(0, 1);
        while (!_isFinishAnimation)
        {
            MakeAnimation();
            yield return null;
        }
        FinishExecute();
    }
    private void MakeAnimation()
    {
        var scaleImageX = _dataSizeImage.localScale.x + _offset;
        if (scaleImageX >= 1)
        {
            _isFinishAnimation = true;
            _dataSizeImage.localScale = Vector3.one;
            return;
        }
        _dataSizeImage.localScale = new Vector3(scaleImageX, _dataSizeImage.localScale.y);
    }
    public void FinishExecute()
    {
        CommandQueue.GetInstance.CurrentCommandFinish();
    }
}
