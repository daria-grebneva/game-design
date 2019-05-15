using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public static class LevelGenerator
    {
        private static Dictionary<int, string> dots= new Dictionary<int, string>();

        private static List<GameObject> generatedDots;
        
        private static List<String> generatedDotsStr;
        
        private static List<int> variants = new List<int>();

        public static List<GameObject> GetGeneratedList()
        {
            return generatedDots;
        }
        
        public static List<String> GetGeneratedListStr()
        {
            return generatedDotsStr;
        }
        
        public static void generate()
        {
            generatedDots = new List<GameObject>();
            generatedDotsStr = new List<string>();
            GenerateDotsDictionary();
            GenerateVariants();
            int numberOfVariant = Random.Range(1, variants.Count);
            int numberOfDots = variants[numberOfVariant];
            while (numberOfDots != 0)
            {
                int currentNumber = Random.Range(1, 24);
                string value = dots[currentNumber];
                if (!generatedDotsStr.Contains(value))
                {
                    generatedDotsStr.Add(value);
                    --numberOfDots;
                }
            }

            GenerateDotsList(generatedDotsStr);
        }

        private static void GenerateVariants()
        {
            variants.Clear();
            variants.Add(3);
            variants.Add(3);
            variants.Add(6);
            variants.Add(6);
            variants.Add(3);
            variants.Add(4);
            variants.Add(5);
            variants.Add(5);
            variants.Add(3);
            variants.Add(3);
            variants.Add(4);
            variants.Add(6);
            variants.Add(4);
            variants.Add(5);
            variants.Add(4);
            variants.Add(7);
            variants.Add(4);
            variants.Add(5);
            variants.Add(5);
            variants.Add(5);
            variants.Add(6);
            variants.Add(4);
            variants.Add(7);
            variants.Add(4);
            variants.Add(3);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(3);
            variants.Add(3);
            variants.Add(6);
            variants.Add(4);
            variants.Add(5);
            variants.Add(7);
            variants.Add(5);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(3);
            variants.Add(5);
            variants.Add(6);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(5);
            variants.Add(7);
            variants.Add(5);
            variants.Add(3);
            variants.Add(4);
            variants.Add(4);
            variants.Add(5);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(4);
            variants.Add(6);
            variants.Add(5);
            variants.Add(5);
            variants.Add(4);
            variants.Add(6);
            variants.Add(3);
            variants.Add(3);
            variants.Add(6);
            variants.Add(5);
            variants.Add(5);
            variants.Add(7);
        }

        private static void GenerateDotsList(List<string> dotsArray)
        {
            foreach (var element in dotsArray)
            {
                generatedDots.Add(GameObject.Find(element));
            }
        }

        private static void GenerateDotsDictionary()
        {
            dots.Clear();
            dots.Add(1, "Dot11");
            dots.Add(2, "Dot21");
            dots.Add(3, "Dot31");
            dots.Add(4, "Dot41");
            
            dots.Add(5, "Dot12");
            dots.Add(6, "Dot22");
            dots.Add(7, "Dot32");
            dots.Add(8, "Dot42");
            
            dots.Add(9, "Dot13");
            dots.Add(10, "Dot23");
            dots.Add(11, "Dot33");
            dots.Add(12, "Dot43");
            
            dots.Add(13, "Dot14");
            dots.Add(14, "Dot24");
            dots.Add(15, "Dot34");
            dots.Add(16, "Dot44");
            
            dots.Add(17, "Dot15");
            dots.Add(18, "Dot25");
            dots.Add(19, "Dot35");
            dots.Add(20, "Dot45");
            
            dots.Add(21, "Dot16");
            dots.Add(22, "Dot26");
            dots.Add(23, "Dot36");
            dots.Add(24, "Dot46");
        }

    }
}