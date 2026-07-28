using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Com_Title_Download : Com_Base
{
    //
    [SerializeField] TextMeshProUGUI _progressValue = null;
    [SerializeField] Slider _progressSlider = null;

    /// <summary>
    /// 
    /// </summary>
    public void Init()
    {
        _progressValue.text = string.Empty;
        _progressSlider.gameObject.SetActive(false);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Tick()
    {
        if (Manager_Addressable.Instance.pIsAcceptedDownload)
        {
            //
            if (_progressSlider.gameObject.activeSelf == false)
                _progressSlider.gameObject.SetActive(true);

            //
            _progressValue.text = string.Format("({0}/{1}) {2}%",
                Manager_UI.Instance.GetFileSize(Manager_Addressable.Instance.pDownloadedBytes),
                Manager_UI.Instance.GetFileSize(Manager_Addressable.Instance.pTotalBytes),
                Manager_Addressable.Instance.pDownloadPercent);
            _progressSlider.value = Manager_Addressable.Instance.pDownloadPercent;
        }
    }
}
