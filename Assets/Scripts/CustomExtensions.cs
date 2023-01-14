using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomExtensions
{
    public static class StringExtensions
    {
        public static void FancyDebug(this string str)
        {
            Debug.LogFormat(
                "このstringには {0} 個の文字が含まれている。",
                str.Length
            );
        }
    }
}
