using UnityEngine;
using UnityEngine.UI;

public class CtrlConfigButton : MonoBehaviour
{

    public int keyType;
    private Text text;
    private Animator anim;
    private CommonButton button;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        anim = GetComponent<Animator>();
        button = PlayerController.instance.keyList.GetByKeyType((KeyType)keyType);
    }

    private void OnEnable()
    {
        button = PlayerController.instance.keyList.GetByKeyType((KeyType)keyType);
    }

    void Update()
    {
        text.text = button.GetKeyCode().ToString();
        anim.SetBool("Clash", button.isClash);
    }

}
