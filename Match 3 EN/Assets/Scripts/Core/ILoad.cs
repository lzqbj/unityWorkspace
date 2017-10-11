using System;

/*
 *描述模块的统一加载方式和状态 
 */
namespace Core
{
    public interface ILoad
    {
        void Load();
        void Release();
        EnumObjectState State { get; }
    }

}
