using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Imagemanager : MonoBehaviour
{
    public Sprite normal;
    public Sprite happy;
    public Sprite excited;
    public Sprite cry;
    public Sprite sad;
    private Image kanaeImage;
    private Tweener breathingTweener;

    private void Start()
    {
        kanaeImage = GetComponent<Image>();
        StartBreathingEffect();
    }

    public void changeImage(string index)
    {
        switch (index)
        {
            case "normal":
                kanaeImage.sprite = normal;
                StartBreathingEffect();
                break;
            case "happy":
                kanaeImage.sprite = happy;
                StartBreathingEffect();
                break;
            case "excited":
                kanaeImage.sprite = excited;
                StartBreathingEffect();
                break;
            case "cry":
                kanaeImage.sprite = cry;
                StartBreathingEffect();
                break;
            case "sad":
                kanaeImage.sprite = sad;
                StopBreathingEffect();
                break;
        }
    }

    private void StartBreathingEffect()
    {
        if (breathingTweener == null || !breathingTweener.IsPlaying())
        {
            kanaeImage.transform.localRotation = Quaternion.Euler(0, 0, 0);
            breathingTweener = kanaeImage.transform.DORotate(new Vector3(0, 10, 5), 2f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void StopBreathingEffect()
    {
        if (breathingTweener != null && breathingTweener.IsPlaying())
        {
            breathingTweener.Kill();
            kanaeImage.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
