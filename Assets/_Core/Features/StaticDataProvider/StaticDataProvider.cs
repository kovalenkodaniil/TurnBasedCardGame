﻿using System;
using System.Collections.Generic;
using UnityEditor;
using Object = UnityEngine.Object;

namespace Core.Data
{
    public interface IStaticDataProvider { }
    
    public static class StaticDataProvider
    {
        private static Dictionary<Type, IStaticDataProvider> datas = new(20);

        public static void Add<T>(T provider) where T : IStaticDataProvider
        {
            datas.TryAdd(typeof(T), provider);
        }
        
        public static void Replace<T>(T provider) where T : class, IStaticDataProvider
        {
            var key = typeof(T);
            
            if (datas.ContainsKey(key))
                datas[key] = provider;
        }

        public static T Get<T>() where T : class, IStaticDataProvider
        {
            datas.TryGetValue(typeof(T), out var data);
            return data as T;
        }
    }
}