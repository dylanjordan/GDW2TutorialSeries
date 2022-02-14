using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonDefinition : MonoBehaviour
{
    public Color _unselectedTint = Color.grey;
    public Color _selectedTint = Color.white;

    public bool _animated = false;
    public bool _selected = false;

    private bool _disableControls = false;

    private Button _button;
    private Image _image;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();

        _animated = TryGetComponent<Animator>(out _animator);

        if (!_animated)
        {

            if (_selected)
            {
                _image.color = _selectedTint;
            }
            else
            {
                _image.color = _unselectedTint;
            }
        }
    }

    public void SwappedTo()
    {
        _selected = true;

        if (_animated)
        {
            _animator.SetBool("Selected", _selected);
        }
        else
        {
            _image.color = _selectedTint;
        }
    }

    public void SwappedOff()
    {
        _selected = false;

        if (_animated)
        {
            _animator.SetBool("Selected", _selected);
        }
        else
        {
            _image.color = _unselectedTint;
        }
    }

    public void ClickButton()
    {
        if (!_disableControls)
        {
            _disableControls = true;

            _button.onClick.Invoke();

            _disableControls = false;
        }
    }

    public bool GetDisableControls()
    {
        return _disableControls;
    }
}
