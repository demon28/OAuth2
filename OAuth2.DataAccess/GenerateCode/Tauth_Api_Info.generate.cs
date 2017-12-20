   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_Api_Info.generate.cs
 * CreateTime : 2017-11-23 09:31:13
 * CodeGenerateVersion : 1.0.0.0
 * TemplateVersion: 2.0.0
 * E_Mail : zhj.pavel@gmail.com
 * Blog : 
 * Copyright (C) YXH
 * 
 ***************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Winner.Framework.Core.DataAccess.Oracle;
using OAuth2.Entities;

namespace OAuth2.DataAccess
{
	/// <summary>
	/// API服务接口信息
	/// </summary>
	public partial class Tauth_Api_Info : DataAccessBase
	{
		#region 构造和基本
		public Tauth_Api_Info():base()
		{}
		public Tauth_Api_Info(DataRow dataRow):base(dataRow)
		{}
		public const string _API_ID = "API_ID";
		public const string _API_NAME = "API_NAME";
		public const string _API_URL = "API_URL";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _STATUS = "STATUS";
		public const string _APP_TYPE = "APP_TYPE";
		public const string _TableName = "TAUTH_API_INFO";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_API_INFO");
			table.Columns.Add(_API_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_API_NAME,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_API_URL,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_STATUS,typeof(int)).DefaultValue=1;
			table.Columns.Add(_APP_TYPE,typeof(int)).DefaultValue=0;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 主键(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Api_Id
		{
			get{ return Convert.ToInt32(DataRow[_API_ID]);}
			 set{setProperty(_API_ID, value);}
		}
		/// <summary>
		/// api功能名称(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 40Byte
		/// </para>
		/// </summary>
		public string Api_Name
		{
			get{ return DataRow[_API_NAME].ToString();}
			 set{setProperty(_API_NAME, value);}
		}
		/// <summary>
		/// api地址(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 100Byte
		/// </para>
		/// </summary>
		public string Api_Url
		{
			get{ return DataRow[_API_URL].ToString();}
			 set{setProperty(_API_URL, value);}
		}
		/// <summary>
		/// 发布时间(必填)
		/// <para>
		/// defaultValue: DateTime.Now;   Length: 7Byte
		/// </para>
		/// </summary>
		public DateTime Create_Time
		{
			get{ return Convert.ToDateTime(DataRow[_CREATE_TIME]);}
			 set{setProperty(_CREATE_TIME, value);}
		}
		/// <summary>
		/// 状态(必填)
		/// <para>
		/// defaultValue: 1;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Status
		{
			get{ return Convert.ToInt32(DataRow[_STATUS]);}
			 set{setProperty(_STATUS, value);}
		}
		/// <summary>
		/// 归属项目(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int App_Type
		{
			get{ return Convert.ToInt32(DataRow[_APP_TYPE]);}
			 set{setProperty(_APP_TYPE, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT API_ID,API_NAME,API_URL,CREATE_TIME,STATUS,APP_TYPE FROM TAUTH_API_INFO WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_API_INFO WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int api_id)
		{
			string condition = " API_ID=:API_ID";
			AddParameter(_API_ID,api_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " API_ID=:API_ID";
			AddParameter(_API_ID,DataRow[_API_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			int id = this.Api_Id = GetSequence("SELECT SEQ_TAUTH_API_INFO.nextval FROM DUAL");
			string sql = @"INSERT INTO TAUTH_API_INFO(API_ID,API_NAME,API_URL,STATUS,APP_TYPE)
			VALUES (:API_ID,:API_NAME,:API_URL,:STATUS,:APP_TYPE)";
			AddParameter(_API_ID,DataRow[_API_ID]);
			AddParameter(_API_NAME,DataRow[_API_NAME]);
			AddParameter(_API_URL,DataRow[_API_URL]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			AddParameter(_APP_TYPE,DataRow[_APP_TYPE]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_Api_InfoCollection.Field,object> alterDic,Dictionary<Tauth_Api_InfoCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tauth_Api_InfoCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tauth_Api_InfoCollection.Field key in conditionDic.Keys)
            {
                object value = conditionDic[key];
                string name = key.ToString();
				if (alterDic.Keys.Contains(key))
                {
                    name = string.Concat("condition_", key);
                }
                sql.Append(key).Append("=:").Append(name).Append(" and ");
                AddParameter(name, value);
            }
            int len = " and ".Length;
            sql.Remove(sql.Length - len, len);//移除最后一个and
            return UpdateBySql(sql.ToString());
		}
		protected bool UpdateByCondition(string condition)
		{
			ChangePropertys.Remove(_API_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_API_INFO SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE API_ID=:API_ID");
			AddParameter(_API_ID, DataRow[_API_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByPk(int api_id)
		{
			string condition = " API_ID=:API_ID";
			AddParameter(_API_ID,api_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	/// <summary>
	/// API服务接口信息[集合对象]
	/// </summary>
	public partial class Tauth_Api_InfoCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tauth_Api_InfoCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_Api_Info().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_Api_Info(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_Api_Info._TableName;}
		}
		public Tauth_Api_Info this[int index]
        {
            get { return new Tauth_Api_Info(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Api_Id=0,
			Api_Name=1,
			Api_Url=2,
			Create_Time=3,
			Status=4,
			App_Type=5,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT API_ID,API_NAME,API_URL,CREATE_TIME,STATUS,APP_TYPE FROM TAUTH_API_INFO WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_Api_Info Find(Predicate<Tauth_Api_Info> match)
        {
            foreach (Tauth_Api_Info item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_Api_InfoCollection FindAll(Predicate<Tauth_Api_Info> match)
        {
            Tauth_Api_InfoCollection list = new Tauth_Api_InfoCollection();
            foreach (Tauth_Api_Info item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_Api_Info> match)
        {
            foreach (Tauth_Api_Info item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_Api_Info> match)
        {
            BeginTransaction();
            foreach (Tauth_Api_Info item in this)
            {
                item.ReferenceTransactionFrom(Transaction);
                if (!match(item))
                    continue;
                if (!item.Delete())
                {
                    Rollback();
                    return false;
                }
            }
            Commit();
            return true;
        }
		#endregion
		#endregion		
	}
}