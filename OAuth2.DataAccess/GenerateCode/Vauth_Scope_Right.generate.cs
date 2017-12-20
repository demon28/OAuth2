   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Vauth_Scope_Right.generate.cs
 * CreateTime : 2017-11-23 09:31:15
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
using Winner.Framework.Utils;

namespace OAuth2.DataAccess
{
	/// <summary>
	/// 授权作用域权限信息视图
	/// </summary>
	public partial class Vauth_Scope_Right : DataAccessBase
	{
		#region 构造和基本
		public Vauth_Scope_Right():base()
		{}
		public Vauth_Scope_Right(DataRow dataRow):base(dataRow)
		{}
		public const string _SCOPE_ID = "SCOPE_ID";
		public const string _API_ID = "API_ID";
		public const string _API_NAME = "API_NAME";
		public const string _API_URL = "API_URL";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _STATUS = "STATUS";
		public const string _APP_TYPE = "APP_TYPE";
		public const string _TableName = "VAUTH_SCOPE_RIGHT";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("VAUTH_SCOPE_RIGHT");
			table.Columns.Add(_SCOPE_ID,typeof(int)).DefaultValue=DBNull.Value;
			table.Columns.Add(_API_ID,typeof(int)).DefaultValue=DBNull.Value;
			table.Columns.Add(_API_NAME,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_API_URL,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue=DBNull.Value;
			table.Columns.Add(_STATUS,typeof(int)).DefaultValue=DBNull.Value;
			table.Columns.Add(_APP_TYPE,typeof(int)).DefaultValue=DBNull.Value;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 授权作用域ID(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 22Byte
		/// </para>
		/// </summary>
		public int? Scope_Id
		{
			get{ return Helper.ToInt32(DataRow[_SCOPE_ID]);}
			 set{setProperty(_SCOPE_ID, value);}
		}
		/// <summary>
		/// api编号ID(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 22Byte
		/// </para>
		/// </summary>
		public int? Api_Id
		{
			get{ return Helper.ToInt32(DataRow[_API_ID]);}
			 set{setProperty(_API_ID, value);}
		}
		/// <summary>
		/// 接口名称(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 40Byte
		/// </para>
		/// </summary>
		public string Api_Name
		{
			get{ return DataRow[_API_NAME].ToString();}
			 set{setProperty(_API_NAME, value);}
		}
		/// <summary>
		/// 接口地址(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 100Byte
		/// </para>
		/// </summary>
		public string Api_Url
		{
			get{ return DataRow[_API_URL].ToString();}
			 set{setProperty(_API_URL, value);}
		}
		/// <summary>
		/// 创建时间(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 7Byte
		/// </para>
		/// </summary>
		public DateTime? Create_Time
		{
			get{ return Helper.ToDateTime(DataRow[_CREATE_TIME]);}
			 set{setProperty(_CREATE_TIME, value);}
		}
		/// <summary>
		/// 状态(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 22Byte
		/// </para>
		/// </summary>
		public int? Status
		{
			get{ return Helper.ToInt32(DataRow[_STATUS]);}
			 set{setProperty(_STATUS, value);}
		}
		/// <summary>
		/// 接口属于项目ID(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 22Byte
		/// </para>
		/// </summary>
		public int? App_Type
		{
			get{ return Helper.ToInt32(DataRow[_APP_TYPE]);}
			 set{setProperty(_APP_TYPE, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT SCOPE_ID,API_ID,API_NAME,API_URL,CREATE_TIME,STATUS,APP_TYPE FROM VAUTH_SCOPE_RIGHT WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM VAUTH_SCOPE_RIGHT WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		#endregion
	}
	/// <summary>
	/// 授权作用域权限信息视图[集合对象]
	/// </summary>
	public partial class Vauth_Scope_RightCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Vauth_Scope_RightCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Vauth_Scope_Right().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Vauth_Scope_Right(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Vauth_Scope_Right._TableName;}
		}
		public Vauth_Scope_Right this[int index]
        {
            get { return new Vauth_Scope_Right(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Scope_Id=0,
			Api_Id=1,
			Api_Name=2,
			Api_Url=3,
			Create_Time=4,
			Status=5,
			App_Type=6,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT SCOPE_ID,API_ID,API_NAME,API_URL,CREATE_TIME,STATUS,APP_TYPE FROM VAUTH_SCOPE_RIGHT WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Vauth_Scope_Right Find(Predicate<Vauth_Scope_Right> match)
        {
            foreach (Vauth_Scope_Right item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Vauth_Scope_RightCollection FindAll(Predicate<Vauth_Scope_Right> match)
        {
            Vauth_Scope_RightCollection list = new Vauth_Scope_RightCollection();
            foreach (Vauth_Scope_Right item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Vauth_Scope_Right> match)
        {
            foreach (Vauth_Scope_Right item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		#endregion
		#endregion		
	}
}