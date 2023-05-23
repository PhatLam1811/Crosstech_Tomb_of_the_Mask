using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayerCollider : MonoBehaviour
{
    public System.Action<CPlayerCollider> onCollider;
    public BoxCollider2D _collider;

    public bool _isCollided;

#if UNITY_EDITOR
    private void OnValidate()
    {
        this._collider = this.GetComponent<BoxCollider2D>();
    }
#endif

    /// <summary>
    /// Collider nên bắt được và handle tại đây
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnHandleWhenCollideWall();
    }
    /// <summary>
    /// Nếu collision lỗi ko bắt được tại lúc enter => check Stay
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!_isCollided)
        {
            OnHandleWhenCollideWall();
        }
    }
    /// <summary>
    /// Khi collider đang enable chạm tường => set state để không gọi lúc Stay nữa và invoke callback
    /// </summary>
    private void OnHandleWhenCollideWall()
    {
        _isCollided = true;
        onCollider?.Invoke(this);
    }
    /// <summary>
    /// Chuyển state thành chờ nhận collide
    /// </summary>
    public void ReadyForCatchCollide()
    {
        TurnCollider(true);
        _isCollided = false;
    }
    public void TurnCollider(bool isON)
    {
        this._collider.enabled = isON;
    }
}
