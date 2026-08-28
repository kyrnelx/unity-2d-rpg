using System.Collections;
using System.Text;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

public class LoadingView : UIBehaviour
{
    private Slider _progressSlider;
    private Text _progressText;
    private float _smoothSpeed = 50f;
    private float _accelerateSpeed = 100f;
    private string _currentText;
    private StringBuilder _stringBuilder;
    private string _loadingSymbol = "...";
    private string _percentageSymbol = "%";

    protected override void ParseComponent()
    {
        _stringBuilder = new StringBuilder();
        _progressSlider = FindComponent<Slider>("Slider_LoadingBar");
        _progressText = FindComponent<Text>("Slider_LoadingBar/Text_LoadingValue");
    }

    public override void Show()
    {
        base.Show();
        _progressSlider.value = 0;
        _progressText.GetComponent<Localize>().SetTerm("Loading");
        _currentText = _progressText.text;
        CoroutineManager.Instance.StartCoroutine(FillProgress());
    }

    public override void Hide()
    {
        if (_progressSlider.value < 100)
        {
            CoroutineManager.Instance.StartCoroutine(FillToComplete());
        }
        else
        {
            base.Hide();
        }
    }

    private IEnumerator FillProgress()
    {
        while (_progressSlider.value < 100)
        {
            _progressSlider.value = Mathf.MoveTowards(_progressSlider.value, 100, _smoothSpeed * Time.deltaTime);
            _stringBuilder.Clear();
            _stringBuilder.Append(_currentText).Append(_loadingSymbol).Append(Mathf.RoundToInt(_progressSlider.value))
                .Append(_percentageSymbol);
            _progressText.text = _stringBuilder.ToString();
            yield return null;
        }
    }

    private IEnumerator FillToComplete()
    {
        while (_progressSlider.value < 100)
        {
            _progressSlider.value = Mathf.MoveTowards(_progressSlider.value, 100, _accelerateSpeed * Time.deltaTime);
            _stringBuilder.Clear();
            _stringBuilder.Append(_currentText).Append(_loadingSymbol).Append(Mathf.RoundToInt(_progressSlider.value))
                .Append(_percentageSymbol);
            yield return null;
        }

        base.Hide();
    }
}