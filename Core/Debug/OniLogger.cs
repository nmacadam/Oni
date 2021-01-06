// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System.Collections.Generic;
using UnityEngine;

namespace Oni
{
    public class OniLogger
	{
        public enum OniLogType
        {
            Assert,
            Trace,
            Log,
            Info,
            Warning,
            Error,
            Exception,
            Fatal
        }

        [System.Serializable]
        public struct TagPair
        {
            public string OpeningTag;
            public string ClosingTag;
        }

        public readonly Dictionary<string, string> OniDefaultTags = new Dictionary<string, string>
        {
            { "system", "679fca" },
            { "performance", "73ca67" },
            { "graphics", "b667ca" },
            { "ai", "3cdac6" },
            { "audio", "7c67ca" },
            { "content", "c2ca67" },
            { "logic", "caa667" },
            { "gui", "85c9dd" },
            { "input", "90b47" },
            { "network", "67caa8" },
            { "animation", "679fca" },
            { "physics", "dd85ae" },
            { "event", "e2c7a3" }
        };

        private readonly Dictionary<OniLogType, TagPair> _typeColors = new Dictionary<OniLogType, TagPair>
        {
            { OniLogType.Assert,       new TagPair { OpeningTag = "<color=#ea889a>",    ClosingTag = "</color>"     } },
            { OniLogType.Trace,        new TagPair { OpeningTag = "<color=#9591aa>",    ClosingTag = "</color>"     } },
            { OniLogType.Log,          new TagPair { OpeningTag = "",                   ClosingTag = ""             } },
            { OniLogType.Info,         new TagPair { OpeningTag = "<color=#82e08a>",    ClosingTag = "</color>"     } },
            { OniLogType.Warning,      new TagPair { OpeningTag = "<color=#eadd88>",    ClosingTag = "</color>"     } },
            { OniLogType.Error,        new TagPair { OpeningTag = "<color=#ea889a>",    ClosingTag = "</color>"     } },
            { OniLogType.Exception,    new TagPair { OpeningTag = "<color=#ea889a>",    ClosingTag = "</color>"     } },
            { OniLogType.Fatal,        new TagPair { OpeningTag = "<color=#e04a65><b>", ClosingTag = "</b></color>" } }
        };

		//private OniLoggerInternal _internalLogger = new OniLoggerInternal();
		private ILogger _internalLogger = Debug.unityLogger;
        private bool _logEnabled = true;
        private Dictionary<string, string> _recognizedTags = new Dictionary<string, string>();
        
        public bool LogEnabled
        { 
            get => _logEnabled;
            set
            {
                _logEnabled = value;
                _internalLogger.logEnabled = _logEnabled;
            }
        }

        public OniLogger()
        {
            _recognizedTags = OniDefaultTags;
        }

        public OniLogger(bool enabled)
        {
            _logEnabled = enabled;
            _recognizedTags = OniDefaultTags;
        }

        public OniLogger(bool enabled, Dictionary<string, string> recognizedTags)
        {
            _logEnabled = enabled;
            _recognizedTags = recognizedTags;
        }

        private string GetTagString(string tag)
        {
            string s = "";
            string containedTag = string.Concat("▪️ ", tag);

            if (_recognizedTags.ContainsKey(tag))
            {
                string colorTag = string.Concat("<color=#", _recognizedTags[tag], ">");
                string richTextTag = string.Concat(colorTag, containedTag, "</color>");

                s = string.Concat(s, richTextTag, " ");
            }
            else
            {
                s = string.Concat(s, containedTag, " ");
            }

            return s;
        }

        private string GetTagString(IEnumerable<string> tags)
        {
            string s = "";
            foreach (var tag in tags)
            {
                s = string.Concat(s, GetTagString(tag));
            }

            return s;
        }

        private string GetMessageString(OniLogType type, object message)
        {
            var tags = _typeColors[type];
            return string.Concat("ONI\t\t" + tags.OpeningTag, message, tags.ClosingTag);
        }

