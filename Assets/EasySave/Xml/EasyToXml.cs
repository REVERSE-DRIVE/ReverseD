using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace EasySave.Xml
{
    public static class EasyToXml
    {
        private static readonly string LocalPath = Application.dataPath + "/XML/";
        private static XmlSerializer _serializer;
        
        /** <summary>
         * obj를 xml 파일로 저장
         * </summary>
         * <param name="obj">저장할 obj</param>
         * <param name="xmlFileName">xml 파일 이름</param>
         */
        public static void ToXml<T>(T obj, string xmlFileName)
        {
            if (!Directory.Exists(LocalPath))
            {
                Debug.Log("폴더가 존재하지 않습니다.");
                Debug.Log("폴더를 생성합니다.");
                Directory.CreateDirectory(LocalPath);
                Debug.Log("저장 경로: " + LocalPath);
            }
            string path = LocalPath + xmlFileName + ".xml";
            _serializer = new XmlSerializer(typeof(T));
            using FileStream stream = new FileStream(path, FileMode.Create);
            _serializer.Serialize(stream, obj);
        }
        
        /** <summary>
         * xml 파일을 읽어서 객체로 반환
         * </summary>
         * <param name="xmlFileName">xml 파일 이름</param>
         * <returns>xml 파일을 읽어 만든 객체</returns>
         */
        public static T FromXml<T>(string xmlFileName)
        {
            string path = LocalPath + xmlFileName + ".xml";
            if (!File.Exists(path))
            {
                Debug.Log("파일이 존재하지 않습니다.");
                Debug.Log("파일을 생성합니다.");
                T defaultObj = default;
                ToXml(defaultObj, xmlFileName);
                return defaultObj;
            }
            _serializer = new XmlSerializer(typeof(T));
            using FileStream stream = new FileStream(path, FileMode.Open);
            T obj = (T)_serializer.Deserialize(stream);
            return obj;
        }
        
        /** <summary>
         * List를 xml 파일로 저장
         * </summary>
         * <param name="list">저장할 List</param>
         * <param name="xmlFileName">xml 파일 이름</param>
         */
        public static void ListToXml<T>(List<T> list, string xmlFileName)
        {
            if (!Directory.Exists(LocalPath))
            {
                Debug.Log("폴더가 존재하지 않습니다.");
                Debug.Log("폴더를 생성합니다.");
                Directory.CreateDirectory(LocalPath);
                Debug.Log("저장 경로: " + LocalPath);
            }
            string path = LocalPath + xmlFileName + ".xml";
            _serializer = new XmlSerializer(typeof(List<T>));
            using FileStream stream = new FileStream(path, FileMode.Create);
            _serializer.Serialize(stream, list);
        }

        /** <summary>
         * xml 파일을 읽어서 List로 반환
         * </summary>
         * <param name="xmlFileName">xml 파일 이름</param>
         * <returns>xml 파일을 읽어서 만든 List</returns>
         */
        public static List<T> ListFromXml<T>(string xmlFileName)
        {
            string path = LocalPath + xmlFileName + ".xml";
            if (!File.Exists(path))
            {
                Debug.Log("파일이 존재하지 않습니다.");
                Debug.Log("파일을 생성합니다.");
                List<T> defaultList = new List<T>();
                ListToXml(defaultList, xmlFileName);
                return defaultList;
            }
            _serializer = new XmlSerializer(typeof(List<T>));
            using FileStream stream = new FileStream(path, FileMode.Open);
            List<T> list = (List<T>)_serializer.Deserialize(stream);
            return list;
        }
        
        /** <summary>
         * Dictionary를 xml 파일로 저장
         * </summary>
         * <param name="dict">저장할 Dictionary</param>
         * <param name="xmlFileName">xml 파일 이름</param>
         */
        [Obsolete("이 메서드는 불안정합니다. 사용하지 않는 걸 권장합니다. EasyToXml.DictionaryToXml<TKey, TValue>를 사용하세요.")]
        public static void DictionaryToXml<TKey, TValue>(Dictionary<TKey, TValue> dict, string xmlFileName)
        {
            if (!Directory.Exists(LocalPath))
            {
                Debug.Log("폴더가 존재하지 않습니다.");
                Debug.Log("폴더를 생성합니다.");
                Directory.CreateDirectory(LocalPath);
                Debug.Log("저장 경로: " + LocalPath);
            }

            XElement root = 
                new XElement("Root", 
                    from kv in dict 
                    select new XElement(kv.Key.ToString(), kv.Value));
            string path = LocalPath + xmlFileName + ".xml";
            root.Save(path);
        }

        /** <summary>
         * xml 파일을 읽어서 Dictionary로 반환
         * </summary>
         * <param name="xmlFileName">xml 파일 이름</param>
         * <returns>xml 파일을 읽어서 만든 Dictionary</returns>
         */
        [Obsolete("이 메서드는 불안정합니다. 사용하지 않는 걸 권장합니다. EasyToXml.DictionaryFromXml<TKey, TValue>를 사용하세요.")]
        public static Dictionary<TKey, TValue> DictionaryFromXml<TKey, TValue>(string xmlFileName)
        {
            string path = LocalPath + xmlFileName + ".xml";
            if (!File.Exists(path))
            {
                Debug.Log("파일이 존재하지 않습니다.");
                Debug.Log("파일을 생성합니다.");
                Dictionary<TKey, TValue> defaultDict = new Dictionary<TKey, TValue>();
                DictionaryToXml(defaultDict, xmlFileName);
                return defaultDict;
            }
            XElement root = XElement.Load(path);
            Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();
            foreach (XElement xl in root.Elements())
            {
                dict.Add(
                    (TKey)System.Convert.ChangeType(xl.Name.LocalName, typeof(TKey)), 
                    (TValue)System.Convert.ChangeType(xl.Value, typeof(TValue))
                    );
            }
            return dict;
        }
    }
}
