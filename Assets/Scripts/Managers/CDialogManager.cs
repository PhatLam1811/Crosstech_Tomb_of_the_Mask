using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CDialogManager : MonoSingleton<CDialogManager>
{
    private Dictionary<string, GameObject> _dialogs;

    public void OpenApp()
    {
        this.SetUpDictionary();
    }

    private void SetUpDictionary()
    {
        this._dialogs = new Dictionary<string, GameObject>();
    }

    public void ShowDialog<T>(string path, Transform canvasPos, object data = null, UnityAction callbackCompleteShow = null) where T : CBaseDialog
    {
        if (this._dialogs == null)
        {
            this.SetUpDictionary();
        }

        if (this._dialogs.TryGetValue(path, out GameObject dialog))
        {
            dialog.SetActive(true);
        }
        else
        {
            GameObject dialogPrefab = CGameManager.Instance.GetResourceFile<GameObject>(path);

            if (dialogPrefab != null)
            {
                this._dialogs.Add(path, dialogPrefab);

                T dialogComponent = (Instantiate(dialogPrefab, canvasPos)).GetComponent<T>();

                if (dialogComponent != null)
                {
                    dialogComponent.OnShow(data, callbackCompleteShow);
                }
            }
        }
    }
}
