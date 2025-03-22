using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

[RequireComponent(typeof(TextMeshProUGUI))]
public class AnimationTextLoading : MonoBehaviour
{
    [BoxGroup("Setup")]
    [SerializeField] private float _animationInterval = 0.5f;
    
    private TextMeshProUGUI _text;

    private void Start()
    {
        if (_text == null)
            _text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(AnimateLoading());
    }

    private IEnumerator AnimateLoading()
    {
        while (true)
        {
            for (int i = 1; i <= 3; i++)
            {
                _text.text = "loading" + new string('.', i);
                yield return new WaitForSeconds(_animationInterval);
            }
            _text.text = "loading";
            yield return new WaitForSeconds(_animationInterval);
        }
    }
}
