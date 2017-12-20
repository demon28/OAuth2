   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_App.generate.cs
 * CreateTime : 2017-10-29 10:33:05
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

namespace OAuth2.Merchant
{
	/// <summary>
	/// TAUTH_APP
	/// </summary>
	internal partial class Tauth_App : DataAccessBase
	{
		#region 构造和基本
		public Tauth_App():base()
		{}
		public Tauth_App(DataRow dataRow):base(dataRow)
		{}
		public const string _APP_ID = "APP_ID";
		public const string _APP_CODE = "APP_CODE";
		public const string _APP_NAME = "APP_NAME";
		public const string _APP_HOST = "APP_HOST";
		public const string _SECRET_KEY = "SECRET_KEY";
		public const string _UID_ENCRYPT_KEY = "UID_ENCRYPT_KEY";
		public const string _ACCESS_TOKEN = "ACCESS_TOKEN";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _REMARKS = "REMARKS";
		public const string _STATUS = "STATUS";
		public const string _IS_INTERNAL = "IS_INTERNAL";
		public const string _LOGO_URL = "LOGO_URL";
		public const string _TableName = "TAUTH_APP";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_APP");
			table.Columns.Add(_APP_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_APP_CODE,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_APP_NAME,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_APP_HOST,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_SECRET_KEY,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_UID_ENCRYPT_KEY,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_ACCESS_TOKEN,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_REMARKS,typeof(string)).DefaultValue=DBNull.Value;
			table.Columns.Add(_STATUS,typeof(int)).DefaultValue=0;
			table.Columns.Add(_IS_INTERNAL,typeof(int)).DefaultValue=0;
			table.Columns.Add(_LOGO_URL,typeof(string)).DefaultValue=DBNull.Value;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 应用ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int App_Id
		{
			get{ return Convert.ToInt32(DataRow[_APP_ID]);}
			 set{setProperty(_APP_ID, value);}
		}
		/// <summary>
		/// 应用账号(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		public string App_Code
		{
			get{ return DataRow[_APP_CODE].ToString();}
			 set{setProperty(_APP_CODE, value);}
		}
		/// <summary>
		/// 应用名称(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 40Byte
		/// </para>
		/// </summary>
		public string App_Name
		{
			get{ return DataRow[_APP_NAME].ToString();}
			 set{setProperty(_APP_NAME, value);}
		}
		/// <summary>
		/// 域名(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 100Byte
		/// </para>
		/// </summary>
		public string App_Host
		{
			get{ return DataRow[_APP_HOST].ToString();}
			 set{setProperty(_APP_HOST, value);}
		}
		/// <summary>
		/// 安全码(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		public string Secret_Key
		{
			get{ return DataRow[_SECRET_KEY].ToString();}
			 set{setProperty(_SECRET_KEY, value);}
		}
		/// <summary>
		/// OPEN_ID加密密钥(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		public string Uid_Encrypt_Key
		{
			get{ return DataRow[_UID_ENCRYPT_KEY].ToString();}
			 set{setProperty(_UID_ENCRYPT_KEY, value);}
		}
		/// <summary>
		/// 访问令牌(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 200Byte
		/// </para>
		/// </summary>
		public string Access_Token
		{
			get{ return DataRow[_ACCESS_TOKEN].ToString();}
			 set{setProperty(_ACCESS_TOKEN, value);}
		}
		/// <summary>
		/// 创建时间(必填)
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
		/// 备注信息(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 400Byte
		/// </para>
		/// </summary>
		public string Remarks
		{
			get{ return DataRow[_REMARKS].ToString();}
			 set{setProperty(_REMARKS, value);}
		}
		/// <summary>
		/// 应用状态(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Status
		{
			get{ return Convert.ToInt32(DataRow[_STATUS]);}
			 set{setProperty(_STATUS, value);}
		}
		/// <summary>
		/// 是否内部应用(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Is_Internal
		{
			get{ return Convert.ToInt32(DataRow[_IS_INTERNAL]);}
			 set{setProperty(_IS_INTERNAL, value);}
		}
		/// <summary>
		/// logo_url(可空)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 200Byte
		/// </para>
		/// </summary>
		public string Logo_Url
		{
			get{ return DataRow[_LOGO_URL].ToString();}
			 set{setProperty(_LOGO_URL, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT APP_ID,APP_CODE,APP_NAME,APP_HOST,SECRET_KEY,UID_ENCRYPT_KEY,ACCESS_TOKEN,CREATE_TIME,REMARKS,STATUS,IS_INTERNAL,LOGO_URL FROM TAUTH_APP WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_APP WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int app_id)
		{
			string condition = " APP_ID=:APP_ID";
			AddParameter(_APP_ID,app_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " APP_ID=:APP_ID";
			AddParameter(_APP_ID,DataRow[_APP_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			int id = this.App_Id = GetSequence("SELECT SEQ_TAUTH_APP.nextval FROM DUAL");
			string sql = @"INSERT INTO TAUTH_APP(APP_ID,APP_CODE,APP_NAME,APP_HOST,SECRET_KEY,UID_ENCRYPT_KEY,ACCESS_TOKEN,REMARKS,STATUS,IS_INTERNAL,LOGO_URL)
			VALUES (:APP_ID,:APP_CODE,:APP_NAME,:APP_HOST,:SECRET_KEY,:UID_ENCRYPT_KEY,:ACCESS_TOKEN,:REMARKS,:STATUS,:IS_INTERNAL,:LOGO_URL)";
			AddParameter(_APP_ID,DataRow[_APP_ID]);
			AddParameter(_APP_CODE,DataRow[_APP_CODE]);
			AddParameter(_APP_NAME,DataRow[_APP_NAME]);
			AddParameter(_APP_HOST,DataRow[_APP_HOST]);
			AddParameter(_SECRET_KEY,DataRow[_SECRET_KEY]);
			AddParameter(_UID_ENCRYPT_KEY,DataRow[_UID_ENCRYPT_KEY]);
			AddParameter(_ACCESS_TOKEN,DataRow[_ACCESS_TOKEN]);
			AddParameter(_REMARKS,DataRow[_REMARKS]);
			AddParameter(_STATUS,DataRow[_STATUS]);
			AddParameter(_IS_INTERNAL,DataRow[_IS_INTERNAL]);
			AddParameter(_LOGO_URL,DataRow[_LOGO_URL]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_AppCollection.Field,object> alterDic,Dictionary<Tauth_AppCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tauth_AppCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tauth_AppCollection.Field key in conditionDic.Keys)
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
			ChangePropertys.Remove(_APP_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_APP SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE APP_ID=:APP_ID");
			AddParameter(_APP_ID, DataRow[_APP_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByPk(int app_id)
		{
			string condition = " APP_ID=:APP_ID";
			AddParameter(_APP_ID,app_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
    /// <summary>
    /// TAUTH_APP[集合对象]
    /// </summary>
    internal partial class Tauth_AppCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tauth_AppCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_App().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_App(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_App._TableName;}
		}
		public Tauth_App this[int index]
        {
            get { return new Tauth_App(DataTable.Rows[index]); }
        }
		public enum Field
        {
			App_Id=0,
			App_Code=1,
			App_Name=2,
			App_Host=3,
			Secret_Key=4,
			Uid_Encrypt_Key=5,
			Access_Token=6,
			Create_Time=7,
			Remarks=8,
			Status=9,
			Is_Internal=10,
			Logo_Url=11,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT APP_ID,APP_CODE,APP_NAME,APP_HOST,SECRET_KEY,UID_ENCRYPT_KEY,ACCESS_TOKEN,CREATE_TIME,REMARKS,STATUS,IS_INTERNAL,LOGO_URL FROM TAUTH_APP WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_App Find(Predicate<Tauth_App> match)
        {
            foreach (Tauth_App item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_AppCollection FindAll(Predicate<Tauth_App> match)
        {
            Tauth_AppCollection list = new Tauth_AppCollection();
            foreach (Tauth_App item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_App> match)
        {
            foreach (Tauth_App item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_App> match)
        {
            BeginTransaction();
            foreach (Tauth_App item in this)
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