        private LogType OniLogTypeToUnity(OniLogType type)
        {
            switch (type)
            {
                case OniLogType.Assert: return LogType.Assert;
                case OniLogType.Trace: return LogType.Log;
                case OniLogType.Log: return LogType.Log;
                case OniLogType.Info: return LogType.Log;
                case OniLogType.Warning: return LogType.Warning;
                case OniLogType.Error: return LogType.Error;
                case OniLogType.Exception: return LogType.Exception;
                case OniLogType.Fatal: return LogType.Error;
            }

            return LogType.Log;
        }

        private void PostMessage(OniLogType type, object message)
        {
            _internalLogger.Log(OniLogTypeToUnity(type), (object)GetMessageString(type, message));
        }

        private void PostMessage(OniLogType type, object message, Object context)
        {
            _internalLogger.Log(OniLogTypeToUnity(type), (object)GetMessageString(type, message), context);
        }

        private void PostMessage(OniLogType type, string tag, object message)
        {
            _internalLogger.Log(OniLogTypeToUnity(type), GetTagString(tag), (object)GetMessageString(type, message));
        }

        private void PostMessage(OniLogType type, string tag, object message, UnityEngine.Object context)
        {
            _internalLogger.Log(OniLogTypeToUnity(type), GetTagString(tag), (object)GetMessageString(type, message), context);
        }

        private void PostMessage(OniLogType type, IEnumerable<string> tags, object message)
        {
            _internalLogger.Log(OniLogTypeToUnity(type), GetTagString(tags), (object)GetMessageString(type, message));
        }

        private void PostMessage(OniLogType type, IEnumerable<string> tags, object message, UnityEngine.Object context)
        {
            _internalLogger.Log(OniLogTypeToUnity(type), GetTagString(tags), (object)GetMessageString(type, message), context);
        }



        public void Assert(bool condition)
        {
            if (!LogEnabled) return;
            if (!condition) PostMessage(OniLogType.Assert, "Assertion Failed");
        }

        public void Assert(bool condition, string tag)
        {
            if (!LogEnabled) return;
            if (!condition) PostMessage(OniLogType.Assert, tag, "Assertion Failed");
        }

        public void Assert(bool condition, IEnumerable<string> tags)
        {
            if (!LogEnabled) return;
            if (!condition) PostMessage(OniLogType.Assert, tags, "Assertion Failed");
        }

        public void Assert(bool condition, Object context)
        {
            if (!LogEnabled) return;
            if (!condition) PostMessage(OniLogType.Assert, (object)"Assertion Failed", context);
        }

        public void Assert(bool condition, string tag, Object context)
        {
            if (!LogEnabled) return;
            if (!condition) PostMessage(OniLogType.Assert, tag, (object)"Assertion Failed", context);
        }

        public void Assert(bool condition, IEnumerable<string> tags, Object context)
        {
            if (!LogEnabled) return;
            if (!condition) PostMessage(OniLogType.Assert, tags, (object)"Assertion Failed", context);
        }

        public void Assert(bool condition, object message)
        {
            if (!LogEnabled) return;
            if (!condition) PostMessage(OniLogType.Assert, message);
        }

        public void Assert(bool condition, string tag, object message)
        {
            if (!LogEnabled) return;
            if (!condition) PostMessage(OniLogType.Assert, tag, message);
        }

        public void Assert(bool condition, IEnumerable<string> tags, object message)
        {
            if (!LogEnabled) return;
            if (!condition) PostMessage(OniLogType.Assert, tags, message);
        }

        public void Assert(bool condition, object message, Object context)
        {
            if (!LogEnabled) return;
            if (!condition) PostMessage(OniLogType.Assert, message, context);
        }

        public void Assert(bool condition, string tag, object message, Object context)
        {
            if (!LogEnabled) return;
            if (!condition) PostMessage(OniLogType.Assert, tag, message, context);
        }

        public void Assert(bool condition, IEnumerable<string> tags, object message, Object context)
        {
            if (!LogEnabled) return;
            if (!condition) PostMessage(OniLogType.Assert, tags, message, context);
        }

