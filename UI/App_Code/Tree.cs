using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

/// <summary>
/// Tree 的摘要说明
/// </summary>
[Serializable]
public class Tree
{
	public Tree()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    private int id;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    private string text;

    public string Text
    {
        get { return text; }
        set { text = value; }
    }
    private string state;

    public string State
    {
        get { return state; }
        set { state = value; }
    }
}