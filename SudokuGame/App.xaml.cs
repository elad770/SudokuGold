using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SudokuGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 






    public partial class App : Application
    {


        public int LengthOfLongestSubstring(string s)
        {

            int max = 0;
            int[] asciTable = new int[255];

            // int[] asciTable = new int[94];
            int j = 0;
            // int[] appearances = new int[94];
            for (int i = 0; i < s.Length && max < s.Length - i; i++)
            {

                int temp_max = 0;

                for (j = i; j < s.Length; j++)
                {
                    int asciiValue = s[j];
                    //if (asciTable[asciiValue] == appearances[asciiValue] + 1)
                    //{
                    //    appearances[asciiValue] = asciTable[asciiValue];
                    //    break;
                    //}
                    if (asciTable[asciiValue] == 1)
                    {
                        break;
                    }
                    asciTable[asciiValue]++;
                    temp_max++;
                }
                max = Math.Max(temp_max, max);
                for (int k = 0; k < j - i; k++)
                {
                    asciTable[s[k + i]]--;
                }

            }
            return max;
        }
        //private void PrintIList(IList<IList<int>> list)
        //{
        //    foreach (var sublist in list)
        //    {
        //        string sublistAsString = string.Join(", ", sublist);
        //        Debug.WriteLine(sublistAsString);
        //    }
        //}

        //public void InternalCombine(IList<IList<int>> li_i, int[] arr, int len, int value, int n, int k)
        //{

        //    if (k == len)
        //    {
        //        return;
        //    }
        //    for (int i = value; i <= n; i++)
        //    {
        //        arr[len] = i;
        //        InternalCombine(li_i, arr, len + 1, i + 1, n, k);
        //        if (len == k - 1)
        //        {
        //            li_i.Add(arr.ToList());
        //        }
        //    }

        //}

        //public IList<IList<int>> Combine(int n, int k)
        //{
        //    IList<IList<int>> li_i = new List<IList<int>>();
        //    int[] arr = new int[k];
        //    int len = 0;
        //    int value_start = 1;
        //    InternalCombine(li_i, arr, len, value_start, n, k);
        //    return li_i;
        //}
        public App()
        {
            //string str = "abcabdbb";
            //Debug.WriteLine(LengthOfLongestSubstring(str));
            //str = "pwwkew";
            //LengthOfLongestSubstring(str);
            //Debug.WriteLine(LengthOfLongestSubstring(str));
            //str = "bbBBaaxla";
            //LengthOfLongestSubstring(str);
            //Debug.WriteLine(LengthOfLongestSubstring(str));
            //str = "abbac";
            //Debug.WriteLine(LengthOfLongestSubstring(str));
            //str = "iohdoisHDSVVusaAAIHXZxz#@!xsaiyyz";
            //Debug.WriteLine(LengthOfLongestSubstring(str));
            //str = "abcdaecbb";
            //Debug.WriteLine(LengthOfLongestSubstring(str));
            // PrintIList(Combine(6, 2));
        }



    }
    public class ColorDictionary : Dictionary<string, string> { }
}
