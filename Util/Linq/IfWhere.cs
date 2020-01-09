﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace System.Linq
{
    /// <summary>
    /// Linq Where查询条件的扩展
    /// 
    /// if(string.IsNullOrEmpty(name) == false)
    /// result = source.Where(p => p.Name.Contains(name));
    /// 
    /// 对于这样的条件我们使用本扩展进行编写, 增强代码易读性
    /// .IfWhere(string.IsNullOrEmpty(name) == false, p => p.Name.Contains(name))
    /// </summary>
    public static class IfWhereExtension
    {
        public static IQueryable<T> IfWhere<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IQueryable<T> IfWhere<T>(this IQueryable<T> source, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IEnumerable<T> IfWhere<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static IEnumerable<T> IfWhere<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}