using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// 需要在系统加载的时候通过new来进行创建.
    /// </summary>
    public class GameDebug
    {
        private static GameDebug instance;

        private static GameDebug Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameDebug();
                return instance;
            }
        }

        public GameDebug()
        {
            GameDebug.instance = this;
        }

		//游戏场景等待太长了
		public static bool ShortTimeEnterScene = true;

		//游戏场景 测试用
		public static bool ShowDebugBar = false;

		//是否关闭AI
		public static bool CloseAI = false;


        /// <summary>
        /// 需要重写.
        /// </summary>
        /// <param name="str"></param>
        protected virtual void Print(object str)
        {
			if (str == null)
				return;
            switch (logLevel)
            {
                case LogLevel.ERROR:
                    Debug.LogError(str);
                    break;
                case LogLevel.WORNING:
                    Debug.LogWarning(str);
                    break;
                case LogLevel.DEBUG:
                    Debug.Log(str);
                    break;
            }
        }

        /// <summary>
        /// log日志级别
        /// </summary>
        public enum LogLevel
        {
            DEBUG = 0,
            INFO = 10,
            WORNING = 20,
            EXCEPTION = 30,
            ERROR = 40,
            NULL = 50,
        }

        /// <summary>
        /// 当前log级别.
        /// </summary>
        public static LogLevel logLevel = LogLevel.DEBUG;
        private static bool isPrintDebugInfo = true;

        public static void SetIsPrintDebugInfo(bool value)
        {
            isPrintDebugInfo = value;
        }
        /// <summary>
        /// 打印普通日志
        /// </summary>
        /// <param name="message">打印信息</param>
        public static void Log(object message)
        {
            if (!isPrintDebugInfo)
            {
                return;
            }
            logLevel = LogLevel.DEBUG;
            if (logLevel <= LogLevel.DEBUG)
            {
                Instance.Print(message);
            }
        }

        public static void LogWarning(object message)
        {
            if (!isPrintDebugInfo)
            {
                return;
            }
            logLevel = LogLevel.WORNING;
            if (logLevel <= LogLevel.WORNING)
            {
                Instance.Print(message);
            }
        }

        public static void LogException(Exception exception)
        {
            if (!isPrintDebugInfo)
            {
                return;
            }
            if (logLevel <= LogLevel.EXCEPTION)
            {
                Instance.Print(exception.ToString());
            }
        }

        public static void LogError(object message)
        {
            if (!isPrintDebugInfo)
            {
                return;
            }
            logLevel = LogLevel.ERROR;
            if (logLevel <= LogLevel.ERROR)
            {
                Instance.Print(message);
            }
        }
    }
}
