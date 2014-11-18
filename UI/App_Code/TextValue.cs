using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TextValue 的摘要说明
/// </summary>
[Serializable]
public class TextValue
{
    public TextValue()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //

    }
    private string text;

    public string Text
    {
        get { return text; }
        set { text = value; }
    }
    private string value;

    public string Value
    {
        get { return this.value; }
        set { this.value = value; }
    }
}