   /***************************************************
 *
 * Data Access Layer Of Winner Framework
 * FileName : Tauth_Token.generate.cs
 * CreateTime : 2017-08-10 14:32:08
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
	/// 用户授权令牌
	/// </summary>
	public partial class Tauth_Token : DataAccessBase
	{
		#region 构造和基本
		public Tauth_Token():base()
		{}
		public Tauth_Token(DataRow dataRow):base(dataRow)
		{}
		public const string _GRANT_ID = "GRANT_ID";
		public const string _TOKEN_ID = "TOKEN_ID";
		public const string _APP_ID = "APP_ID";
		public const string _USER_ID = "USER_ID";
		public const string _TOKEN_CODE = "TOKEN_CODE";
		public const string _SCOPE_ID = "SCOPE_ID";
		public const string _CREATE_TIME = "CREATE_TIME";
		public const string _EXPIRE_TIME = "EXPIRE_TIME";
		public const string _REFRESH_TOKEN = "REFRESH_TOKEN";
		public const string _REFRESH_TIMEOUT = "REFRESH_TIMEOUT";
		public const string _TableName = "TAUTH_TOKEN";
		protected override DataRow BuildRow()
		{
			DataTable table = new DataTable("TAUTH_TOKEN");
			table.Columns.Add(_GRANT_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_TOKEN_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_APP_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_USER_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_TOKEN_CODE,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_SCOPE_ID,typeof(int)).DefaultValue=0;
			table.Columns.Add(_CREATE_TIME,typeof(DateTime)).DefaultValue=DateTime.Now;
			table.Columns.Add(_EXPIRE_TIME,typeof(DateTime)).DefaultValue=DBNull.Value;
			table.Columns.Add(_REFRESH_TOKEN,typeof(string)).DefaultValue=string.Empty;
			table.Columns.Add(_REFRESH_TIMEOUT,typeof(DateTime)).DefaultValue=DBNull.Value;
			return table.NewRow();
		}
		#endregion
		
		#region 属性
		protected override string TableName
		{
			get{return _TableName;}
		}
		/// <summary>
		/// 授权ID（TAUTH_CODE.AUTH_ID）(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Grant_Id
		{
			get{ return Convert.ToInt32(DataRow[_GRANT_ID]);}
			 set{setProperty(_GRANT_ID, value);}
		}
		/// <summary>
		/// 令牌ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Token_Id
		{
			get{ return Convert.ToInt32(DataRow[_TOKEN_ID]);}
			 set{setProperty(_TOKEN_ID, value);}
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
		/// 用户ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int User_Id
		{
			get{ return Convert.ToInt32(DataRow[_USER_ID]);}
			 set{setProperty(_USER_ID, value);}
		}
		/// <summary>
		/// 令牌代码(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		public string Token_Code
		{
			get{ return DataRow[_TOKEN_CODE].ToString();}
			 set{setProperty(_TOKEN_CODE, value);}
		}
		/// <summary>
		/// 作用域ID(必填)
		/// <para>
		/// defaultValue: 0;   Length: 22Byte
		/// </para>
		/// </summary>
		public int Scope_Id
		{
			get{ return Convert.ToInt32(DataRow[_SCOPE_ID]);}
			 set{setProperty(_SCOPE_ID, value);}
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
		/// 过期时间(必填)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 7Byte
		/// </para>
		/// </summary>
		public DateTime Expire_Time
		{
			get{ return Convert.ToDateTime(DataRow[_EXPIRE_TIME]);}
			 set{setProperty(_EXPIRE_TIME, value);}
		}
		/// <summary>
		/// 刷新令牌的凭证(必填)
		/// <para>
		/// defaultValue: string.Empty;   Length: 50Byte
		/// </para>
		/// </summary>
		public string Refresh_Token
		{
			get{ return DataRow[_REFRESH_TOKEN].ToString();}
			 set{setProperty(_REFRESH_TOKEN, value);}
		}
		/// <summary>
		/// 刷新令牌过期时间(必填)
		/// <para>
		/// defaultValue: DBNull.Value;   Length: 7Byte
		/// </para>
		/// </summary>
		public DateTime Refresh_Timeout
		{
			get{ return Convert.ToDateTime(DataRow[_REFRESH_TIMEOUT]);}
			 set{setProperty(_REFRESH_TIMEOUT, value);}
		}
		#endregion
		
		#region 基本方法
		protected bool SelectByCondition(string condition)
		{
			string sql = "SELECT GRANT_ID,TOKEN_ID,APP_ID,USER_ID,TOKEN_CODE,SCOPE_ID,CREATE_TIME,EXPIRE_TIME,REFRESH_TOKEN,REFRESH_TIMEOUT FROM TAUTH_TOKEN WHERE "+condition;
			return base.SelectBySql(sql);
		}
		protected bool DeleteByCondition(string condition)
		{
			string sql = "DELETE FROM TAUTH_TOKEN WHERE "+condition;
			return base.DeleteBySql(sql);
		}
		
		public bool Delete(int token_id)
		{
			string condition = " TOKEN_ID=:TOKEN_ID";
			AddParameter(_TOKEN_ID,token_id);
			return DeleteByCondition(condition);
		}
		public bool Delete()
		{
			string condition = " TOKEN_ID=:TOKEN_ID";
			AddParameter(_TOKEN_ID,DataRow[_TOKEN_ID]);
			return DeleteByCondition(condition);
		}
				
		public bool Insert()
		{		
			int id = this.Token_Id = GetSequence("SELECT SEQ_TAUTH_TOKEN.nextval FROM DUAL");
			string sql = @"INSERT INTO TAUTH_TOKEN(GRANT_ID,TOKEN_ID,APP_ID,USER_ID,TOKEN_CODE,SCOPE_ID,EXPIRE_TIME,REFRESH_TOKEN,REFRESH_TIMEOUT)
			VALUES (:GRANT_ID,:TOKEN_ID,:APP_ID,:USER_ID,:TOKEN_CODE,:SCOPE_ID,:EXPIRE_TIME,:REFRESH_TOKEN,:REFRESH_TIMEOUT)";
			AddParameter(_GRANT_ID,DataRow[_GRANT_ID]);
			AddParameter(_TOKEN_ID,DataRow[_TOKEN_ID]);
			AddParameter(_APP_ID,DataRow[_APP_ID]);
			AddParameter(_USER_ID,DataRow[_USER_ID]);
			AddParameter(_TOKEN_CODE,DataRow[_TOKEN_CODE]);
			AddParameter(_SCOPE_ID,DataRow[_SCOPE_ID]);
			AddParameter(_EXPIRE_TIME,DataRow[_EXPIRE_TIME]);
			AddParameter(_REFRESH_TOKEN,DataRow[_REFRESH_TOKEN]);
			AddParameter(_REFRESH_TIMEOUT,DataRow[_REFRESH_TIMEOUT]);
			return InsertBySql(sql);
		}
		
		public bool Update()
		{
			return UpdateByCondition(string.Empty);
		}
		public bool Update(Dictionary<Tauth_TokenCollection.Field,object> alterDic,Dictionary<Tauth_TokenCollection.Field,object> conditionDic)
		{
			if (alterDic.Count <= 0)
                return false;
            if (conditionDic.Count <= 0)
                return false;
            StringBuilder sql = new StringBuilder();
            sql.Append("update ").Append(_TableName).Append(" set ");
            foreach (Tauth_TokenCollection.Field key in alterDic.Keys)
            {
                object value = alterDic[key];
                string name = key.ToString();
                sql.Append(name).Append("=:").Append(name).Append(",");
                AddParameter(name, value);
            }
            sql.Remove(sql.Length - 1, 1);//移除最后一个逗号
            sql.Append(" where ");
            foreach (Tauth_TokenCollection.Field key in conditionDic.Keys)
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
			ChangePropertys.Remove(_TOKEN_ID);
			if (ChangePropertys.Count == 0)
            {
                return true;
            }
            
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("UPDATE TAUTH_TOKEN SET");
			while (ChangePropertys.MoveNext())
            {
         		sql.AppendFormat(" {0}{1}=:{1} ", (ChangePropertys.CurrentIndex == 0 ? string.Empty : ","), ChangePropertys.Current);
                AddParameter(ChangePropertys.Current, DataRow[ChangePropertys.Current]);
            }
			sql.Append(" WHERE TOKEN_ID=:TOKEN_ID");
			AddParameter(_TOKEN_ID, DataRow[_TOKEN_ID]);			
			if (!string.IsNullOrEmpty(condition))
            {
				sql.AppendLine(" AND " + condition);
			}
			bool result = base.UpdateBySql(sql.ToString());
            ChangePropertys.Clear();
            return result;
		}	
		public bool SelectByAppId_UserId(int app_id,int user_id)
		{
			string condition = " APP_ID=:APP_ID AND USER_ID=:USER_ID";
			AddParameter(_APP_ID,app_id);
			AddParameter(_USER_ID,user_id);
			return SelectByCondition(condition);
		}
		public bool SelectByPk(int token_id)
		{
			string condition = " TOKEN_ID=:TOKEN_ID";
			AddParameter(_TOKEN_ID,token_id);
			return SelectByCondition(condition);
		}
		#endregion
	}
	
	public partial class Tauth_TokenCollection : DataAccessCollectionBase
	{
		#region 构造和基本
		public Tauth_TokenCollection():base()
		{			
		}
		
		protected override DataTable BuildTable()
		{
			return new Tauth_Token().CloneSchemaOfTable();
		}
		protected override DataAccessBase GetItemByIndex(int index)
        {
            return new Tauth_Token(DataTable.Rows[index]);
        }
		protected override string TableName
		{
			get{return Tauth_Token._TableName;}
		}
		public Tauth_Token this[int index]
        {
            get { return new Tauth_Token(DataTable.Rows[index]); }
        }
		public enum Field
        {
			Grant_Id=0,
			Token_Id=1,
			App_Id=2,
			User_Id=3,
			Token_Code=4,
			Scope_Id=5,
			Create_Time=6,
			Expire_Time=7,
			Refresh_Token=8,
			Refresh_Timeout=9,
		}
		#endregion
		#region 基本方法
		protected bool ListByCondition(string condition)
		{
			string sql = "SELECT GRANT_ID,TOKEN_ID,APP_ID,USER_ID,TOKEN_CODE,SCOPE_ID,CREATE_TIME,EXPIRE_TIME,REFRESH_TOKEN,REFRESH_TIMEOUT FROM TAUTH_TOKEN WHERE "+condition;
			return ListBySql(sql);
		}

		public bool ListAll()
		{
			string condition = " 1=1";
			return ListByCondition(condition);
		}
		#region Linq
		public Tauth_Token Find(Predicate<Tauth_Token> match)
        {
            foreach (Tauth_Token item in this)
            {
                if (match(item))
                    return item;
            }
            return null;
        }
        public Tauth_TokenCollection FindAll(Predicate<Tauth_Token> match)
        {
            Tauth_TokenCollection list = new Tauth_TokenCollection();
            foreach (Tauth_Token item in this)
            {
                if (match(item))
                    list.Add(item);
            }
            return list;
        }
        public bool Contains(Predicate<Tauth_Token> match)
        {
            foreach (Tauth_Token item in this)
            {
                if (match(item))
                    return true;
            }
            return false;
        }
		public bool DeleteAt(Predicate<Tauth_Token> match)
        {
            BeginTransaction();
            foreach (Tauth_Token item in this)
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