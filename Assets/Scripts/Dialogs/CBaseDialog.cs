using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class CBaseDialog : MonoBehaviour
{
    [SerializeField] protected Animator _animator;

    protected object data;
    protected UnityAction callbackShow;

    protected const string DIALOG_SHOW_ANIM = "dialog_show";

#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        this._animator = this.GetComponent<Animator>();
    }
#endif

    protected void OnEnable()
    {
        this._animator.Play(DIALOG_SHOW_ANIM);
    }

    public virtual void ClickCloseDialog()
    {
        this.OnHide();
    }

    public virtual void OnShow(object data = null, UnityAction callback = null)
    {
        // if (data != null) Debug.Log(data.GetType());
        this.data = data;
        this.callbackShow = callback;
        this.gameObject.SetActive(true);
    }

    public virtual void OnCompleteShow()
    {
        if (this.callbackShow != null)
        {
            var bk = this.callbackShow;
            this.callbackShow = null;
            bk.Invoke();
        }

        // Debug.Log(this.GetType().Name + " Complete Show");
    }

    public virtual void OnHide()
    {
        // this._animator.Play(ANIMATOR_HIDE);
    }

    public virtual void OnCompleteHide()
    {
        this.gameObject.SetActive(false);
    }

    public virtual void OnBtnCloseClicked()
    {
        CGameSoundManager.Instance.PlayFx(GameDefine.BUTTON_CLICK_FX_KEY);
        this.OnHide();
    }
}
