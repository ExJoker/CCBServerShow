using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseUI : MonoBehaviour {
    public Graphic graphic;
    //要实现噪波的UI
    public float intensity = 0.2f;
    //强度系数
    public AnimationCurve curve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 0f), new Keyframe(0.5f, 1f, 0f, 0f), new Keyframe(1f, 0f, 0f, 0f));
    //振动与时间的曲线
    public Material material_Noice;
    //叠加的噪波材质
    private int shakeStrength;
    //波动强度
    private int progressRate;
    //波动进度
    private float lifeTime = 0;
    //噪波抖动的生命周期
    private float currentTime = 0f;
    //当前波动时间

    void Awake()
    {
        shakeStrength = Shader.PropertyToID("_ShakeStrength");  //获取shaderValue的属性标识符

        progressRate = Shader.PropertyToID("_ProgressRate");  //从"_NormalizedTime"中获取shaderValue的属性标识符       
        graphic = gameObject.GetComponent<Graphic>();
    }

    public void Shake()
    {
        graphic.material = material_Noice;

      StartCoroutine(Interactive());
    }
 
    private IEnumerator Interactive()
    {
        currentTime = 0f; //已运行时间

        lifeTime = curve.keys[curve.length - 1].time;   //从曲线中获取振动的生命周期

        while (currentTime < lifeTime)  //如果在生命周期内
        {
            currentTime += Time.deltaTime;

            var shakeValue = curve.Evaluate(currentTime) * intensity;

            graphic.material.SetFloat(shakeStrength, shakeValue);  //把振动值传递给Shader

            graphic.material.SetFloat(progressRate, currentTime / lifeTime);  //把运行进度传递给Shader
           
            yield return null;   //等待画面刷新
        }
        graphic.material = null;
    }
    //public IEnumerator ShakeAlpha() {
       
    //}
}
