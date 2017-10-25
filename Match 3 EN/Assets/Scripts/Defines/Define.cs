using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region Enums



/// <summary>
/// 对象当前状态 
/// </summary>
public enum EnumObjectState
{
    None,
    Initial,
    Loading,
    Ready,
    Render,
    Disabled,
    Pause,
    Closing
}



/// <summary>
/// 消息传输类型 
/// </summary>
public enum EnumRequestType
{
    GET,
    POST
}


#endregion