        public void Assert(object message)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Assert, message);
		}

        public void Assert(object message, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
            PostMessage(OniLogType.Assert, message, context);
		}

        public void Assert(object message, string tag)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Assert, tag, message);
		}
        
        public void Assert(object message, IEnumerable<string> tags)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Assert, tags, message);
		}

        public void Assert(object message, string tag, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Assert, tag, message, context);
		}



        public void Trace(object message)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Trace, message);
		}

        public void Trace(object message, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
            PostMessage(OniLogType.Trace, message, context);
		}

        public void Trace(object message, string tag)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Trace, tag, message);
		}
        
        public void Trace(object message, IEnumerable<string> tags)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Trace, tags, message);
		}

        public void Trace(object message, string tag, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Trace, tag, message, context);
		}

        public void Trace(object message, IEnumerable<string> tags, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Trace, tags, message, context);
		}

        public void Log(object message)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Log, message);
		}

        public void Log(object message, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
            PostMessage(OniLogType.Log, message, context);
		}

        public void Log(object message, string tag)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Log, tag, message);
		}
        
        public void Log(object message, IEnumerable<string> tags)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Log, tags, message);
		}

        public void Log(object message, string tag, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Log, tag, message, context);
		}

        public void Log(object message, IEnumerable<string> tags, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Log, tags, message, context);
		}

        public void Info(object message)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Info, message);
		}

        public void Info(object message, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
            PostMessage(OniLogType.Info, message, context);
		}

        public void Info(object message, string tag)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Info, tag, message);
		}
        
        public void Info(object message, IEnumerable<string> tags)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Info, tags, message);
		}

        public void Info(object message, string tag, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Info, tag, message, context);
		}

        public void Info(object message, IEnumerable<string> tags, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Info, tags, message, context);
		}

		public void Warning(object message)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Warning, message);
		}

        public void Warning(object message, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
            PostMessage(OniLogType.Warning, message, context);
		}

        public void Warning(object message, string tag)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Warning, tag, message);
		}
        
        public void Warning(object message, IEnumerable<string> tags)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Warning, tags, message);
		}

        public void Warning(object message, string tag, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Warning, tag, message, context);
		}

        public void Warning(object message, IEnumerable<string> tags, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Warning, tags, message, context);
		}

		public void Error(object message)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Error, message);
		}

        public void Error(object message, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
            PostMessage(OniLogType.Error, message, context);
		}

        public void Error(object message, string tag)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Error, tag, message);
		}
        
        public void Error(object message, IEnumerable<string> tags)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Error, tags, message);
		}

        public void Error(object message, string tag, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Error, tag, message, context);
		}

        public void Error(object message, IEnumerable<string> tags, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Error, tags, message, context);
		}

		public void Exception(System.Exception exception)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Exception, exception);
		}

        public void Exception(System.Exception exception, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
            PostMessage(OniLogType.Exception, exception, context);
		}

        public void Exception(System.Exception exception, string tag)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Exception, tag, exception);
		}
        
        public void Exception(System.Exception exception, IEnumerable<string> tags)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Exception, tags, exception);
		}

        public void Exception(System.Exception exception, string tag, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Exception, tag, exception, context);
		}

        public void Exception(System.Exception exception, IEnumerable<string> tags, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Exception, tags, exception, context);
		}

		public void Fatal(object message)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Fatal, message);
		}

        public void Fatal(object message, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
            PostMessage(OniLogType.Fatal, message, context);
		}

        public void Fatal(object message, string tag)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Fatal, tag, message);
		}
        
        public void Fatal(object message, IEnumerable<string> tags)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Fatal, tags, message);
		}

        public void Fatal(object message, string tag, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Fatal, tag, message, context);
		}

        public void Fatal(object message, IEnumerable<string> tags, UnityEngine.Object context)
		{
            if (!LogEnabled) return;
			PostMessage(OniLogType.Fatal, tags, message, context);
		}
	}
}