using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#region Enums


public enum EnumHurtType:int
{
	Normal,
	Metal,//金
	Wood,
	Water,
	Fire,
	Earth,
	MonsterNormalHurt
}

public enum EnumPetElementType:int
{

	Metal =1,//金
	Wood=2,
	Water=3,
	Fire=4,
	Earth=5,
}

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

public enum EnumOperation
{
    Add,
    Remove,
    Update,
    Clear,
    None
}

public enum EnumUpdateOrder : int
{
    First = 1,
    Second = 2,
    Third = 3,
    Four = 4,
    EnumSceneModule,
    LanguageModule,
    EnumCastleDefenseModule,
    EnumTiroMissionModule,
}

/// <summary>
/// 消息传输类型 
/// </summary>
public enum EnumRequestType
{
    GET,
    POST
}

public enum EnumStartMode : int
{
    Create,
    Normal,
    Login
}


public class GameLayer
{
    public const int Nothing = 0;
    public const int UI = 5;
    public const int UI_3D = 9;
    
    public const int sprite = 12;                              //一般精灵层，场景美术也有用到， 
    public const int Player = 14;                              //玩家精灵层.
}


#endregion